using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    GameObject player;

    // スタミナ消費分の数値を決める変数
    public int subStamina;
    
    private void Start()
    {
        player = GameObject.Find("Player");
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
        //player.GetComponent<Player>().SubStamina(subStamina);
    }

    public void Nothing()
    {
        if (player.GetComponent<Player>().isEnd == false)
        {// プレイヤーが移動中の場合
            return;
        }

        // プレイヤーのモードをNOTHINGに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;

        // スタミナを減らす
        player.GetComponent<Player>().SubStamina(subStamina);
    }
}
