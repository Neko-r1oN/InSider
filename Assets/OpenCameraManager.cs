using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCameraManager : MonoBehaviour
{
    private GameObject Camera1;
    private GameObject Camera2;


    private float _repeatSpan;    //�J��Ԃ��Ԋu
    private float _timeElapsed;   //�o�ߎ���

    

    // Start is called before the first frame update
    void Start()
    {


        Camera1.SetActive(true);
        Camera2.SetActive(true);

        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 2.0f;
        _timeElapsed = 0;

       
    }

    // Update is called once per frame
    void Update()
    {

        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����


        if (_timeElapsed + 0.0f >= _repeatSpan)
        {//���Ԍo�߂ŃJ�����ύX
            Camera1.SetActive(false);
        }
        if (_timeElapsed + 2.0f >= _repeatSpan)
        {
            Camera2.SetActive(false);

        }


    }

}
