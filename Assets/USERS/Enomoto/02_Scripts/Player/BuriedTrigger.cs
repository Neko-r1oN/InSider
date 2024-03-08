using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Threading.Tasks;

public class BuriedTrigger : MonoBehaviour
{
    GameObject player;  // プレイヤー
    NavMeshAgent agent; // エージェント
    Vector3 startPos;   // 開始時の座標

    // トリガーの判定が入ったかどうか
    bool isBuried;

    // Start is called before the first frame update
    void Start()
    {
        // 一つ上の親を取得する
        player = transform.parent.gameObject;

        // Agent取得
        agent = player.GetComponent<NavMeshAgent>();

        // 開始時の座標取得する
        startPos = player.transform.position;
    }

    private async Task OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Block" && isBuried == false) // 処理が何度も入るのを阻止
        {// ブロックに埋まった
            Debug.Log("埋められた");

            // クラス変数を作成
            RevisionPosData revisionPosData = new RevisionPosData();
            revisionPosData.playerID = ClientManager.Instance.playerID;
            revisionPosData.targetID = ClientManager.Instance.playerID;
            revisionPosData.isBuried = true;
            revisionPosData.targetPosX = 0f;
            revisionPosData.targetPosY = 0.9f;
            revisionPosData.targetPosZ = -5f;

            // [revisionPosData]サーバーに送信
            await ClientManager.Instance.Send(revisionPosData, 12);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 偽にする
        isBuried = false;
    }
}
