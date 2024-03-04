using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class BuriedTrigger : MonoBehaviour
{
    GameObject player;  // プレイヤー
    NavMeshAgent agent; // エージェント
    Vector3 startPos;   // 開始時の座標

    // Start is called before the first frame update
    void Start()
    {
        // 一つ上の親を取得する
        player = transform.parent.gameObject;

        // Agent取得
        agent = player.GetComponent<NavMeshAgent>();

        // 開始時の座標取得する
        startPos = player.transform.position;

        Debug.Log("開始位置:"+startPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Block")
        {// ブロックに埋まった
            Debug.Log("埋められた");

            // 目的地を変更する
            //agent.destination = startPos;

            // 開始時の位置へ戻す
            //transform.parent.transform.position = new Vector3(startPos.x, startPos.y, startPos.y);

            // 復活パーティクルを出す
            //Instantiate();
        }
    }
}
