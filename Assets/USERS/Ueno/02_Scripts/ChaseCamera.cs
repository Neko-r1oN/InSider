using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    GameObject player;
    Vector3 offset;

    private void Start()
    {
        // プレイヤー情報の取得

        // Player
        if (EditorManager.Instance.useServer)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        // サブカメラとプレイヤーとの相対距離を求める
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        //Startで求めたプレイヤーとの位置関係を常にキープするようにカメラを動かす
        transform.position = player.transform.position + offset;
    }
}
