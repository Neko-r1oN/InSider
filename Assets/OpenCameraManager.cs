using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCameraManager : MonoBehaviour
{
    private GameObject Camera1;
    private GameObject Camera2;


    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    

    // Start is called before the first frame update
    void Start()
    {


        Camera1.SetActive(true);
        Camera2.SetActive(true);

        //表示切り替え時間を指定
        _repeatSpan = 2.0f;
        _timeElapsed = 0;

       
    }

    // Update is called once per frame
    void Update()
    {

        _timeElapsed += Time.deltaTime;     //時間をカウントする


        if (_timeElapsed + 0.0f >= _repeatSpan)
        {//時間経過でカメラ変更
            Camera1.SetActive(false);
        }
        if (_timeElapsed + 2.0f >= _repeatSpan)
        {
            Camera2.SetActive(false);

        }


    }

}
