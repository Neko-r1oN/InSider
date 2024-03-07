using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabotage : MonoBehaviour
{
    GameObject player;

    ButtonManager button;

    private void Start()
    {
        player = GameObject.Find("Player1");

        GameObject buttonManager = GameObject.Find("ButtonManager");
        button = buttonManager.GetComponent<ButtonManager>();
    }

    public void EventFill()
    {
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEFILL;
        button.sabotage.SetActive(false);
        button.canselButton.SetActive(true);
    }
}
