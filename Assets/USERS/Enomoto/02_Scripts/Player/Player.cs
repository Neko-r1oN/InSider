using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI用
using UnityEngine.AI;   // AI用
using Unity.AI.Navigation;
using DG.Tweening;
using System;
using Cinemachine;

public class Player : MonoBehaviour
{
    // このスクリプトを装備しているオブジェクトのID
    public int playerObjID;

    // スタート地点に復活するときのエフェクト
    [SerializeField] GameObject spawnEffectPrefab; 

    // 自分自身
    NavMeshAgent agent;

    // クリックしたパネルの座標を格納
    Vector3 clickedTarget;

    // アニメーター
    Animator animator;

    // スタミナゲージ
    GameObject staminaGauge;

    // スタミナゲージ内の値
    Text staminaNum;

    // パス
    NavMeshPath path = null;

    // PlayerManager
    GameObject manager;

    // GoldDrop
    [SerializeField] GameObject gold;

    // 目的地を設定したかどうか
    bool isSetTarget = false;

    // プレイヤーのY座標を固定
    const float pos_Y = 0.9f;

    // 完全に移動が完了したかどうか
    public bool isEnd = false;

    // スタミナ
    public int stamina;

    // 無敵状態かどうか
    public bool isInvincible;

    private double _time;

    // 点滅周期[s]
    [SerializeField] private float _cycle = 1;

    public enum PLAYER_MODE
    {
        MOVE,         // 移動
        MINING,       // 採掘
        FILL,         // 埋める
        NOTHING,      // 何もしない
        DOWN,         // ダウン
        SABOTAGEFILL, // 埋める(サボタージュ)
        SABOTAGEBOMB, // 爆弾(サボタージュ)
        SABOTAGETRAP  // トラップ(サボタージュ)
    }

    // プレイヤーのモード
    public PLAYER_MODE mode = PLAYER_MODE.MOVE;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgentを取得する
        agent = GetComponent<NavMeshAgent>();

        // アニメーター情報を取得
        animator = GetComponent<Animator>();

        // パスのクラス変数をnewする
        path = new NavMeshPath();

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (playerObjID != ClientManager.Instance.playerID)
            {// 自身のIDと一致しない場合
                return;
            }
        }

        // staminaGaugeを取得する
        staminaGauge = GameObject.Find("staminaGauge");

        // スタミナゲージ内の値を取得する
        staminaNum = GameObject.Find("StaminaNum").GetComponent<Text>();

        // 初期化
        clickedTarget = transform.position;

        // スタミナゲージのオブジェクト情報を取得
        staminaGauge = GameObject.Find("staminaGauge");

        // アニメーター情報を取得
        animator = GetComponent<Animator>();

        stamina = 50;
        
        // PlayerManagerを取得
        manager = this.transform.parent.gameObject;

        animator.keepAnimatorStateOnDisable = true;

        isInvincible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Y座標を固定 → 目的地に到達したかどうかの判定が難しくなるため
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (playerObjID != ClientManager.Instance.playerID)
            {// 自身のIDと一致しない場合
                return;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                DownPlayer(4);
            }
        }

        if(staminaNum == null)
        {// nullチェック
            return;
        }
        
        // 現在のスタミナを表示
        staminaNum.GetComponent<Text>().text = "" + stamina;

        // クリックした && モード：MOVE
        if (Input.GetMouseButtonDown(0) && mode == PLAYER_MODE.MOVE)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {// Rayが当たったオブジェクトの情報をhitに渡す

                Debug.Log(hit.transform.name);

                if (hit.transform.tag == "RoadPanel" || hit.transform.tag == "AbnormalPanel"
                    || hit.transform.tag == "EventPanel")
                {// 道パネルの場合

                    Vector3 pos = Input.mousePosition;
                    clickedTarget = hit.point;

                    //// NavMeshのパスを取得
                    NavMesh.CalculatePath(transform.position, clickedTarget, NavMesh.AllAreas, path);

                    if (path.corners.Length > 0)
                    {// パスを取得できる場合
                        var length = path.corners[path.corners.Length - 1] - clickedTarget;

                        // 真
                        isSetTarget = true;

                        if (length.magnitude < 1.0f)
                        {// 目的地にたどり着くことができる場合
                            if (EditorManager.Instance.useServer == true)
                            {// サーバーを使用する場合
                                // データ変数を設定
                                MoveData moveData = new MoveData();
                                moveData.playerID = ClientManager.Instance.playerID;
                                moveData.targetPosX = clickedTarget.x;
                                moveData.targetPosY = clickedTarget.y;
                                moveData.targetPosZ = clickedTarget.z;

                                // 送信する
                                ClientManager.Instance.SendData(moveData, 5);
                            }
                            else
                            {// サーバーを使用しない場合
                                // 目的地へ移動
                                agent.destination = clickedTarget;
                            }
                        }
                        else
                        {
                            Debug.Log("パスを取得できませせん");
                        }
                    }
                    else
                    {
                        Debug.Log("範囲外");
                    }
                }
            }
        }
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

    private void OnTriggerStay(Collider other)
    {
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (playerObjID != ClientManager.Instance.playerID)
            {// 自身のIDと一致しない場合
                return;
            }
        }

        //******************************
        //  採掘モード
        //******************************
        if (other.gameObject.tag == "Block" || other.gameObject.tag == "EventBlock")
        {// 採掘可能なブロック
            if (mode == PLAYER_MODE.MINING)
            {// 採掘モードの場合
                // このブロックも対象にする
                other.GetComponent<Block>().isMining = true;
            }
            else
            {
                // 対象から外す
                other.GetComponent<Block>().isMining = false;
            }
        }

        //******************************
        //  埋めるモード
        //******************************
        if (other.gameObject.tag == "RoadPanel")
        {// 埋めることが可能な道パネル
            if (mode == PLAYER_MODE.FILL)
            {// 埋めるモードの場合
                // この道パネルも対象にする
                other.GetComponent<RoadPanel>().isFill = true;
            }
            else
            {
                // 対象から外す
                other.GetComponent<RoadPanel>().isFill = false;
            }
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="targetPos"></param>
    public async void MoveAgent(Vector3 targetPos)
    {
        if(playerObjID == ClientManager.Instance.playerID)
        {// 自身のIDと一致する場合
            // 目的地へ移動
            agent.destination = targetPos;
        }
        else
        {// 自身のIDと一致しない場合
            // NavMeshのパスを取得
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
                    Debug.Log("他のプレイヤーオブジェクトが座標を修正する");

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
                Debug.Log("他のプレイヤーオブジェクトが座標を修正する");

                // 本来の座標に修正する通知を送信
                revisionPosData.playerID = ClientManager.Instance.playerID;
                revisionPosData.targetID = playerObjID;
                revisionPosData.isBuried = false;

                // [revisionPosData]サーバーに送信
                await ClientManager.Instance.Send(revisionPosData, 12);
            }

        }
    }

    /// <summary>
    /// 座標を修正する
    /// </summary>
    /// <param name="pos"></param>
    public void RevisionPos(Vector3 pos)
    {
        Debug.Log(pos);

        if(pos == new Vector3(0f, 0.9f, -5f))
        {// 引数の座標がスタート地点の場合

            Debug.Log("スタート地点に戻る");

            // 復活パーティクルを出す
            Instantiate(spawnEffectPrefab, pos,Quaternion.identity);
        }

        // Agentの目的地を設定
        agent.destination = pos;

        // コンポーネントを無効にする
        agent.enabled = false;

        // 開始位置へ移動
        transform.position = pos;

        // コンポーネントを有効にする
        agent.enabled = true;

        // 数秒後にboolをfalseにする
        Invoke("IsHitFalse", 2f);
    }

    public void SubStamina(int num)
    {
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (playerObjID != ClientManager.Instance.playerID)
            {// 自身のIDと一致しない場合
                return;
            }
        }

        if (this.gameObject.tag == "Insider")
        {
            // スタミナを減らす
            stamina -= num - 10;
        }
        else
        {
            // スタミナを減らす
            stamina -= num;
        }

        if (stamina <= 0)
        {// スタミナが0以下になった時
            // 0に固定する
            stamina = 0;
        }

        // スライダーを減らすアニメーション(DOTween)
        staminaGauge.GetComponent<Slider>().DOValue(stamina, 1f);

        // スタミナゲージ内の数値を減らす
        staminaNum.text = "" + stamina;
    }

    /// <summary>
    /// スタミナ回復処理
    /// </summary>
    /// <param name="num"></param>
    public void AddStamina(int num)
    {
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if (playerObjID != ClientManager.Instance.playerID)
            {// 自身のIDと一致しない場合
                return;
            }
        }

        // スタミナを増やす
        stamina += num;
        if (stamina >= 100)
        {// スタミナが100以上になった時
            // 100に固定する
            stamina = 100;
        }

        // スライダーを減らすアニメーション(DOTween)
        staminaGauge.GetComponent<Slider>().DOValue(stamina, 1f);

        // スタミナゲージ内の数値を減らす
        staminaNum.text = "" + stamina;

        // 残りスタミナを表示(デバックのみ)
        Debug.Log("残りスタミナ" + stamina);
    }

    /// <summary>
    /// ダウン処理
    /// </summary>
    public void DownPlayer(int dropNum)
    {
        if(isInvincible == true)
        {// ダウンしている場合
            return;
        }

        // フラグを真
        isInvincible = true;

        // ダウンモードに変更
        mode = PLAYER_MODE.DOWN;

        // Restアニメを再生する
        animator.Play("Rest");

        // 金のドロップ処理
        GoldDrop(dropNum);

        // 点滅のコルーチンを開始する
        manager.GetComponent<PlayerManager>().StartCoroutine
            (manager.GetComponent<PlayerManager>().StartBlink(playerObjID));

        Debug.Log("ダウンした");
    }

    /// <summary>
    /// ダウンから元に戻す処理
    /// </summary>
    public async void RecoverPlayer()
    {
        mode = PLAYER_MODE.MOVE;

        // Idleアニメを再生する
        animator.Play("MinerIdle");

        //----------------
        //  座標を修正
        //----------------
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用している場合
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
        else
        {
            // 座標を設定
            RevisionPos(new Vector3(0f, 0.9f, -5f));
        }
    }

    /// <summary>
    /// プレイヤーがダウン状態になったときに金を落とす処理
    /// </summary>
    /// <param name="num"></param>
    public void GoldDrop(int num)
    {
        Debug.Log("金を落とす");

        for (int i = 0; i < num; i++)
        {
            // 金を生成する
            Instantiate(gold, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
