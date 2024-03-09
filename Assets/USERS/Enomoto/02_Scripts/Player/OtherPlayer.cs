using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   // AI用
using Unity.AI.Navigation;
using DG.Tweening;

public class OtherPlayer : MonoBehaviour
{
    // 自分自身
    NavMeshAgent agent;

    // アニメーター
    Animator animator;

    // パス
    NavMeshPath path = null;

    // プレイヤーのY座標を固定
    const float pos_Y = 0.9f;

    // 完全に移動が完了したかどうか
    public bool isEnd = false;

    // このスクリプトを装備しているオブジェクトのID
    public int playerObjID;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgentを取得する
        agent = GetComponent<NavMeshAgent>();

        // アニメーター情報を取得
        animator = GetComponent<Animator>();

        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        // Y座標を固定 → 目的地に到達したかどうかの判定が難しくなるため
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (agent.remainingDistance <= 0.1f)
        {// 移動量が0以下
            // 滑らかに回転
            transform.forward = Vector3.Slerp(transform.forward, Vector3.back, Time.deltaTime * 8f);    // 後ろを向く
        }

        if (agent.remainingDistance > 0.1f)
        {// 移動中は偽
            // 任意のアニメーションをtrueに変更
            animator.SetBool("Run", true);

            isEnd = false;
        }
        else if (Mathf.Abs(transform.localEulerAngles.y) >= 179f && isEnd == false) // 条件を絶対値にする
        {// 回転が終了すると

            // 回転を調整
            transform.localEulerAngles = new Vector3(0, 180, 0);

            isEnd = true;

            // 任意のアニメーションをfalseに変更
            animator.SetBool("Run", false);
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="targetPos"></param>
    public async void MoveAgent(Vector3 targetPos)
    {
        //// NavMeshのパスを取得
        NavMesh.CalculatePath(transform.position, targetPos, NavMesh.AllAreas, path);

        // クラス変数を作成
        RevisionPosData revisionPosData = new RevisionPosData();

        if (path.corners.Length > 0)
        {// パスが取れた場合
            var length = path.corners[path.corners.Length - 1] - targetPos;

            if (length.magnitude < 1.0f)
            {// 目的地にたどり着くことができる場合

                // 目的地へ移動
                agent.destination = targetPos;
            }
            else
            {
                Debug.Log("座標を修正する");

                // 本来の座標に修正する通知を送信
                revisionPosData.playerID = ClientManager.Instance.playerID;
                revisionPosData.targetID = playerObjID;
                revisionPosData.isBuried = false;

                // [revisionPosData]サーバーに送信
                await ClientManager.Instance.Send(revisionPosData, 12);
            }
        }
        else
        {
            Debug.Log("座標を修正する");

            // 本来の座標に修正する通知を送信
            revisionPosData.playerID = ClientManager.Instance.playerID;
            revisionPosData.targetID = playerObjID;
            revisionPosData.isBuried = false;

            // [revisionPosData]サーバーに送信
            await ClientManager.Instance.Send(revisionPosData, 12);
        }
    }

    /// <summary>
    /// 座標を修正する
    /// </summary>
    /// <param name="pos"></param>
    public void RevisionPos(Vector3 pos,bool isBruried)
    {
        if (isBruried == true)
        {// ブロックに埋まった時の場合

            // 座標を書き換える
            pos = new Vector3(0f, 0.9f, -5f);

            // 復活パーティクルを出す
            //Instantiate();

            Debug.Log("posを修正");
        }

        // Agentの目的地を設定
        agent.destination = pos;

        // コンポーネントを無効にする
        agent.enabled = false;

        // 開始位置へ移動
        transform.position = pos;

        // コンポーネントを有効にする
        agent.enabled = true;

        Debug.Log(pos);
    }
}
