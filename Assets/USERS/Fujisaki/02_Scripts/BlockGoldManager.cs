using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlockGoldManager : MonoBehaviour
{
    /// <summary>
    /// 金が掘ったプレイヤーを追いかける処理
    /// </summary>
    GameObject playerManager;
    GameObject targetObj;
    public float speed = 5.0f;
    bool isStart;

    // 追尾するプレイヤーのID
    public int targetID;

    private void Start()
    {
        isStart = false;

        // Player
        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない

            // IDを代入する
            targetID = 0;

            playerManager = GameObject.Find("player-List");

            StartMove(0);
        }

        Debug.Log("あたいは金だよ");
    }

    void Update()
    {
        if(isStart == false)
        {
            return;
        }

        //スタート位置、ターゲットの座標、速度
        transform.position = Vector3.MoveTowards(
          transform.position,
          targetObj.transform.position,
          speed * Time.deltaTime);

        // Y座標を固定する
        transform.position = new Vector3(transform.position.x, 1f,transform.position.z);
    }

    /// <summary>
    /// Update処理を開始
    /// </summary>
    public void StartMove(int id)
    {
        targetID = id;
        playerManager = GameObject.Find("player-List");
        targetObj = playerManager.GetComponent<PlayerManager>().players[targetID];
        isStart = true;
    }

    /// <summary>
    /// プレイヤーと金が当たったら消す処理 [インスペクター上でレイヤーを指定済み]
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if(other.GetComponent<Player>().playerObjID == targetID)
            {// プレイヤーオブジェクトのIDがターゲットのIDと一致する
                // 加算するスコアをサーバーに送信する関数
                ScoreMethodList.Instance.SendAddScore();
            }
        }

        // 破棄する
        Destroy(this.gameObject);
        Debug.Log("あたりめ");
    }
}
