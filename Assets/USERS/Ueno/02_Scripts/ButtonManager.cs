using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    GameObject player;
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
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;
        stamina -= 10;
        Debug.Log("残りスタミナ"+ stamina);
    }

    public void fill()
    {//埋めるを選んだ場合
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;
        stamina -= 10;
        Debug.Log("残りスタミナ" + stamina);
    }

    public void Nothing()
    {
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;
        stamina -= 10;
        Debug.Log("残りスタミナ" + stamina);
    }
}
