using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject playerManager;

    List<GameObject> targetList;

    NavMeshAgent navMeshAgent;

    // 生成されてから三秒待機する
    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("player-List");

        // newする
        targetList = new List<GameObject>();

        // Player
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            targetList = playerManager.GetComponent<PlayerManager>().players;
        }
        else
        {// サーバーを使用しない
            targetList.Add(GameObject.Find("Player1"));
        }

        // NavMeshAgentを保持しておく
        navMeshAgent = GetComponent<NavMeshAgent>();

        isStart = false;

        Invoke("StartMove", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart == false)
        {
            return;
        }

        // 一番短い距離を格納
        float shortDis = 10000f;

        // ターゲットのインデックス番号
        int indexNum = 0;

        // ターゲットが存在するかどうか
        bool isTurget = true;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetComponent<Player>().isInvincible == false)
            {// プレイヤーが無敵状態以外の場合

                isTurget = true;

                Vector3 targetPos = targetList[i].transform.position;

                Vector3 enemyPos = transform.position;

                float distance = Vector3.Distance(targetPos, enemyPos);

                if (distance < shortDis)
                {
                    indexNum = i;
                    shortDis = distance;
                }
            }
            else
            {
                isTurget = false;
            }
        }

        if (isTurget == true)
        {// ターゲットがいる場合
            // プレイヤーを目指して進む
            navMeshAgent.destination = targetList[indexNum].transform.position;
        }
        else
        {// ターゲットがいない場合
            navMeshAgent.destination = this.transform.position;
        }
    }

    /// <summary>
    /// Updateの処理を開始
    /// </summary>
    private void StartMove()
    {
        isStart = true;
    }
}
