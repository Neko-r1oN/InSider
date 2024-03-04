using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;
    [SerializeField] List<GameObject> roadUIList;

    // 情報を取得
    RoadManager roadManager;
    GameObject player;

    public GameObject GetRoadUI()
    {
        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 情報を取得
        road = GameObject.Find("RoadUI");

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        GameObject roadManagerObject = GameObject.Find("RoadManager");
        roadManager = roadManagerObject.GetComponent<RoadManager>();

        // RoadUIを非表示にする
        road.SetActive(false);
    }

    public void ShowRoad(int selectNum)
    {// RoadUIを表示する
        road.SetActive(true);

        if (selectNum >= 0)
        {
            // 前回選んだ道UIを非表示にする
            roadUIList[selectNum].SetActive(false);
        }
    }

    public void HideRoad(int selectNum)
    {
        if(selectNum >= 0)
        {
            // 非表示にしていた道UIを表示
            roadUIList[selectNum].SetActive(true);
        }

        // 道UIを非表示
        road.SetActive(false);

        // プレイヤーモードをMOVEに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    public bool ActiveRoad()
    {
        // true・falseを返す
        return road.activeSelf;
    }

    public void RotRoadUI()
    {
        for(int i = 0; i < roadUIList.Count; i++)
        {// リストの中身をカウントする

            // リストの全てを回転する
            roadUIList[i].transform.Rotate(0f, 0f, -90f);
        }
    }
}
