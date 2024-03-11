using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlayer : MonoBehaviour
{
    Animator animator;
    private OpenBox openBox;

    //[SerializeField] AudioClip Oh;

    private float _repeatSpan;    //�J��Ԃ��Ԋu
    private float _timeElapsed;   //�o�ߎ���

    bool Once;
    bool Mimic;
    public AudioSource damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<AudioSource>();
        //�\���؂�ւ����Ԃ��w��
        _repeatSpan = 4.5f;
        _timeElapsed = 0;

        Mimic = true;
        Once = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        _timeElapsed += Time.deltaTime;     //���Ԃ��J�E���g����

      
        if (_timeElapsed >= _repeatSpan + 0.5f && !Once)
        {//���Ԍo�߂Ńe�L�X�g�\��
            _repeatSpan = 0;
            Anim();
            damage.Play();
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
