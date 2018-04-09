using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    public bool isLeftController;

    //Teleporter variables
    private LineRenderer laser;
    public LineRenderer debugLine;
    public Material laserMat;
    public GameObject teleportAimerObject;
    public Vector3 teleportLocation;
    public GameObject player;
    public LayerMask laserMask;
    public int teleportDistance = 10; // in meters
    //public LayerMask unteleportable;
    public float yNudgeAmount = 0.5f; //specific to telportAimerObject height
    public GameObject levelHint;



	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        laser = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index); // access the controllers at the assigned index

        //teleport Stuff

        if(device.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) // getPress = button is held
        {
            laser.gameObject.SetActive(true);
            teleportAimerObject.SetActive(true);

            laser.SetPosition(0, gameObject.transform.position);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, teleportDistance, laserMask))
            //creates a ray from the controller position in the forward direction of the controller
            //outputs a hit object and has a max range of x meters can only collide with laserMask
            {
                Debug.Log("raycast hit laserMask");
                teleportLocation = hit.point;
                laser.SetPosition(1, teleportLocation);
                // moves the cylinder to the hit position position position position position position
                teleportAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
            }

            else // if the raycast does not hit the lasermask position
            {
                Vector3 endOfLaser = transform.position + transform.forward * teleportDistance;
                debugLine.SetPosition(0, endOfLaser);
                debugLine.SetPosition(1, endOfLaser + new Vector3(0, -30, 0));
                teleportLocation = endOfLaser;
                RaycastHit groundRayHit; // new raycast from the end of the laser to the ground
                if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRayHit, 17, laserMask))
                {
                    // if that second raycast hits the ground, teleport there
                    teleportLocation = groundRayHit.point + new Vector3(0, yNudgeAmount, 0);
                    //teleportLocation = new Vector3(transform.forward.x * teleportDistance + transform.position.x, groundRayHit.point.y, transform.forward.z * 15 + transform.position.z);
                }

                else // if the teleport laser is pointed completely out of the play area
                {
                    Vector3 playerHeadOffset = player.transform.position - Camera.main.transform.position;
                    playerHeadOffset.y = 0;
                    teleportLocation = player.transform.position - (playerHeadOffset);
                }

                laser.SetPosition(1, transform.forward * 15 + transform.position);
                //aimer position
                teleportAimerObject.transform.position = teleportLocation;
            }
        }

        if(device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) // getPressUp = button is released
        {
            laser.gameObject.SetActive(false);
            teleportAimerObject.SetActive(false);
            Vector3 playerHeadOffset = player.transform.position - Camera.main.transform.position;
            playerHeadOffset.y = 0;
                // vive space position - player camera position
            player.transform.position = teleportLocation + playerHeadOffset;
            device.TriggerHapticPulse(2800);
        }
        // toggle solution
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) // getPressUp = button is released
        {
            if (levelHint.activeSelf == false)
            {
                levelHint.SetActive(true);
            }
            else {
                levelHint.SetActive(false);
                }
            device.TriggerHapticPulse(4000);
        }

        // if player finger is on right touchpad, then enable ObjectMenu.
        // if player finger is lifted from right touchpad, then disable ObjectMenu.
    }
}
