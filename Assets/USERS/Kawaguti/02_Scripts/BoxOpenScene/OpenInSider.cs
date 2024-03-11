using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInSider : MonoBehaviour
{
    Animator animator;
   

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


        if (_timeElapsed >= _repeatSpan + 4.5f && !Once)
        {//���Ԍo�߂ŃA�j���[�V����
            Anim();
            Once = true;
        }


    }
    private void Anim()
    {
        if (Mimic)
        {
            animator.SetTrigger("isYorokobu");
        }
        else if (!Mimic)
        {
            animator.SetTrigger("isStand");
        }
    }
}
