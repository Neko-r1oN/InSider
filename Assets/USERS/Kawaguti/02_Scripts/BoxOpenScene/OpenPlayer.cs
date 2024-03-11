using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlayer : MonoBehaviour
{
    Animator animator;
    private OpenBox openBox;
    
    private float _repeatSpan;    //�J��Ԃ��Ԋu
    private float _timeElapsed;   //�o�ߎ���

    bool Once;
    bool Mimic;

    // Start is called before the first frame update
    void Start()
    {
        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 2.0f;
        _timeElapsed = 0;

        Mimic = true;
        Once = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����

      
        if (_timeElapsed >= _repeatSpan + 4.0f && !Once)
        {//���Ԍo�߂Ńe�L�X�g�\��
            Anim();
            Once = true;
        }


    }
    private void Anim()
    {
        if (!Mimic)
        {
            animator.SetTrigger("isYorokobu");
        }
        else if (Mimic)
        {
            animator.SetTrigger("isDamage");
        }
    }
}
