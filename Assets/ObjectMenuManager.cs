using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> objectList; //handled automatically at start
    public List<GameObject> objectPrefabList; // set manually in inspector and must match order of scene menu objects
    public int currentObject = 0;

    public int woodPlanks;
    public int metalPlanks;
    public int fans;
    public int trampolines;

    // Use this for initialization
    void Start () {
		foreach(Transform child in transform)
        {
            objectList.Add(child.gameObject);
        }
	}

    public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);
        currentObject--;
        if(currentObject < 0)
        {
            currentObject = objectList.Count - 1;
        }
        objectList[currentObject].SetActive(true);
    }

	public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject++;
        if (currentObject > objectList.Count - 1)
        {
            currentObject = 0;
        }
        objectList[currentObject].SetActive(true);
    }

    public void SpawnCurrentObject()
    {
        // handle the limited number of prefabs that can spawn

        if(currentObject == 0 && metalPlanks > 0) // this is the metal plank
        {
            Instantiate(objectPrefabList[currentObject],
            objectList[currentObject].transform.position,
            objectList[currentObject].transform.rotation);

            metalPlanks--; // decrement the # of metal planks
            //objectList[currentObject].GetComponentInChildren<Text>().text = "Metal Planks : " + metalPlanks;
        }

    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    public bool GetMenuActiveState()
    {
        if (gameObject.activeSelf == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
