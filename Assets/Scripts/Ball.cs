using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Vector3 originalPosition;
    private Vector3 originalVelocity;
    private Quaternion originalRotation;
    public Rigidbody ballRigidbody;

    public GameLogic gameLogic;
    public bool cheating = false;
    public Material cheatingMat;
    public Material defaultMat;
    public LayerMask cheatDetectionLayerMask;

    //public int starsInLevel;   <- make this a const in a gamelogic script

    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
        originalVelocity = ballRigidbody.velocity;
        originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Vector3.down, out hit, 15, cheatDetectionLayerMask))
        {
            PlayerIsNotCheating();
        }

        else
        {
            PlayerIsCheating();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // use OnCollisionEnter with a tag check
        // reset the position and velocity of the ball when it collides with the ground.
        if (collision.gameObject.CompareTag("Ground"))
        {

            if (gameLogic != null)
            {
                transform.position = originalPosition;
                ballRigidbody.velocity = originalVelocity;
                transform.rotation = originalRotation;

                gameLogic.ResetLevel();
            }

            // turns stars back on, decrement score by 1000 for each star
            //GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
            //foreach (GameObject star in stars)
            //{
                //if(star.GetComponentInChildren<GameObject>().activeSelf == false)
                //{
                 //   Debug.Log("This star is deactivated");
                  //  star.GetComponent<CollectStar>().StarReset();
               // }
                
            //}
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            if (cheating == false)
            {
                // if the ball has collected all the stars
                Debug.Log("Level Complete");
                gameLogic.GoToNextLevel();
            }

        }

        // ball has touched and collected a star
        if (collision.gameObject.CompareTag("Star"))
        {
            if (cheating == false)
            {
                Star collidedStar = collision.gameObject.GetComponent<Star>();

                if (collidedStar != null)
                {
                    collidedStar.StarCollect();
                    // tell gamelogic to do a score increment
                    gameLogic.IncrementScore();
                }
            }

        }

    }

    public void PlayerIsCheating()
    {
        cheating = true;
        gameObject.GetComponent<Renderer>().material = cheatingMat;
    }

    public void PlayerIsNotCheating()
    {
        cheating = false;
        gameObject.GetComponent<Renderer>().material = defaultMat;
    }
}
