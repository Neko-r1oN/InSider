using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI用
using UnityEngine.AI;   // AI用
using Unity.AI.Navigation;


public class Player : MonoBehaviour
{
    [SerializeField] Text modeText; // モード表示

    // 自分自身
    NavMeshAgent agent;

    // クリックしたパネルの座標を格納
    Vector3 clickedTarget;

    // アニメーター
    Animator animator;

    // 目的地を設定したかどうか
    bool isSetTarget = false;

    // プレイヤーのY座標を固定
    const float pos_Y = 0.9f;

    int stamina = 100;

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

        modeText.text = "現在のモード：移動";
    }

    // Update is called once per frame
    void Update()
    {
        // モード変更
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            modeText.text = "  現在のモード：移動";
            mode = PLAYER_MODE.MOVE;
            stamina -= 10;
            Debug.Log("残りスタミナ" + stamina);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            modeText.text = "  現在のモード：採掘";
            mode = PLAYER_MODE.MINING;
            stamina -= 10;
            Debug.Log("残りスタミナ" + stamina);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            modeText.text = "  現在のモード：埋める";
            mode = PLAYER_MODE.FILL;
            stamina -= 10;
            Debug.Log("残りスタミナ+ stamina");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            modeText.text = "  現在のモード：何もしない";
            mode = PLAYER_MODE.NOTHING;
            stamina -= 10;
            Debug.Log("残りスタミナ" + stamina);
        }

        // Y座標を固定 → 目的地に到達したかどうかの判定が難しくなるため
        transform.position = new Vector3(transform.position.x, pos_Y, transform.position.z);

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

        //// クリックした
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (mode == PLAYER_MODE.MINING || mode == PLAYER_MODE.FILL)
        //    {// モード：MINING || モード：FILL

        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit = new RaycastHit();

        //        if (Physics.Raycast(ray, out hit))
        //        {// Rayが当たったオブジェクトの情報をhitに渡す

        //            //******************************
        //            //  採掘モード
        //            //******************************
        //            if (other.gameObject.tag == "Block" && hit.transform.gameObject == other.gameObject
        //                && mode == PLAYER_MODE.MINING)
        //            {// ブロックの場合

        //                // 生成 → 破棄 → ベイク
        //                Bake(roadPrefab, new Vector3(other.transform.position.x, 0f, other.transform.position.z), 0, other.gameObject);
        //            }

        //            //******************************
        //            //  埋めるモード
        //            //******************************
        //            else if (other.gameObject.tag == "RoadPanel" && hit.transform.gameObject == other.gameObject
        //                && mode == PLAYER_MODE.FILL)
        //            {// 道パネルの場合

        //                // 生成 → 破棄 → ベイク
        //                Bake(blockPrefab, new Vector3(other.transform.position.x, 1.3f, other.transform.position.z), 0, other.gameObject);
        //            }
        //        }
        //    }
        //}
    }

    void PushButton()
    {
        
    }
}
