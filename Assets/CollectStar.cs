using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStar : MonoBehaviour {

    public BoxCollider starCollider;

	// Use this for initialization
	void Start () {
        starCollider = GetComponentInChildren<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            /*  if a ball touches this star
             *  destry star, increment score by 1000, play a sound
             */

            
        }

    }

}
