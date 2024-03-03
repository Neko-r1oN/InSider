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
        player = GameObject.Find("player-List");
        player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];

        // サブカメラとプレイヤーとの相対距離を求める
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        //Startで求めたプレイヤーとの位置関係を常にキープするようにカメラを動かす
        transform.position = player.transform.position + offset;
    }
}
