using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {

    public SteamVR_LoadLevel loadLevel;

    public LevelStats[] levels;
    public int currentLevel;
    //public string nextSceneName = "Level2";
    public int starsCollected;
    public GameObject ball;
    public GameObject goal;
    public Star[] starsInLevel;

    public Text scoreText;
    public int scoreValue = 0;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ResetLevel()
    {
        // turns all stars back on
        // puts score to 0

        for (int i = 0; i < starsInLevel.Length; i++)
        {
            starsInLevel[i].StarReset();
            gameObject.SetActive(true);
            scoreValue = 0;
            scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel.Length.ToString();
        }
            
    }

    public void IncrementScore()
    {
        scoreValue += 1;
        scoreText.text = "Stars:  " + scoreValue.ToString() + " / " + starsInLevel.Length.ToString();
        Debug.Log("I have gained 1 score");
    }

    public void GoToNextLevel()
    {
        //SteamVR_LoadLevel.Begin(nextSceneName);
        loadLevel.Trigger();

    }


}
