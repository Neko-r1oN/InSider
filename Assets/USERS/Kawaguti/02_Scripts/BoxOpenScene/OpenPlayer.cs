using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlayer : MonoBehaviour
{
    Animator animator;
    private OpenBox openBox;
    
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    bool Once;
    bool Mimic;

    // Start is called before the first frame update
    void Start()
    {
        //表示切り替え時間を指定
        _repeatSpan = 3.0f;
        _timeElapsed = 0;

        Mimic = false;
        Once = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        _timeElapsed += Time.deltaTime;     //時間をカウントする

      
        if (_timeElapsed+1.0f >= _repeatSpan && !Once)
        {//時間経過でテキスト表示
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
