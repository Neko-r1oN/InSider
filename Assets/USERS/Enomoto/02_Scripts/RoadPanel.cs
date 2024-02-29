using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPanel : MonoBehaviour
{
    // ブロックのプレファブ
    [SerializeField] GameObject blockPrefab;

    // ステージの管理
    GameObject startPanel;

    // プレイヤー
    GameObject player;

    // デフォルトカラー
    Color defaultMaterial;

    // 「埋める」の対象になっているかどうか
    public bool isFill = false;

    // Start is called before the first frame update
    void Start()
    {
        // 取得する
        startPanel = GameObject.Find("StageManager");
        player = GameObject.Find("Player1");
        defaultMaterial = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().mode != Player.PLAYER_MODE.FILL)
        {// 埋めるモード以外の場合
            isFill = false;    // 偽
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {// Rayが当たったオブジェクトの情報をhitに渡す

            //**********************
            //  移動モードの場合
            //**********************
            if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.MOVE)
            {// モード：MOVE

                if (hit.transform.gameObject == this.gameObject)
                {// 自分にカーソルが当たった
                    gameObject.GetComponent<Renderer>().material.color = Color.blue; // 青色
                }
                else
                {
                    // デフォルトカラーに戻す
                    gameObject.GetComponent<Renderer>().material.color = defaultMaterial;
                }
            }

            //*************************************************************
            //  「埋める」の対象になっている場合（埋めるモードの場合）
            //*************************************************************
            else if (isFill == true && this.gameObject.tag == "RoadPanel")
            {// モード：FILL

                if (hit.transform.gameObject == this.gameObject)
                {// 自分にカーソルが当たった
                    gameObject.GetComponent<Renderer>().material.color = Color.green; // 緑色

                    // 左クリックした
                    if (Input.GetMouseButtonDown(0))
                    {
                        // オブジェクトを生成する
                        GameObject block = Instantiate(blockPrefab, new Vector3(transform.position.x, 1.47f, transform.position.z), Quaternion.identity);

                        // 破棄する
                        Destroy(this.gameObject);

                        // ベイクを開始
                        startPanel.GetComponent<StageBake>().StartBake();
                    }
                }
                else
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.yellow; // 黄色
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
