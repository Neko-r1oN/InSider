using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Threading.Tasks;

public class BuriedAndHit : MonoBehaviour
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
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合

            // クラス変数を作成
            RevisionPosAndDropGoldData revisionPosData = new RevisionPosAndDropGoldData();

            Player playerCom = player.GetComponent<Player>();

            if(playerCom.mode == Player.PLAYER_MODE.MINING || playerCom.mode == Player.PLAYER_MODE.FILL)
            {// 採掘・埋めるModeのとき
                return;
            }

            if (other.transform.tag == "Block" 
                && playerCom.isInvincible == false  // ダウン状態ではない場合
                && isBuried == false) // 処理が何度も入るのを阻止
            {// ブロックに埋まった

                Debug.Log("埋められた");

                // クラス変数を作成
                revisionPosData.playerID = ClientManager.Instance.playerID;
                revisionPosData.targetID = ClientManager.Instance.playerID;
                revisionPosData.isBuried = true;
                revisionPosData.targetPosX = 0f;
                revisionPosData.targetPosY = 0.9f;
                revisionPosData.targetPosZ = -5f;

                // [revisionPosData]サーバーに送信
                await ClientManager.Instance.Send(revisionPosData, 12);
            }
            else if (other.gameObject.layer == 7)
            {// 敵に触れた || イベントの落石が当たった

                if (player.GetComponent<Player>().isInvincible == false)
                {// Playerがダウンしていない場合

                    Debug.Log("敵に攻撃された");

                    // クラス変数を作成
                    revisionPosData.playerID = ClientManager.Instance.playerID;
                    revisionPosData.targetID = ClientManager.Instance.playerID;
                    revisionPosData.isDown = true;

                    // [revisionPosData]サーバーに送信
                    await ClientManager.Instance.Send(revisionPosData, 12);
                }
            }
        }
        else
        {// サーバーを使用しない場合
            if (other.gameObject.layer == 7 && player.GetComponent<Player>().isInvincible == false)
            {// 敵に触れた && Playerがダウンしていない場合
                Debug.Log("タッチされた");

                player.GetComponent<Player>().DownPlayer(4);

                Debug.Log(player.GetComponent<Player>().mode);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        // 偽にする
        isBuried = false;
    }
}
