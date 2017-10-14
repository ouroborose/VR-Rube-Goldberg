using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {
    
    public Text scoreText;
    //public int scoreValue;
    //public int starsInLevel = 2;
    public Ball ball;

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
                //scoreValue += 1;
                //scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel;
            }

        }
    }

    /*
    public void StarCollect()
    {
        if (ball.cheating == false)
        {
            gameObject.SetActive(false);
            scoreValue += 1;
            scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel;
        }

    }
    */

    public void StarReset()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = starInitialPosition;
        //scoreValue = 0;
        //scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel;
    }

}
