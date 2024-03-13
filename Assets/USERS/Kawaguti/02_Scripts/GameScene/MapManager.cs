using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject GoldImg;
    [SerializeField] GameObject MimicImg;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void honto()
    {
        GoldImg.SetActive(false);
        MimicImg.SetActive(true);
    }
    public void uso()
    {
        GoldImg.SetActive(true);
        MimicImg.SetActive(false);
    }

}
