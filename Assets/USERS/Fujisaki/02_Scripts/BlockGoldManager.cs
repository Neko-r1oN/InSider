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

    GameObject player;

    // アイテムを拾ったときのエフェクト
    [SerializeField] GameObject goldEffect;

    // キラキラしてるやつ
    [SerializeField] GameObject goldEffect2;

    GameObject childEffect;

    // 追尾するプレイヤーのID
    public int targetID;

    private void Start()
    {
        // Player
        if (EditorManager.Instance.useServer == false)
        {// サーバーを使用しない

            isStart = false;

            // IDを代入する
            targetID = 0;

            playerManager = GameObject.Find("player-List");

            StartMove(0);
        }

        player = GameObject.Find("Player1");

        childEffect = Instantiate(goldEffect2,
            new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y,this.gameObject.transform.position.z),
             Quaternion.identity);

        Debug.Log("あたいは金だよ");
    }

    void Update()
    {
        if(isStart == false)
        {
            return;
        }

        //スタート位置、ターゲットの座標、速度
        transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, speed * Time.deltaTime);

        // Y座標を固定する
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
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

        Debug.Log("スタート");
    }

    /// <summary>
    /// プレイヤーと金が当たったら消す処理 [インスペクター上でレイヤーを指定済み]
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合

            if (other.gameObject.layer == 3)
            {// Playerのレイヤー

                if(other.gameObject.GetComponent<Player>() == null)
                {
                    return;
                }

                if (other.gameObject.GetComponent<Player>().playerObjID == targetID)
                {// プレイヤーオブジェクトのIDがターゲットのIDと一致する

                    if (ClientManager.Instance.playerID == targetID)
                    {// ターゲットのIDが自分自身のIDの場合は送信する
                     // 加算するスコアをサーバーに送信する関数
                        ScoreMethodList.Instance.SendAddScore();
                    }

                    GameObject childObject = Instantiate(goldEffect, player.transform);

                    childObject.transform.position = new Vector3(player.transform.position.x, 0.9f, player.transform.position.z);

                    Debug.Log(childObject);

                    // 破棄する
                    Destroy(this.gameObject);
                    Destroy(childEffect);
                    Debug.Log("あたりめ");
                }

            }

        }
        else
        {
            if (other.gameObject.layer == 3)
            {
                GameObject childObject = Instantiate(goldEffect, player.transform);

                childObject.transform.position = new Vector3(player.transform.position.x, 0.9f, player.transform.position.z);

                // 破棄する
                Destroy(this.gameObject);
                Destroy(childEffect);
                Debug.Log("あたりめ");
            }
               
        }
    }
}
