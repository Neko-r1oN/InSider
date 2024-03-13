using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] GameObject mimicPrefab;
    [SerializeField] List<GameObject> text;
    public bool isMimic;    // ミミックかどうか

    bool isPlayer;  // プレイヤーを検知したかどうか

    private void Start()
    {
        isPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isPlayer == true)
        {
            return;
        }

        if (other.gameObject.layer == 3)
        {// プレイヤーの場合
            if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// 自分自身の場合
                if (ClientManager.Instance.isInsider == false)
                {// Insiderではない場合
                    SendRoundEndData();
                }
            }
        }
    }

    private async void SendRoundEndData()
    {
        if (isPlayer == true)
        {
            return;
        }

        Debug.Log("送信準備");

        isPlayer = true;

        // クラス変数を作成
        RoundEndData roundEndData = new RoundEndData();
        roundEndData.isMimic = isMimic;
        roundEndData.openPlayerID = ClientManager.Instance.playerID;

        if (isMimic == true)
        {// 自身がミミックの場合
            Debug.Log("ミミック");
        }
        else
        {// 宝箱の場合
            Debug.Log("宝箱");
        }

        // サーバーに送信する
        await ClientManager.Instance.Send(roundEndData, 4);
    }
}
