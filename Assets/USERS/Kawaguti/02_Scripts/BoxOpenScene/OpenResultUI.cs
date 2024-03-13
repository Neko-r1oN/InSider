using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる

public class OpenResultUI : MonoBehaviour
{
    [SerializeField] GameObject logo;

    // 通信待機中のテキストとその親
    [SerializeField] GameObject loadingPrefab;
    [SerializeField] GameObject canvasObj;

    // Start is called before the first frame update
    void Start()
    {
        logo = GetComponent<GameObject>();

        //this.transform.DOLocalMove(new Vector3(-387.8f, 286.15f, 0f),5.0f);
        Invoke("move", 17f);
    }

    // Update is called once per frame
    void Update()
    {
        //logo.material.color = Color.Lerp(logo.material.color, new Color(1, 1, 1.0f, 1), 0.350f * Time.deltaTime);
    }

    private void move()
    {
        this.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 3.0f);
        Invoke("SendNotification", 9f);
    }

    private async void SendNotification()
    {
        Debug.Log("現在のラウンド数：" + ClientManager.Instance.roundNum);

        // 通信中のテキストを生成
        GameObject text = Instantiate(loadingPrefab, canvasObj.transform);
        text.transform.localPosition = new Vector3(508, -495, 0);

        // 適当なクラス変数を作成
        ReadyData readyData = new ReadyData();

        // 次のラウンドシーンに遷移する準備ができたことを通知
        await ClientManager.Instance.Send(readyData, 15);
    }
}
