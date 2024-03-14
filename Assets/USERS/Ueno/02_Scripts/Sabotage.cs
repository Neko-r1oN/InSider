using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sabotage : MonoBehaviour
{
    GameObject player;

    ButtonManager button;

    [SerializeField] Text sabotageText;
    TextUIManager textUI;

    [SerializeField] Text timeText;

    GameObject parent;

    // サボタージュ(埋める)を何回選択したか
    public bool isFill;

    // サボタージュ(爆弾)を何回選択したか
    public bool isBomb;

    // サボタージュ(スロートラップ)を何回選択したか
    public bool isTrap;

    public int timeNum;

    // シングルトン用
    public static Sabotage Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


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

        //parent = this.transform.parent.gameObject;

        if(timeNum > 0)
        {
            InvokeRepeating("SubCoolTime", 0, 1);

            Debug.Log("ee");
            //ResetCoolTime();
        }

        isFill = false;
        isBomb = false;
        isTrap = false;
    }

    /// <summary>
    /// サボタージュ(埋める)モード
    /// </summary>
    public void SabotageFill()
    {
        if(timeNum <= 0)
        {
            if (isFill == false)
            {
                // モード変更
                player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEFILL;

                textUI.saboText.SetActive(true);

                // サボタージュボタンを非表示
                button.sabotageUI.SetActive(false);
                // キャンセルボタンを表示
                button.canselButton.SetActive(true);
            }
        }
    }

    /// <summary>
    /// サボタージュ(爆弾)モード
    /// </summary>
    public void SabotageBomb()
    {
        if (timeNum <= 0)
        {
            if (isBomb == false)
            {
                // モード変更
                player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGEBOMB;

                textUI.saboText.SetActive(true);

                // サボタージュボタンを非表示
                button.sabotageUI.SetActive(false);
                // キャンセルボタンを表示
                button.canselButton.SetActive(true);
            }
        }
            
    }

    /// <summary>
    /// サボタージュ(スロートラップ)モード
    /// </summary>
    public void SbotageTrap()
    {
        if (timeNum <= 0)
        {
            if (isTrap == false)
            {
                // モード変更
                player.GetComponent<Player>().mode = Player.PLAYER_MODE.SABOTAGETRAP;

                // サボタージュボタンを非表示
                button.sabotageUI.SetActive(false);
                // キャンセルボタンを表示
                button.canselButton.SetActive(true);
            }
        }
    }

    /// <summary>
    /// boolを戻す
    /// </summary>
    public void ResetBool()
    {
        if(player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEFILL)
        {
            isFill = false;
        }
        else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGEBOMB)
        {
            isBomb = false;
        }
        else if (player.GetComponent<Player>().mode == Player.PLAYER_MODE.SABOTAGETRAP)
        {
            isTrap = false;
        }
    }

    /// <summary>
    /// サボタージュのクールタイム処理
    /// </summary>
    /// <returns></returns>
    public void SubCoolTime()
    {
        timeNum--;

        if(timeText != null)
        {
            timeText.text = "" + timeNum;
        }
        
        if (timeNum <= 0)
        {
            button.sabotageCoolTime.SetActive(false);
            CancelInvoke("SubCoolTime");
        }
    }

    /// <summary>
    /// クールタイムを戻す
    /// </summary>
    public void ResetCoolTime()
    {
        timeNum = 60;

        InvokeRepeating("SubCoolTime", 0, 1);
    }

    /// <summary>
    ///  サボタージュの説明文を表示する処理
    /// </summary>
    /// <param name="num"></param>
    public void SabotageText(int num)
    {
        switch (num)
        {
            case 0:
                sabotageText.text = "切り開いている道を4か所埋めます";
                break;

            case 1:
                sabotageText.text = "道の上に爆弾を2個設置します";
                break;

            case 2:
                sabotageText.text = "選択した場所にスロートラップ(3×3)\n" +
                    "を設置します";
                break;
        }
    }
}
