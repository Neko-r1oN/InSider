using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    // ƒ^ƒCƒ}[‚ª~‚Ü‚Á‚Ä‚é‚©‚Ç‚¤‚©
    bool isTimerStop;
    // •b”
    private float seconds;
    //‘O‚Ìupdate‚Ì‚Ì•b”
    private float oldSeconds;

    // Start is called before the first frame update
    void Start()
    {
        // 40•b‚Éİ’è
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

        Debug.Log("I—¹");
    }
}
