using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI用
using UnityEngine.AI;   // AI用
using Unity.AI.Navigation;
using DG.Tweening;
using System;

public class Player : MonoBehaviour
{
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

    // ランダム関数
    System.Random rnd = new System.Random();

    // パス
    NavMeshPath path = null;

    // 連続選択ができないよう前回の選択した数値を保存
    public int selectRoadNum = -1;

    // 目的地を設定したかどうか
    bool isSetTarget = false;

    // プレイヤーのY座標を固定
    const float pos_Y = 0.9f;

    // 完全に移動が完了したかどうか
    public bool isEnd = false;

    // スタミナ
    public int stamina = 100;

    // ランダムの数値を入れる変数
    int rand;

    int insiderCount = 0; // 内密者の人数をカウント

    public enum PLAYER_MODE
    {
        MOVE,   // 移動
        MINING, // 採掘
        FILL,   // 埋める
        NOTHING // 何もしない
    }

    // プレイヤーのモード
    public PLAYER_MODE mode = PLAYER_MODE.MOVE;

    private void Awake()
    {
        path = new NavMeshPath();
    }

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgentを取得する
        agent = GetComponent<NavMeshAgent>();

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

        // 0～6までのランダムの数値が入る
        rand = rnd.Next(0, 7);

        // 引数にランダムの数値を入れる
        SetPost(rand);
    }

    // Update is called once per frame
    void Update()
    {
        // Y座標を固定 → 目的地に到達したかどうかの判定が難しくなるため
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);

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

                if (hit.transform.tag == "RoadPanel" || hit.transform.tag == "StartPanel")
                {// 道パネルの場合

                    Vector3 pos = Input.mousePosition;
                    clickedTarget = hit.point;

                    //// 取得する
                    //clickedTarget = hit.collider.transform.position;

                    //// 調整
                    //clickedTarget = new Vector3(clickedTarget.x, pos_Y, clickedTarget.z);

                    //// NavMeshのパスを取得
                    NavMesh.CalculatePath(transform.position, clickedTarget, NavMesh.AllAreas, path);

                    if (path.corners.Length > 0)
                    {
                        var length = path.corners[path.corners.Length - 1] - clickedTarget;

                        // 真
                        isSetTarget = true;

                        if (length.magnitude < 1.0f)
                        {
                            if (EditorManager.Instance.useServer == true)
                            {// サーバーを使用する場合
                                // データ変数を設定
                                MoveData moveData = new MoveData();
                                moveData.playerID = ClientManager.Instance.playerID;
                                moveData.targetPosX = clickedTarget.x;
                                moveData.targetPosY = clickedTarget.y;
                                moveData.targetPosZ = clickedTarget.z;

                                Debug.Log(moveData.playerID);

                                // 送信する
                                ClientManager.Instance.SendData(moveData, 5);
                            }

                            // 目的地へ移動
                            agent.destination = clickedTarget;
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

                    // エージェントのベロシティが0以下になったら判定
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(agent.remainingDistance <= 0)
        {// 移動量が0以下
            // 滑らかに回転
            transform.forward = Vector3.Slerp(transform.forward, Vector3.back, Time.deltaTime * 8f);    // 後ろを向く
        }

        if (agent.remainingDistance > 0)
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
        //******************************
        //  採掘モード
        //******************************
        if(other.gameObject.tag == "Block")
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
        if(other.gameObject.tag == "RoadPanel")
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

    public void SubStamina(int num)
    {
        if(this.gameObject.tag == "Secrecy")
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

    public void AddStamina(int num)
    {
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

    public void SetPost(int rnd)
    {// タグを変更する
        if (rnd <= 4)
        {// 数値が4以下ならPioneerに設定
            this.gameObject.tag = "Pioneer";
        }
        else if (rnd >= 5)
        {// 数値が5以上ならSecrecyに設定
            if (insiderCount <= 1)
            {// カウントが1以下なら
                this.gameObject.tag = "Insider";

                // カウントを増やす
                insiderCount++;
            }
        }
    }
}
