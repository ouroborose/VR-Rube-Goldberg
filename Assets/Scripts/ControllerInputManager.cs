using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;

    public bool isLeftController;

    //Teleporter variables
    private LineRenderer laser;
    public GameObject teleportAimerObject;
    public Vector3 teleportLocation;
    public GameObject player;
    public LayerMask laserMask;
    public LayerMask unteleportable;
    public float yNudgeAmount = 0.5f; //specific to telportAimerObject height



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

            if (Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask))
            //creates a ray from the controller position in the forward direction of the controller
            //outputs a hit object and has a max range of 15 meters can only collide with laserMask
            {
                teleportLocation = hit.point;
                laser.SetPosition(1, teleportLocation);
                // moves the cylinder to the hit position position position position position position
                teleportAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
            }

            else
            {
                teleportLocation = transform.position + transform.forward * 15;
                RaycastHit groundRay; // new raycast from the end of the laser to the ground
                if (Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
                {
                    // if that second raycast hits the ground, teleport there
                    teleportLocation = new Vector3(transform.forward.x * 15 + transform.position.x, groundRay.point.y, transform.forward.z * 15 + transform.position.z);
                }

                else
                {
                    teleportLocation = player.transform.position;
                }

                laser.SetPosition(1, transform.forward * 15 + transform.position);
                //aimer position
                teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);
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
        }

        // if player finger is on right touchpad, then enable ObjectMenu.
        // if player finger is lifted from right touchpad, then disable ObjectMenu.
	}
}
