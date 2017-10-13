using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {
    
    public Text scoreText;
    public int scoreValue;
    public int starsInLevel = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

 /*   void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Throwable"))
        {
            //  if a ball touches this star destry star, increment score by 1000, play a sound
            gameObject.SetActive(false);
            scoreValue += 1;
            scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel;

        }
*/
    public void StarCollect()
    {
        gameObject.SetActive(false);
        scoreValue += 1;
        scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel;
    }


    public void StarReset()
    {
        gameObject.SetActive(true);
        //scoreValue = 0;
        //scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel;
    }

}
