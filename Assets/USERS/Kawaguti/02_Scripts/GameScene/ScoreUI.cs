using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject scoreUI;
    TurnUI turnUI;
    static public int turnnum;    ///ìÆçÏämîFóp
    void Start()
    {
        turnnum = 0;
        scoreUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreUI.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreUI.SetActive(false);
        }
    }
}
