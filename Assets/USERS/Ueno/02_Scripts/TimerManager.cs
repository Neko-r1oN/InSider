using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    // タイマーが止まってるかどうか
    bool isTimerStop;
    // 秒数
    private float seconds;
    //前のupdateの時の秒数
    private float oldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        // 40秒に設定
        seconds = 40f;
        oldSeconds = 40f;
        isTimerStop = false;
    }

    public float Seconds()
    {
        return seconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (EditorManager.Instance.useServer)
        {
            if (ClientManager.Instance.isGetNotice == false)
            {// ゲーム開始通知を受信していない場合
                return;
            }
        }

        if (isTimerStop == false)
        {
            seconds -= Time.deltaTime;

            if (seconds <= 0)
            {
                seconds = 0;

                TimerStop();
            }
        }

        oldSeconds = seconds;
    }

    public void TimerStop()
    {
        isTimerStop = true;

        Debug.Log("終了");
    }
}
