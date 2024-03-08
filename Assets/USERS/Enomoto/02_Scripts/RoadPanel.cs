using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RoadPanel : MonoBehaviour
{
    // ブロックのプレファブ
    [SerializeField] GameObject blockPrefab;

    // ステージの管理
    GameObject stageManager;

    // プレイヤー
    GameObject player;

    RoadManager roadManager;

    // デフォルトカラー
    Color defaultMaterial;

    // 「埋める」の対象になっているかどうか
    public bool isFill = false;

    // オブジェクトID
    public int objeID;

    public bool isSelect;

    // マネージャーを取得する
    GameObject manager;

    //モクモクアニメーションのプレハブを取得
    [SerializeField] GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        // 取得する
        manager = GameObject.Find("BlockList");
        stageManager = GameObject.Find("StageManager");

        GameObject roadManagerobj = GameObject.Find("RoadManager");
        roadManager = roadManagerobj.GetComponent<RoadManager>();
        
        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        defaultMaterial = gameObject.GetComponent<Renderer>().material.color;

        isSelect = false;
    }

    // Update is called once per frame
    async Task Update()
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

            //**********************
            //  埋める(サボタージュ)モードの場合
            //**********************
            else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEFILL)
            {// モード：MOVE

                if (hit.transform.gameObject == this.gameObject)
                {// 自分にカーソルが当たった

                    // スタートパネル以外だったら
                    if(hit.transform.gameObject.tag != "StartPanel")
                    {
                        // 色を黄色に変更
                        gameObject.GetComponent<Renderer>().material.color = Color.yellow; // 黄色
                    }
                    
                    // 左クリックした
                    if (Input.GetMouseButtonDown(0))
                    {
                        // falseだったら
                        if (isSelect == false)
                        {
                            // 埋める場所選択数をカウント
                            roadManager.fillCount++;

                            // リストにオブジェクト情報を格納
                            roadManager.blokObjList.Add(this.gameObject);
                            Debug.Log("追加しました。");

                            // trueに変更
                            isSelect = true;
                        }
                    }
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
                        if (EditorManager.Instance.useServer == true)
                        {// サーバーを使用する場合
                            // データ変数を設定
                            Action_FillData fillData = new Action_FillData();
                            fillData.playerID = ClientManager.Instance.playerID;
                            fillData.objeID = objeID;

                            Debug.Log("埋めるオブジェクトID : " + fillData.objeID);

                            // 送信処理
                            await ClientManager.Instance.Send(fillData, 6);
                        }
                        else
                        {// サーバーを使用しない
                            // オブジェクトを生成する
                            GameObject block = Instantiate(blockPrefab, new Vector3(transform.position.x, 1.47f, transform.position.z), Quaternion.identity);
                            
                            //モクモクするアニメーションの再生
                            Instantiate(smoke, new Vector3(transform.position.x, 1.47f, transform.position.z), Quaternion.identity);


                            // 破棄する
                            Destroy(this.gameObject);

                            // ベイクを開始
                            stageManager.GetComponent<StageManager>().StartBake();
                        }

                        // スタミナを減らす
                        player.GetComponent<Player>().SubStamina(20);
                        Debug.Log("残りスタミナ" + player.GetComponent<Player>().stamina);
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
