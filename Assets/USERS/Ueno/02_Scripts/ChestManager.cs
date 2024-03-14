using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    GameObject textUI;

    // チェストの中身を表示するテキスト
    Text chestText;

    GameObject player;

    private void Start()
    {
        textUI = GameObject.Find("TextUIManager");

        GameObject chestTextObj = GameObject.Find("ChestText");
        chestText = chestTextObj.GetComponent<Text>();

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject == this.gameObject)
            {
                if (ClientManager.Instance.isInsider)
                {
                    textUI.GetComponent<TextUIManager>().OnMouseEnter(10);

                    //if(this.)
                }
            }
            if(hit.transform.gameObject != this.gameObject)
            {
                textUI.GetComponent<TextUIManager>().OnMouseExit(10);
            }
        }
    }
}
