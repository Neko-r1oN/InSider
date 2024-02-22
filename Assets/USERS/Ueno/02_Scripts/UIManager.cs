using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;
    //[SerializeField] GameObject textUI;
    [SerializeField] List<GameObject> roadUIList;

    RoadManager roadManager;

    public GameObject GetRoadUI()
    {
        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        road = GameObject.Find("RoadUI");

        GameObject roadManagerObject = GameObject.Find("RoadManager");

        roadManager = roadManagerObject.GetComponent<RoadManager>();

        road.SetActive(false);

        //textUI = GameObject.Find("TextUI");

        //textUI.SetActive(false);
    }

    public void SetRoad(bool set)
    {
        //if(set == true)
        //{

        //}
        //else if(set == false)
        //{

        //}

        road.SetActive(set);
    }

    public bool ActiveRoad()
    {
        return road.activeSelf;
    }

    public void RotRoadUI()
    {
        for(int i = 0; i < roadUIList.Count; i++)
        {
            roadUIList[i].transform.Rotate(0f, 0f, -90f);

            Debug.Log(-roadManager.rotY);
        }
    }
}
