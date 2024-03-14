using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // ステージの管理
    GameObject stageManager;

    // プレイヤー
    GameObject player;

    // RoadUI
    GameObject roadUI;

    // UIManager
    GameObject uiMnager;

    // RoadManager
    GameObject RoadManager;

    GameObject buttonManager;

    //MeshRenderer mr;

    // デフォルトカラー
    Color defaultMaterial;

    // Animator
    Animator animator;

    // 採掘の対象になっている場合
    public bool isMining = false;

    // オブジェクトID
    public int objeID;

    void Start()
    {
        // 取得する
        stageManager = GameObject.Find("StageManager");

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        // UIManager
        uiMnager = GameObject.Find("UIManager");

        // RoadManager取得する
        RoadManager = GameObject.Find("RoadManager");

        //mr = GameObject.Find("Road1").GetComponent<MeshRenderer>();

        // アニメーター情報取得
        animator = player.GetComponent<Animator>();

        buttonManager = GameObject.Find("ButtonManager");

        defaultMaterial = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Player>().mode != Player.PLAYER_MODE.MINING)
        {// 採掘モード以外の場合
            isMining = false;    // 偽
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {// Rayが当たったオブジェクトの情報をhitに渡す

            //**********************
            //  「採掘」の対象になっている場合（採掘モードの場合）
            //**********************
            if (isMining == true)
            {
                if (hit.transform.gameObject == this.gameObject)
                {// 自分にカーソルが当たった
                    gameObject.GetComponent<Renderer>().material.color = Color.green; // 緑色

                    // 左クリックした && 選択肢のUIが非表示の場合
                    if(Input.GetMouseButtonDown(0) && uiMnager.GetComponent<UIManager>().ActiveRoad() == false)
                    {
                        uiMnager.GetComponent<UIManager>().ShowRoadUI();

                        // ブロックの情報を渡す
                        RoadManager.GetComponent<RoadManager>().targetBlock = this.gameObject;

                        player.GetComponent<Player>().lookTarget = this.gameObject.transform.position;

                        buttonManager.GetComponent<ButtonManager>().canselButton.SetActive(false);
                    }
                }
                else
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.blue; // 青色
                }
            }

            //************************************
            //  その他
            //************************************
            else
            {
                // デフォルトカラーに戻す
                gameObject.GetComponent<Renderer>().material.color = defaultMaterial;
            }
        }

    }
}