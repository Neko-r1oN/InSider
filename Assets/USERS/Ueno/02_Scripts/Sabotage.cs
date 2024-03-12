using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sabotage : MonoBehaviour
{
    GameObject player;

    ButtonManager button;

    [SerializeField] Text sabotageText;

    // サボタージュ(スロートラップ)のボタン
    [SerializeField] GameObject sabotage3;

    TextUIManager textUI;

    // サボタージュ(埋める)を何回選択したか
    public int fillCount;

    // サボタージュ(爆弾)を何回選択したか
    public int bombCount;

    // サボタージュ(スロートラップ)を何回選択したか
    public int trapCount;

    private void Start()
    {
        // Player
        if (EditorManager.Instance.useServer)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }

        GameObject textUIObj = GameObject.Find("TextUIManager");
        textUI = textUIObj.GetComponent<TextUIManager>();

        GameObject buttonManager = GameObject.Find("ButtonManager");
        button = buttonManager.GetComponent<ButtonManager>();
    }

    /// <summary>
    /// サボタージュ(埋める)モード
    /// </summary>
    public void SabotageFill()
    {
        if(fillCount <= 0)
        {
            // モード変更
            player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEFILL;

            textUI.saboText.SetActive(true);

            // サボタージュボタンを非表示
            button.sabotageUI.SetActive(false);
            // キャンセルボタンを表示
            button.canselButton.SetActive(true);

            fillCount++;
        }
    }

    /// <summary>
    /// サボタージュ(爆弾)モード
    /// </summary>
    public void SabotageBomb()
    {
        if(bombCount <= 0)
        {
            // モード変更
            player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEBOMB;

            textUI.saboText.SetActive(true);

            // サボタージュボタンを非表示
            button.sabotageUI.SetActive(false);
            // キャンセルボタンを表示
            button.canselButton.SetActive(true);

            bombCount++;
        }
    }

    public void SabotageText(int num)
    {
        switch (num)
        {
            case 0:
                sabotageText.text = "切り開いている道を4か所埋めます";
                break;

            case 1:
                sabotageText.text = "道の上に爆弾を3個設置します";
                break;

            case 2:
                sabotageText.text = "選択した場所にスロートラップ(3×3)\n" +
                    "を設置します";
                break;
        }
    }
}
