using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;
    [SerializeField] List<GameObject> roadUIList;

    // "○○のターン"のテキスト
    [SerializeField] GameObject turnText;

    // プレイヤーのUI
    [SerializeField] List<GameObject> playerUIList;

    // プレイヤーの名前
    [SerializeField] List<Text> playerName;

    // プレイヤーが途中退出したときのUI
    [SerializeField] List<GameObject> outImageUI;

    // ダウトUIの親
    [SerializeField] List<GameObject> doubtImageParent;

    // ダウトのUI
    GameObject[,] doubtImageUiList = new GameObject[6,6];

    private Quaternion _initialRotation; // 初期回転

    // 情報を取得
    RoadManager roadManager;
    GameObject player;

    public GameObject GetRoadUI()
    {
        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        //// 情報を取得
        //road = GameObject.Find("RoadUI");

        //if (EditorManager.Instance.useServer == true)
        //{// サーバーを使用する場合
        //    player = GameObject.Find("player-List");
        //    player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        //}
        //else
        //{// サーバーを使用しない
        //    player = GameObject.Find("Player1");
        //}

        //GameObject roadManagerObject = GameObject.Find("RoadManager");
        //roadManager = roadManagerObject.GetComponent<RoadManager>();

        //_initialRotation = gameObject.transform.rotation;

        // RoadUIを非表示にする
        road.SetActive(false);

        UdTurnPlayerUI("川口", 2);

        //---------------------
        // ダウトUIを格納する
        //---------------------

        for (int i = 0; i < 6; i++)
        {
            // 子オブジェクトの数を取得
            int childCount = doubtImageParent[i].transform.childCount;

            // 子オブジェクトを順に取得する
            for (int n = 0; n < childCount; n++)
            {
                // 子オブジェクトを取得処理
                Transform childTransform = doubtImageParent[i].transform.GetChild(n);
                GameObject childObject = childTransform.gameObject;

                // 格納する
                doubtImageUiList[i, n] = childObject;

                // 非アクティブにする
                childObject.SetActive(false);
            }
        }

        //------------------------------
        // プレイヤーの名前を更新
        //------------------------------
        //UdPlayerName(ClientManager.Instance.playerNameList);

        //-----------------------------
        // 先行のプレイヤーを表示する
        //-----------------------------
        //int indexNum = ClientManager.Instance.turnPlayerID;
        //UdTurnPlayerUI(ClientManager.Instance.playerNameList[indexNum], indexNum);
    }

    public void ShowRoad(int selectNum)
    {// RoadUIを表示する
        road.SetActive(true);

        if (selectNum >= 0)
        {
            // 前回選んだ道UIを非表示にする
            roadUIList[selectNum].SetActive(false);
        }
    }

    public void HideRoad(int selectNum)
    {
        if(selectNum >= 0)
        {
            // 非表示にしていた道UIを表示
            roadUIList[selectNum].SetActive(true);
        }

        // 道UIを非表示
        road.SetActive(false);

        // プレイヤーモードをMOVEに変更
        player.GetComponent<Player>().mode = Player.PLAYER_MODE.MOVE;
    }

    public bool ActiveRoad()
    {
        // true・falseを返す
        return road.activeSelf;
    }

    public void RotRoadUI()
    {
        for(int i = 0; i < roadUIList.Count; i++)
        {// リストの中身をカウントする

            // リストの全てを回転する
            roadUIList[i].transform.Rotate(0f, 0f, -90f);
        }
    }

    public void ResetRoadUI()
    {
        for (int i = 0; i < roadUIList.Count; i++)
        {// リストの中身をカウントする

            // リストの全てを回転する
            roadUIList[i].transform.rotation = _initialRotation;
        }
    }

    /// <summary>
    /// プレイヤーの名前を更新
    /// </summary>
    private void UdPlayerName(List<string> nameList)
    {
        for (int i = 0; i < playerName.Count; i++)
        {
            if (i >= ClientManager.Instance.playerNameList.Count)
            {// 存在しない場合
                // 破棄する
                Destroy(playerName[i]);
                continue;
            }

            playerName[i].text = nameList[i];
        }
    }

    /// <summary>
    /// ターンテキストの更新(＋プレイヤーUIを動かす)
    /// </summary>
    /// <param name="indexNum"></param>
    public void UdTurnPlayerUI(string name, int indexNum)
    {
        // アクティブ化
        turnText.SetActive(true);

        // テキスト更新
        Transform textTransform = turnText.transform.GetChild(0);   // 子オブジェクトを取得する
        GameObject textObject = textTransform.gameObject;
        textObject.GetComponent<Text>().text = name + "のターン";

        // アニメーション(コルーチン)
        turnText.GetComponent<TurnUI>().StartCoroutine("PanelAnim");

        // プレイヤーUIを動かす
        playerUIList[indexNum].GetComponent<MovePlayerUI>().MoveOrReturn(true,indexNum);     // 動かす

        // プレイヤーUIを元の位置へ戻す
        indexNum = (indexNum + 1 >= playerUIList.Count) ? 0 : indexNum++;   // インデックス番号を調整
        playerUIList[indexNum].GetComponent<MovePlayerUI>().MoveOrReturn(false, indexNum);    // 元の位置へ戻す
    }

    /// <summary>
    /// 途中退出用のUIを表示する
    /// </summary>
    /// <param name="indexNum"></param>
    public void UdOutUI(int indexNum)
    {
        // アクティブ化
        outImageUI[indexNum].SetActive(true);
    }

    /// <summary>
    /// ダウトUIを更新
    /// </summary>
    /// <param name="targetNum"></param>
    /// <param name="indexNum"></param>
    public void UdDoubt(int targetNum,int indexNum)
    {
        // インデックス番号を調整
        indexNum = (indexNum - 1 <= 0) ? 0 : indexNum--;    // 自分自身をダウとすることはできないので画像の種類は５種類

        // アクティブ化する
        doubtImageUiList[targetNum, indexNum].SetActive(true);
    }
}
