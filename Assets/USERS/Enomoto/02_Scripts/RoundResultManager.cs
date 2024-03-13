using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundResultManager : MonoBehaviour
{
    [SerializeField] List<GameObject> playerUIList;
    [SerializeField] List<Text> playerNameList;
    [SerializeField] List<Text> totalScoreList;
    [SerializeField] List<Text> allieScoreList;
    [SerializeField] List<GameObject> insiderLogoList;

    // 通信待機中用UI
    [SerializeField] GameObject loadingPrefab;
    [SerializeField] GameObject canvasObj;

    bool isClick;

    // シングルトン用
    public static RoundResultManager Instance;

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
        isClick = false;
    }

    private void Update()
    {
        if(isClick == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            isClick = true;

            // 通信中のテキストを生成
            GameObject text = Instantiate(loadingPrefab, canvasObj.transform);
            text.transform.localPosition = new Vector3(508, -507, 0);

            Invoke("SendNotification", 5f);
        }
    }

    private async void SendNotification()
    {
        Debug.Log("現在のラウンド数：" + ClientManager.Instance.roundNum);

        // 適当なクラス変数を作成
        ReadyData readyData = new ReadyData();

        // 次のラウンドシーンに遷移する準備ができたことを通知
        await ClientManager.Instance.Send(readyData, 15);
    }

    public void SetUI(List<int> totalScore, List<int> allieScore, List<int> insiderID)
    {
        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i >= totalScore.Count)
            {// 存在しないプレイヤーを破棄する
                Destroy(playerUIList[i]);
                continue;
            }

            totalScoreList[i].text = "" + totalScore[i];
            allieScoreList[i].text = "(+" + allieScore[i] + ")";
        }

        for (int i = 0; i < insiderID.Count; i++)
        {
            insiderLogoList[insiderID[i]].SetActive(true);    // Insiderロゴマーク
        }
    }
}
