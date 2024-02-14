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
        Debug.Log("�c��X�^�~�i" + stamina);
    }

    public void CutOpen()
    {//�؂�J����I�񂾏ꍇ
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MINING;
        stamina -= 10;
        Debug.Log("�c��X�^�~�i"+ stamina);
    }

    public void fill()
    {//���߂��I�񂾏ꍇ
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.FILL;
        stamina -= 10;
        Debug.Log("�c��X�^�~�i" + stamina);
    }

    public void Nothing()
    {
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.NOTHING;
        stamina -= 10;
        Debug.Log("�c��X�^�~�i" + stamina);
    }
}
