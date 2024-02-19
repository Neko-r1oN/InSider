using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;

    // Start is called before the first frame update
    void Start()
    {
        road = GameObject.Find("RoadUI");
        road.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoad(bool set)
    {
        road.SetActive(set);
    }

    public bool ActiveRoad()
    {
        return road;
    }
}
