using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {
    
    public Ball ball;
    public GameLogic gameLogic;

    public Vector3 starInitialPosition;

	// Use this for initialization
	void Start () {
        starInitialPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            if (ball.cheating == false)
            {
                //  if a ball touches this star destry star, increment score by 1000, play a sound
                gameObject.SetActive(false);
                gameLogic.IncrementScore();
            }

        }
    }
    

    public void StarReset()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = starInitialPosition;
    }

}
