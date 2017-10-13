using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyWind : MonoBehaviour
{

    public float forceMultiplier = 20;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            /*  look at the forward vector of this wind
             *  apply a force to the throwable's rigidbody (use addForce()) and apply the forward vector as the parameter
             */
            Vector3 WindDirection = transform.forward;
            col.GetComponent<Rigidbody>().AddForce(WindDirection * forceMultiplier);
        }

    }
}