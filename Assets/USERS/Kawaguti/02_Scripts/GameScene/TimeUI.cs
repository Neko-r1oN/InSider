using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間
    public int doubtNum = 0;
    Slider timeSlider;

    // Start is called before the first frame update
    void Start()
    {

        timeSlider = GetComponent<Slider>();
        float maxHp = 30;
        float nowHp = 30f - 5 * doubtNum;


        //スライダーの最大値の設定
        timeSlider.maxValue = maxHp;

        //スライダーの現在値の設定
        timeSlider.value = nowHp;


        //表示切り替え時間を指定
        _repeatSpan = 1.00f;
        _timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;     //時間をカウントする

        if (_timeElapsed >= _repeatSpan)
        {//時間経過でスタミナ減少
         //スライダーの現在値の設定
            timeSlider.value -= 1;
            _timeElapsed = 0;   //経過時間をリセットする
        }
    }
}
