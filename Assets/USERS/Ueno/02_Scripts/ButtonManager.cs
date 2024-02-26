using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ButtonManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject roadUI;         // RoadUIオブジェクトの取得
    [SerializeField] GameObject actionButton;   // actionボタンの取得
    [SerializeField] GameObject fillButton;     // fillボタンの取得
    [SerializeField] GameObject moveButton;     // moveボタンの取得
    [SerializeField] GameObject nothingButton;  // nothingボタンの取得
    [SerializeField] GameObject sabotageButton; // sabotageボタンの取得
    //[SerializeField] GameObject cameraButton;   // カメラ切り替えボタンの取得

    RoadManager roadManager;

    UIManager uIManager;

    CameraManager cameraManager;

    System.Random rnd = new System.Random();

    int stamina = 100;

    int rand;

    // スタミナ消費分の数値を決める変数
    public int subStamina;
    
    private void Start()
    {
        player = GameObject.Find("Player");

        GameObject roadManagerObject = GameObject.Find("RoadManager");

        roadManager = roadManagerObject.GetComponent<RoadManager>();

        GameObject uiManagerObject = GameObject.Find("UIManager");

        uIManager = uiManagerObject.GetComponent<UIManager>();

        GameObject cameraManagerObject = GameObject.Find("CameraManager");

        cameraManager = cameraManagerObject.GetComponent<CameraManager>();

        rand = rnd.Next(0, 31);
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
        {
            // その他のボタンを非表示
            moveButton.SetActive(false);
            fillButton.SetActive(false);
            nothingButton.SetActive(false);
            sabotageButton.SetActive(false);
            actionButton.SetActive(false);
        }
        
        stamina -= 10;
        Debug.Log("残りスタミナ"+ stamina);
    }

    public void fill()
    {//埋めるを選んだ場合

        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        // プレイヤーのモードをFILLに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;
        player.GetComponent<Player>().SubStamina(10);
        stamina -= 10;
        Debug.Log("残りスタミナ" + stamina);
    }

    public void Nothing()
    {
        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        // プレイヤーのモードをNOTHINGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        rand = rnd.Next(0, 31);

        Debug.Log(rand);

        // スタミナを減らす
        player.GetComponent<Player>().AddStamina(rand);
    }

    public void ButtonCancel()
    {
        if(roadUI == true)
        {
            roadUI.SetActive(false);

            // その他のボタンを表示
            moveButton.SetActive(true);
            fillButton.SetActive(true);
            nothingButton.SetActive(true);
            sabotageButton.SetActive(true);
            actionButton.SetActive(true);
        }
    }

    public void DisplayButton()
    {
        // ボタンを表示
        moveButton.SetActive(true);
        fillButton.SetActive(true);
        nothingButton.SetActive(true);
        sabotageButton.SetActive(true);
        actionButton.SetActive(true);
    }

    public void RotRoad()
    {
        roadManager.AddRotButton();
        uIManager.RotRoadUI();
    }

    public void Camera()
    {
        cameraManager.SwitchCamera();
    }
}
