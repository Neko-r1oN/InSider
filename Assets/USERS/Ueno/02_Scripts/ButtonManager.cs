using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject roadUI; // RoadUIオブジェクトの取得
    [SerializeField] GameObject actionButton; // actionボタンの取得
    [SerializeField] GameObject fillButton; // fillボタンの取得
    [SerializeField] GameObject moveButton; // moveボタンの取得
    [SerializeField] GameObject nothingButton; //nothingボタンの取得
    [SerializeField] GameObject sabotageButton; //sabotageボタンの取得
    int stamina = 100;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void PlayerMove()
    {
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
        stamina -= 10;
        Debug.Log("残りスタミナ" + stamina);
    }

    public void CutOpen()
    {//切り開くを選んだ場合

        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;

        //roadUI.SetActive(true);

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

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;
        stamina -= 10;
        Debug.Log("残りスタミナ" + stamina);
    }

    public void Nothing()
    {
        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;
        stamina -= 10;
        Debug.Log("残りスタミナ" + stamina);
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
}
