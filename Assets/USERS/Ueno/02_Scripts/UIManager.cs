using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;
    //[SerializeField] GameObject textUI;
    [SerializeField] List<GameObject> roadUIList;

    RoadManager roadManager;
    GameObject player;

    public GameObject GetRoadUI()
    {
        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        road = GameObject.Find("RoadUI");
        player = GameObject.Find("Player");

        GameObject roadManagerObject = GameObject.Find("RoadManager");

        roadManager = roadManagerObject.GetComponent<RoadManager>();

        // RoadUI���\���ɂ���
        road.SetActive(false);

        //textUI = GameObject.Find("TextUI");

        //textUI.SetActive(false);
    }

    public void ShowRoad(int selectNum)
    {
        road.SetActive(true);

        if (selectNum >= 0)
        {
            roadUIList[selectNum].SetActive(false);

            //roadUIList[selectNum].GetComponent<Image>().material.color 
            //    = new Color32(0, 0, 0, 150);
        }
    }

    public void HideRoad(int selectNum)
    {
        if(selectNum >= 0)
        {
            roadUIList[selectNum].SetActive(true);

            //roadUIList[selectNum].GetComponent<Image>().material.color 
            //    = new Color32(0, 0, 0, 150);
        }

        road.SetActive(false);

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    public bool ActiveRoad()
    {
        // true�Efalse��Ԃ�
        return road.activeSelf;
    }

    public void RotRoadUI()
    {
        for(int i = 0; i < roadUIList.Count; i++)
        {// ���X�g�̒��g���J�E���g����

            // ���X�g�̑S�Ă���]����
            roadUIList[i].transform.Rotate(0f, 0f, -90f);
        }
    }
}
