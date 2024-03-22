using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> text;
    public bool isMimic;    // ミミックかどうか

    GameObject textUI;

    // チェストの中身を表示するテキスト
    Text chestText;

    GameObject player;

    public int chestNum;

    bool isPlayer;  // プレイヤーを検知したかどうか
    private void Start()
    {
        isPlayer = false;

        textUI = GameObject.Find("TextUIManager");
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            //**********************************
            // rayがチェストに当たっていたら
            //**********************************
            if (hit.transform.gameObject == this.gameObject)
            {
                //*********************************
                //サーバーを使っているとき
                //*********************************
                if (EditorManager.Instance.useServer == true)
                {
                    if (ClientManager.Instance.isInsider)
                    {
                        if (isMimic == true)
                        {// ミミックだったら
                            // ミミックのテキストを表示
                            textUI.GetComponent<TextUIManager>().OnMouseEnter(10);
                        }
                        else
                        {// ミミックじゃないとき
                            // 真ん中のチェスト
                            if (chestNum == 1)
                            {
                                // OnMouseEnter(11)はたからばこ
                                textUI.GetComponent<TextUIManager>().OnMouseEnter(11);
                            }
                            // 右のチェスト
                            else if (chestNum == 2)
                            {
                                // OnMouseEnter(12)はたからばこ
                                textUI.GetComponent<TextUIManager>().OnMouseEnter(12);
                            }
                            // 左のチェスト
                            else if (chestNum == 3)
                            {
                                // OnMouseEnter(13)はたからばこ
                                textUI.GetComponent<TextUIManager>().OnMouseEnter(13);
                            }
                        }
                    }
                }
                //********************************
                // サーバーを使っていないとき
                //********************************
                else
                {
                    // 真ん中のチェスト
                    if (chestNum == 1)
                    {
                        // OnMouseEnter(11)はたからばこ
                        textUI.GetComponent<TextUIManager>().OnMouseEnter(11);
                    }
                    // 右のチェスト
                    else if (chestNum == 2)
                    {
                        // OnMouseEnter(12)はたからばこ
                        textUI.GetComponent<TextUIManager>().OnMouseEnter(12);
                    }
                    // 左のチェスト
                    else if (chestNum == 3)
                    {
                        // OnMouseEnter(13)はたからばこ
                        textUI.GetComponent<TextUIManager>().OnMouseEnter(13);
                    }
                }
            }
            //***************************************
            // rayが外れたら
            //***************************************
            else if (hit.transform.gameObject != this.gameObject)
            {
                //*********************************
                //サーバーを使っているとき
                //*********************************
                if (EditorManager.Instance.useServer == true)
                {
                    if (ClientManager.Instance.isInsider)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }
                        else
                        {
                            // 真ん中のチェスト
                            if (chestNum == 1)
                            {
                                textUI.GetComponent<TextUIManager>().OnMouseExit(11);
                            }
                            // 右のチェスト
                            else if (chestNum == 2)
                            {
                                textUI.GetComponent<TextUIManager>().OnMouseExit(12);
                            }
                            // 左のチェスト
                            else if (chestNum == 3)
                            {
                                textUI.GetComponent<TextUIManager>().OnMouseExit(13);
                            }
                        }
                    }
                }
                //*********************************
                //サーバーを使っていないとき
                //*********************************
                else
                {
                    // 真ん中のチェスト
                    if (chestNum == 1)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }

                        textUI.GetComponent<TextUIManager>().OnMouseExit(11);
                    }
                    // 右のチェスト
                    else if (chestNum == 2)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }

                        textUI.GetComponent<TextUIManager>().OnMouseExit(12);
                    }
                    // 左のチェスト
                    else if (chestNum == 3)
                    {
                        if (isMimic == true)
                        {
                            textUI.GetComponent<TextUIManager>().OnMouseExit(10);
                        }

                        textUI.GetComponent<TextUIManager>().OnMouseExit(13);
                    }
                }
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer == true)
        {
            return;
        }

        if (other.gameObject.layer == 3)
        {// プレイヤーの場合

            if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// 自分自身の場合

                Debug.Log("OKOKOKOKO:" + other.gameObject.name);

                if (ClientManager.Instance.isInsider == false)
                {// Insiderではない場合
                    SendRoundEndData();
                }
            }
        }
    }

    private async void SendRoundEndData()
    {
        if (isPlayer == true)
        {
            return;
        }

        isPlayer = true;

        // クラス変数を作成
        RoundEndData roundEndData = new RoundEndData();
        roundEndData.isMimic = isMimic;
        roundEndData.openPlayerID = ClientManager.Instance.playerID;

        if (isMimic == true)
        {// 自身がミミックの場合
            Debug.Log("ミミック");
        }
        else
        {// 宝箱の場合
            Debug.Log("宝箱");
        }

        // サーバーに送信する
        await ClientManager.Instance.Send(roundEndData, 4);
    }
}
