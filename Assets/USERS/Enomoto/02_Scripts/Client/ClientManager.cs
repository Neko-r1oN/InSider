using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;           // UI
using Newtonsoft.Json;          // JSONのデシリアライズなど
using System.Net.Sockets;       // NetworkStreamなど
using System.Threading.Tasks;   // スレッドなど
using System.Threading;         // スレッドなど
using System.Linq;              // Skipメソッドなど
using System.Text;
using System;

public class connectManager : MonoBehaviour
{
    // メインスレッドに処理実行を依頼するもの
    SynchronizationContext context;

    // NetworkStreamを使用
    NetworkStream stream;

    // クライアント作成
    TcpClient tcpClient;

    // 準備完了(OK)ボタンを押したかどうか
    bool isReadyButton = false;

    // 接続を終了するかどうか
    bool isconnect = false;

    // クライアントの名前
    string clientName;

    // PlayerNameテキストリスト
    public List<GameObject> clientTextList;

    // 準備完了テキストのリスト
    public List<GameObject> readyTextList;

    // リスナーデータリスト
    List<ListenerData> listenerList;

    // ボタンリスト
    [SerializeField] List<GameObject> buttonObjList;

    /// <summary>
    /// イベントID
    /// </summary>
    public enum EventID
    {
        ListenerList = 1,     // リスナーデータ
        ReadyData,            // 準備完了
        GameStart,            // ゲーム開始
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        tcpClient = new TcpClient();
        clientTextList = new List<GameObject>();
        readyTextList = new List<GameObject>();
        listenerList = new List<ListenerData>();
        context = SynchronizationContext.Current;

        Debug.Log("総数"+clientTextList.Count);

        // 取得する
        clientName = TitleManager.UserName;

        // 非同期処理を実行する
        StartConnect();
    }

    /// <summary>
    /// 準備完了(OK)ボタンを押した
    /// </summary>
    public async void ReadyButton()
    {
        ReadyData readyData = new ReadyData();

        readyData.name = clientName;
        readyData.isReady = isReadyButton;

        // 準備完了待ちを送信
        await Send(readyData, tcpClient, 2);
        
        // フラグを切り替え
        isReadyButton = !isReadyButton;

        // 表示・非表示切り替え
        buttonObjList[0].SetActive(isReadyButton);
        buttonObjList[1].SetActive(!isReadyButton);
    }

    /// <summary>
    /// 受信処理(スレッド)
    /// </summary>
    /// <param name="arg"></param>
    async void RecvProc(object arg)
    {
        // クライアント作成
        TcpClient tcpClient = (TcpClient)arg;

        // NetworkStreamを使用
        stream = tcpClient.GetStream();

        while (true)
        {
            // 受信待機する
            byte[] receiveBuffer = new byte[1024];
            int length = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);  // 受信する

            // 接続切断チェック
            if (isconnect == true)
            {
                break;
            }

            // 受信データからイベントIDを取り出す
            int eventID = receiveBuffer[0];

            // 受信データからJSON文字列を取り出す
            byte[] bufferJson = receiveBuffer.Skip(1).ToArray();    // １バイト目をスキップ
            string jsonString = System.Text.Encoding.UTF8.GetString(bufferJson, 0, length - 1);  // 受信したものをbyteからstringに変換

            //*************************************
            //  メインスレッドに処理実行を依頼
            //*************************************

            context.Post(_ =>
            {
                switch (eventID)
                {
                    case 1: // イベントIDが１の処理実行

                        Debug.Log("プレイヤー情報を受信した");

                        // JSONデシリアライズで取得する
                        listenerList = JsonConvert.DeserializeObject<List<ListenerData>>(jsonString);

                        // プレイヤーの名前を反映
                        for (int i = 0; i < clientTextList.Count; i++)
                        {
                            if (listenerList.Count > i)
                            {// インデックスが範囲内の場合
                                // Textを更新する
                                clientTextList[i].GetComponent<Text>().text = listenerList[i].name;
                            }
                            else
                            {// 初期化する
                                // Textを更新する
                                clientTextList[i].GetComponent<Text>().text = (i + 1) + "";
                            }
                        }

                        break;
                    case 2: // イベントIDが２の処理実行

                        Debug.Log("準備完了|準備待ち通知を受信");

                        List<ReadyData> readyData = JsonConvert.DeserializeObject<List<ReadyData>>(jsonString);

                        for (int cnt = 0; cnt < clientTextList.Count; cnt++)
                        {
                            for (int i = 0; i < readyData.Count; i++)
                            {
                                // 変更点：clientTextListにClientスクリプトを持たせていた

                                if (clientTextList[cnt].GetComponent<Text>().text == readyData[i].name)
                                {// 名前が一致した
                                    if (readyData[i].isReady == true)
                                    {// 真
                                        readyTextList[cnt].SetActive(true);
                                    }
                                    else
                                    {// 偽
                                        readyTextList[cnt].SetActive(false);
                                    }

                                    // 一つ上のループをぬける
                                    break;
                                }
                                else
                                {// 偽
                                    readyTextList[cnt].SetActive(false);
                                }
                            }
                        }

                        break;
                    case 3:
                        // アクティブ化
                        
                        break;
                }
            }, null);
        }

        // 接続を切断
        tcpClient.Close();

        tcpClient = null;
    }

    /// <summary>
    /// 接続処理 && スレッド起動
    /// </summary>
    private async void StartConnect()
    {
        // 自身の情報を取得
        ListenerData listener = new ListenerData(); // インスタンス化
        listener.name = clientName;  // 自身の名前を代入

        // 自身の準備完了データを取得
        ReadyData readyData = new ReadyData();
        readyData.name = clientName;
        readyData.isReady = false;

        try
        {
            //**********************************
            //  接続の要求 & 通信の確立
            //**********************************

            // 送受信のタイムアウトを設定(msec)
            tcpClient.SendTimeout = 1000;       // 送信
            tcpClient.ReceiveTimeout = 1000;    // 受信

            // サーバーへ接続要求
            await tcpClient.ConnectAsync("127.0.0.1", 20000);
            Debug.Log("***サーバーと通信確立***");

            //**********************************
            //  自身の情報を送信する
            //**********************************

            // 送信する
            await Send(listener, tcpClient, 1);

            //**********************************
            //  準備完了データを送信する
            //**********************************

            // 送受信する
            ReadyButton();
            //await Send(readyData, tcpClient, 2);

            //**********************************
            //  受信用のスレッドを起動
            //**********************************

            Thread thread = new Thread(new ParameterizedThreadStart(RecvProc));
            thread.Start(tcpClient);
        }
        catch (Exception ex)
        {
            Debug.Log("接続できない。後でまた試して");
            Debug.Log(ex);
        }
    }

    /// <summary>
    /// 送信する
    /// </summary>
    /// <param name="param"></param>
    /// <param name="arg"></param>
    public async Task Send(object data, object arg, int eventID)
    {
        // 引数からクライアントをキャストで取得する
        TcpClient tcpClientList = (TcpClient)arg;

        // JSONシリアライズ
        string json = JsonConvert.SerializeObject(data);  // クラス型の変数を指定

        // Json文字列をbyte配列に変換
        byte[] sendData = Encoding.UTF8.GetBytes(json);

        // 送信データの先頭にイベントIDを追加する
        sendData = sendData.Prepend((byte)eventID).ToArray();    // ※必ずIDを確認すること!!!!!!!!!!

        // 送信データの配列数を固定に変更
        Array.Resize(ref sendData, 1024);   // サイズを固定して安定に送受信できるようにする

        // 送信処理
        NetworkStream stream = tcpClientList.GetStream();
        await stream.WriteAsync(sendData, 0, sendData.Length);  // 送信
    }

    private void OnDestroy()
    {
        if (tcpClient != null)
        {
            // 接続を切断
            tcpClient.Close();
        }
    }
}
