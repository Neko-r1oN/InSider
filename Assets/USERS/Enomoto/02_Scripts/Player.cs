using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI用
using UnityEngine.AI;   // AI用
using Unity.AI.Navigation;
using DG.Tweening;


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

    // 目的地を設定したかどうか
    bool isSetTarget = false;

    // プレイヤーのY座標を固定
    const float pos_Y = 0.9f;

    // 完全に移動が完了したかどうか
    public bool isEnd = false;

    // スタミナ
    int stamina = 100;

    // スタミナゲージの数値
    GameObject staminaNum;

    public enum PLAYER_MODE
    {
        MOVE,   // 移動
        MINING, // 採掘
        FILL,   // 埋める
        NOTHING // 何もしない
    }

    // プレイヤーのモード
    public PLAYER_MODE mode = PLAYER_MODE.MOVE;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgentを取得する
        agent = GetComponent<NavMeshAgent>();

        // 初期化
        clickedTarget = transform.position;

        // スタミナゲージのオブジェクト情報を取得
        staminaGauge = GameObject.Find("staminaGauge");

        // StaminaNum情報を取得
        staminaNum = GameObject.Find("StaminaNum");

        // アニメーター情報を取得
        animator = GetComponent<Animator>();
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

                if (hit.transform.tag == "RoadPanel")
                {// 道パネルの場合
                    // 取得する
                    clickedTarget = hit.collider.transform.position;

                    // 調整
                    clickedTarget = new Vector3(clickedTarget.x, pos_Y, clickedTarget.z);

                    // 真
                    isSetTarget = true;

                    // 目的地へ移動
                    agent.destination = clickedTarget;

                    // スタミナを減らす
                    SubStamina(10);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(agent.velocity.magnitude <= 0)
        {// 移動量が0以下
            // 滑らかに回転
            transform.forward = Vector3.Slerp(transform.forward, Vector3.back, Time.deltaTime * 8f);    // 後ろを向く
        }

        if (agent.velocity.magnitude > 0)
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
        // スタミナを減らす
        stamina -= num;

        if(stamina <= 0)
        {// スタミナが0以下になった時
            // 0に固定する
            stamina = 0;
        }

        // スライダーを減らすアニメーション(DOTween)
        staminaGauge.GetComponent<Slider>().DOValue(stamina,1.5f);

        // 残りスタミナを表示(デバックのみ)
        Debug.Log("残りスタミナ" + stamina);
    }
}
