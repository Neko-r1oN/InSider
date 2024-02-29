using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject roadUI;         // RoadUIオブジェクトの取得
    [SerializeField] GameObject actionButton;   // actionボタンの取得
    [SerializeField] GameObject fillButton;     // fillボタンの取得
    [SerializeField] GameObject moveButton;     // moveボタンの取得
    [SerializeField] GameObject nothingButton;  // nothingボタンの取得
    [SerializeField] GameObject sabotageButton; // sabotageボタンの取得

    // 情報を取得
    GameObject player;
    RoadManager roadManager;
    UIManager uIManager;
    CameraManager cameraManager;

    // ランダム関数
    System.Random rnd = new System.Random();

    // スタミナ
    int stamina = 100;

    // ランダムの数値を入れるための変数
    int rand;

    // スタミナ消費分の数値を決める変数
    public int subStamina;
    
    private void Start()
    {
        // 情報を取得
        player = GameObject.Find("Player1");

        GameObject roadManagerObject = GameObject.Find("RoadManager");

        roadManager = roadManagerObject.GetComponent<RoadManager>();

        GameObject uiManagerObject = GameObject.Find("UIManager");

        uIManager = uiManagerObject.GetComponent<UIManager>();

        GameObject cameraManagerObject = GameObject.Find("CameraManager");

        cameraManager = cameraManagerObject.GetComponent<CameraManager>();

        // プレイヤーのモードをNOTHINGに設定
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        if (player.tag == "Pioneer")
        {// タグがPioneerならサボタージュボタンを非表示
            sabotageButton.SetActive(false);
        }
        else if (player.tag == "Secrecy")
        {// タグがSecrecyならサボタージュボタンを表示
            sabotageButton.SetActive(true);
        }
    }

    public void PlayerMove()
    {
        // プレイヤーのモードをMOVEに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    public void CutOpen()
    {//切り開くを選んだ場合

        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        // プレイヤーのモードをMININGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;

        if (roadUI == true)
        {// RoadUIが表示されていたら

            // その他のボタンを非表示
            moveButton.SetActive(false);
            fillButton.SetActive(false);
            nothingButton.SetActive(false);
            sabotageButton.SetActive(false);
            actionButton.SetActive(false);
        }
    }

    public void fill()
    {//埋めるを選んだ場合

        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        // プレイヤーのモードをFILLに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;
        // スタミナを減らす
        player.GetComponent<Player>().SubStamina(10);
        Debug.Log("残りスタミナ" + stamina);
    }

    public void Nothing()
    {// 何もしないを選んだ場合
        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        // プレイヤーのモードをNOTHINGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        rand = rnd.Next(0, 71); // 0～30までのランダムの数値

        // スタミナを増やす
        player.GetComponent<Player>().AddStamina(rand);
    }

    public void ButtonCancel()
    {// キャンセルボタンを選んだ場合
        if (roadUI == true)
        {// RoadUIが表示されていたら
            roadUI.SetActive(false);

            // その他のボタンを表示
            moveButton.SetActive(true);
            fillButton.SetActive(true);
            nothingButton.SetActive(true);
            actionButton.SetActive(true);
            
            if (player.tag == "Secrecy")
            {// タグがSecrecyなら表示
                sabotageButton.SetActive(true);
            }
        }
    }

    public void DisplayButton()
    {
        // ボタンを表示
        moveButton.SetActive(true);
        fillButton.SetActive(true);
        nothingButton.SetActive(true);
        actionButton.SetActive(true);
       
        if (player.tag == "Secrecy")
        {// タグがSecrecyなら表示
            sabotageButton.SetActive(true);
        }
    }

    public void RotRoad()
    {// 道・道UIを回転
        roadManager.AddRotButton();
        uIManager.RotRoadUI();
    }

    public void Camera()
    {// カメラ切り替え
        cameraManager.SwitchCamera();
    }
}
