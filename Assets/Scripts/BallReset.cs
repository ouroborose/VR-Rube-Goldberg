using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    private Vector3 originalPosition;
    private Vector3 originalVelocity;
    public Rigidbody ballRigidbody;

	// Use this for initialization
	void Start () {
        originalPosition = transform.position;
        originalVelocity = ballRigidbody.velocity;
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision collision)
    {
        // use OnCollisionEnter with a tag check
        // reset the position and velocity of the ball when it collides with the ground.
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("This hit the floor");
            transform.position = originalPosition;
            ballRigidbody.velocity = originalVelocity;
        }

    }
}
