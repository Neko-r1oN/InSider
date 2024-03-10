using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sabotage : MonoBehaviour
{
    GameObject player;

    ButtonManager button;

    Text sabotageText;

    private void Awake()
    {
        sabotageText = GameObject.Find("SabotageText").GetComponent<Text>();
    }

    private void Start()
    {
        player = GameObject.Find("Player1");

        GameObject buttonManager = GameObject.Find("ButtonManager");
        button = buttonManager.GetComponent<ButtonManager>();
    }

    /// <summary>
    /// サボタージュ(埋める)モード
    /// </summary>
    public void SabotageFill()
    {
        // モード変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEFILL;
        // サボタージュボタンを非表示
        button.sabotage.SetActive(false);
        // キャンセルボタンを表示
        button.canselButton.SetActive(true);
    }

    /// <summary>
    /// サボタージュ(爆弾)モード
    /// </summary>
    public void SabotageBomb()
    {
        // モード変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEBOMB;
        // サボタージュボタンを非表示
        button.sabotage.SetActive(false);
        // キャンセルボタンを表示
        button.canselButton.SetActive(true);
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
