using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // 道パネルのプレファブ
    [SerializeField] GameObject roadPrefab;

    // ステージの管理
    GameObject startPanel;

    // プレイヤー
    GameObject player;

    // デフォルトカラー
    Color defaultMaterial;

    // 採掘の対象になっている場合
    public bool isMining = false;

    // Start is called before the first frame update
    void Start()
    {
        // 取得する
        startPanel = GameObject.Find("StageManager");
        player = GameObject.Find("Player");
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

                    // 左クリックした
                    if(Input.GetMouseButtonDown(0))
                    {
                        // 生成 → 破棄 → ベイク
                        Bake(roadPrefab, new Vector3(transform.position.x, 0f, transform.position.z), 0, this.gameObject);
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

    /// <summary>
    /// 生成、破棄、ベイクする
    /// </summary>
    /// <param name="prefab">生成するオブジェクト</param>
    /// <param name="pos">生成する座標</param>
    /// <param name="rotY">生成するときの回転</param>
    /// <param name="desObject">破棄するオブジェクト</param>
    private void Bake(GameObject prefab, Vector3 pos, int rotY, GameObject dieObject)
    {
        // オブジェクトを生成する
        GameObject block = Instantiate(prefab, pos, Quaternion.identity);

        // 破棄する
        Destroy(dieObject);

        // ベイクを開始
        startPanel.GetComponent<StageBake>().StartBake();
    }
}
