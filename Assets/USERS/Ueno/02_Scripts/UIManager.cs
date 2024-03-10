using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject road;
    [SerializeField] List<GameObject> roadUIList;

    // 残りターン表示のテキスト
    [SerializeField] GameObject remainingTurnsText;

    // 現在のラウンド数を表示
    [SerializeField] GameObject roundCounterText;

    // "○○のターン"のテキスト
    [SerializeField] GameObject turnText;

    // プレイヤーのUI
    [SerializeField] List<GameObject> playerUIList;

    // プレイヤーの名前
    [SerializeField] List<GameObject> playerName;
    [SerializeField] List<GameObject> scorePlayerName;  // スコア表示の方のプレイヤーName

    // スコア表示の方のプレイヤーUI
    [SerializeField] List<GameObject> scorePlayerUIList;

    // スコアのテキスト
    [SerializeField] List<Text> scoreText;

    // プレイヤーが途中退出したときのUI
    [SerializeField] List<GameObject> outImageUI;

    // ダウトUIの親
    [SerializeField] List<GameObject> doubtImageParent;

    // ダウトのUI
    GameObject[,] doubtImageUiList = new GameObject[6,6];

    // ダウトのボタンのリスト
    [SerializeField] List<GameObject> doubtButtonList;

    // ダウトのボタン用の途中退出したときのUI
    [SerializeField] List<GameObject> outImageList_DoubtUI;

    // 使用不可にするダウトのボタンのインデックス番号
    public List<int> disabledIndexNumList;

    private Quaternion _initialRotation; // 初期回転

    // 連続選択ができないよう前回の選択した数値を保存
    public int selectRoadNum;

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
        // 情報を取得
        road = GameObject.Find("RoadUI");

        // newする
        disabledIndexNumList = new List<int>();

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            player = GameObject.Find("player-List");
            player = player.GetComponent<PlayerManager>().players[ClientManager.Instance.playerID];
        }
        else
        {// サーバーを使用しない
            player = GameObject.Find("Player1");
        }
        
        // ロードマネージャーの情報を格納
        GameObject roadManagerObject = GameObject.Find("RoadManager");
        roadManager = roadManagerObject.GetComponent<RoadManager>();

        // 回転の初期値を格納
        _initialRotation = gameObject.transform.rotation;

        // RoadUIを非表示にする
        road.SetActive(false);

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

        if (EditorManager.Instance.useServer == true)
        {
            //-------------------------------------------
            // 現在のラウンド数と残りのターン数を更新
            //-------------------------------------------
            roundCounterText.GetComponent<Text>().text = "" + ClientManager.Instance.roundNum;
            remainingTurnsText.GetComponent<Text>().text = "" + ClientManager.Instance.turnMaxNum;

            //-----------------------------
            // スコアのテキストを更新する
            //-----------------------------
            for (int i = 0; i < ClientManager.Instance.scoreList.Count; i++)
            {
                // 各スコアのテキスト内容を更新する
                UdScoreText(i, ClientManager.Instance.scoreList[i]);
            }

            //------------------------------
            // プレイヤーの名前を更新
            //------------------------------
            UdPlayerName(ClientManager.Instance.playerNameList);

            //-----------------------------
            // 先行のプレイヤーを表示する
            //-----------------------------
            int indexNum = ClientManager.Instance.turnPlayerID;
            UdTurnPlayerUI(ClientManager.Instance.playerNameList[indexNum], indexNum);

            //-----------------------------
            // プレイヤーUIを動かす
            //-----------------------------
            playerUIList[ClientManager.Instance.turnPlayerID].GetComponent<MovePlayerUI>().MoveOrReturn(true);
        }

        selectRoadNum = -1;
    }

    /// <summary>
    /// 前回選択された道UIの非表示処理
    /// </summary>
    /// <param name="selectNum"></param>
    public void ShowRoad(int selectNum)
    {
        // RoadUIを表示する
        road.SetActive(true);

        // 引数で来た値が0以上なら
        if (selectNum >= 0)
        {
            // 前回選んだ道UIを非表示にする
            roadUIList[selectNum].SetActive(false);
        }
    }

    /// <summary>
    /// 前回選択した道UIの表示処理
    /// </summary>
    /// <param name="selectNum"></param>
    public void HideRoad(int selectNum)
    {
        // 引数で来た値が0以上なら
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

    /// <summary>
    /// 道UIがtrue・falseかを返す処理
    /// </summary>
    /// <returns></returns>
    public bool ActiveRoad()
    {
        // true・falseを返す
        return road.activeSelf;
    }

    /// <summary>
    /// 道UIの回転処理
    /// </summary>
    public void RotRoadUI()
    {
        for(int i = 0; i < roadUIList.Count; i++)
        {// リストの中身をカウントする

            // リストの全てを回転する
            roadUIList[i].transform.Rotate(0f, 0f, -90f);
        }
    }

    /// <summary>
    /// 道UIの回転を元に戻す処理
    /// </summary>
    public void ResetRoadUI()
    {
        for (int i = 0; i < roadUIList.Count; i++)
        {// リストの中身をカウントする

            // リストの全てを回転を元に戻す
            roadUIList[i].transform.rotation = _initialRotation;
        }
    }

    /// <summary>
    /// プレイヤーの名前を更新 && 存在しない分のUIを削除
    /// </summary>
    private void UdPlayerName(List<string> nameList)
    {
        for (int i = 0; i < playerName.Count; i++)
        {
            if (i >= ClientManager.Instance.playerNameList.Count)
            {// 存在しない場合

                // 破棄する
                Destroy(playerUIList[i]);
                Destroy(scorePlayerUIList[i]);
                Destroy(doubtButtonList[i]);
                Destroy(outImageList_DoubtUI[i]);

                Debug.Log("破棄する" + i);

                continue;
            }

            // 常に表示される方の名前UI
            playerName[i].GetComponent<Text>().text = nameList[i];

            // スコア表示の方の名前UI
            scorePlayerName[i].GetComponent<Text>().text = nameList[i];
        }

        Destroy(doubtButtonList[ClientManager.Instance.playerID]);
    }

    /// <summary>
    /// 残りターンテキスト更新する
    /// </summary>
    /// <param name="turnNum"></param>
    public void UdRemainingTurns(int turnNum)
    {
        remainingTurnsText.GetComponent<Text>().text = "" + turnNum;
    }

    /// <summary>
    /// スコアのテキストを更新する
    /// </summary>
    /// <param name="indexNum"></param>
    /// <param name="score"></param>
    public void UdScoreText(int indexNum,int score)
    {
        scoreText[indexNum].text = "" + score;
    }

    /// <summary>
    /// プレイヤーUIの位置を戻す
    /// </summary>
    /// <param name="indexNum"></param>
    public void ReturnPlayerUI(int indexNum)
    {
        //呪文 [座標を正規化するため]
        Canvas.ForceUpdateCanvases();

        // 元の位置へ戻す
        Vector3 pos = playerUIList[indexNum].transform.localPosition;
        playerUIList[indexNum].transform.localPosition = new Vector3(-34.3f, pos.y, pos.z);
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
        playerUIList[indexNum].GetComponent<MovePlayerUI>().MoveOrReturn(true);     // 動かす

        // 前回動かしたプレイヤーUIを元の位置へ戻す
        if (indexNum == 0)
        {
            // 元の位置へ戻す
            playerUIList[ClientManager.Instance.playerNameList.Count - 1].GetComponent<MovePlayerUI>().MoveOrReturn(false);
        }
        else
        {
            // 元の位置へ戻す
            playerUIList[indexNum - 1].GetComponent<MovePlayerUI>().MoveOrReturn(false);
        }
    }

    /// <summary>
    /// 途中退出用のUIを表示する
    /// </summary>
    /// <param name="indexNum"></param>
    public void UdOutUI(int indexNum)
    {
        // アクティブ化
        outImageUI[indexNum].SetActive(true);
        outImageList_DoubtUI[indexNum].SetActive(true);
    }

    /// <summary>
    /// ダウトUIを更新
    /// </summary>
    /// <param name="targetNum"></param>
    /// <param name="indexNum"></param>
    public void UdDoubt(int targetID,int playerID)
    {
        // インデックス番号を調整
        playerID = (playerID - 1 <= 0) ? 0 : playerID--;    // 画像の種類は５種類のため調整

        // アクティブ化する
        doubtImageUiList[targetID, playerID].SetActive(true);
    }

    /// <summary>
    /// UIリストから要素を削除する
    /// </summary>
    /// <param name="indexNum"></param>
    public void RemoveElement(int indexNum)
    {
        playerUIList.RemoveAt(indexNum);
        playerName.RemoveAt(indexNum);
        outImageUI.RemoveAt(indexNum);
        doubtButtonList.RemoveAt(indexNum);

        // 使用できないインデックス番号を追加
        disabledIndexNumList.Add(indexNum);
    }
}
