using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間
    //public int doubtNum = 0;
    Slider timeSlider;

    bool isMyTurn;

    /// <summary>
    /// 今の制限時間
    /// </summary>
    public float nowTime { get; set; }

    // シングルトン用
    public static TimeUI Instance;

    const float timeMax = 20f;

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

    // Start is called before the first frame update
    void Start()
    {
        isMyTurn = false;
        timeSlider = GetComponent<Slider>();
        float maxTime = 0;

        if (EditorManager.Instance.useServer == true)
        {// サーバーを使用する場合
            if(ClientManager.Instance.playerID == ClientManager.Instance.turnPlayerID)
            {// 自分のターンの場合
                maxTime = timeMax;
                nowTime = maxTime;
            }
        }
        else
        {// サーバーを使用しない
            maxTime = 100000000f;
            nowTime = 100000000f;
        }

        //スライダーの最大値の設定
        timeSlider.maxValue = maxTime;

        //スライダーの現在値の設定
        timeSlider.value = nowTime;

        //表示切り替え時間を指定
        _repeatSpan = 1.00f;
        _timeElapsed = 0;
    }

    /// <summary>
    /// 時間制限を生成
    /// </summary>
    /// <param name="doubtNum"></param>
    public void GenerateTimer(int doubtNum)
    {
        isMyTurn = true;

        float maxTime = timeMax;
        nowTime = 20f - 3 * doubtNum;

        //スライダーの最大値の設定
        timeSlider.maxValue = maxTime;

        //スライダーの現在値の設定
        timeSlider.value = nowTime;
    }

    /// <summary>
    /// タイマーを0にする
    /// </summary>
    public void FinishTimer()
    {
        timeSlider.maxValue = 0;
        timeSlider.value = 0;
        nowTime = 0;
    }

    // Update is called once per frame
    async Task Update()
    {
        if (EditorManager.Instance.useServer)
        {
            if (ClientManager.Instance.isGetNotice == false)
            {// ゲーム開始通知を受信していない場合
                return;
            }
        }

        _timeElapsed += Time.deltaTime;     //時間をカウントする

        if (_timeElapsed >= _repeatSpan)
        {//時間経過で時間減少
            //スライダーの現在値の設定
            timeSlider.value -= 1;
            nowTime = timeSlider.value;
            _timeElapsed = 0;   //経過時間をリセットする

            if(nowTime <= 0)
            {// 制限時間が0以下の場合

                if (ClientManager.Instance.playerID == ClientManager.Instance.turnPlayerID)
                {// 現在行動できるプレイヤーのIDが自分自身の場合
                    UpdateTurnsData updateTurnsData = new UpdateTurnsData();

                    // ターンを更新するために送信する
                    await ClientManager.Instance.Send(updateTurnsData, 10);
                }

                // 偽
                isMyTurn = false;
            }
        }
    }
}
