using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device device;
    public float throwForce = 1.5f;

    // swipe variables
    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasPressedLeft;
    public bool hasPressedRight;
    public ObjectMenuManager objectMenuManager;

	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {

        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (objectMenuManager == null) {
            return;
        }
        
        if(device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            objectMenuManager.ShowMenu();
            //touchLast = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            // if the user presses the left half of the touchpad
            if(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x < 0)
            {
                //Debug.Log("pressing on" + device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x);
                SwipeLeft();
                hasPressedLeft = true;
                hasPressedRight = false;
            }

            // if the user presses the right half of the touchpad
            if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0)
            {
                //Debug.Log("pressing on" + device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x);
                SwipeRight();
                hasPressedLeft = false;
                hasPressedRight = true;
            }


            //only accessing x horizontal value of touchpad
            /*
            touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            distance = touchCurrent - touchLast;
            touchLast = touchCurrent;
            swipeSum += distance;
            */


/*
            if (!hasSwipedRight)
            {
                if (swipeSum > 0.5f)
                {
                    swipeSum = 0;
                    SwipeRight();
                    hasSwipedRight = true;
                    hasSwipedLeft = false;
                }
            }
            if(!hasSwipedLeft)
            {
                if (swipeSum < -0.5f)
                {
                    swipeSum = 0;
                    SwipeLeft();
                    hasSwipedRight = false;
                    hasSwipedLeft = true;

                }
            }
            */
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad) )
        {
            swipeSum = 0;
            touchCurrent = 0;
            touchLast = 0;
            hasPressedLeft = false;
            hasPressedRight = false;
            objectMenuManager.HideMenu();
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && objectMenuManager.GetMenuActiveState() == true)
        {
            // spawn object selected by menu
            SpawnObject();
        }
	}

    void SpawnObject()
    {
        objectMenuManager.SpawnCurrentObject();
    }

    void SwipeLeft()
    {
        objectMenuManager.MenuLeft();
    }

    void SwipeRight()
    {
        objectMenuManager.MenuRight();
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.CompareTag("Throwable"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(col);
            }
            else if(device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(col);
            }
        }

        if (col.gameObject.CompareTag("Structure"))
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                DropObject(col);
            }
            else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(col);
            }
        }
    }

    void GrabObject(Collider coli)
    {
        coli.transform.SetParent(gameObject.transform);
        coli.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(2000);
        //Debug.Log("you're pulling the trigger on an object");
    }

    void ThrowObject(Collider coli)
    {
        coli.transform.SetParent(null);
        Rigidbody rigidBody = coli.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.velocity = device.velocity * throwForce;
        rigidBody.angularVelocity = device.angularVelocity;
        //Debug.Log("you have released the trigger");

        // otherwise, if player is standing outside of the start zone, then color the ball red , make cheating boolean true
        //Ball ball = coli.GetComponent<Ball>();
       // ball.PlayerIsCheating();
    }

    void DropObject(Collider coli)
    {
        coli.transform.SetParent(null);
    }
}
