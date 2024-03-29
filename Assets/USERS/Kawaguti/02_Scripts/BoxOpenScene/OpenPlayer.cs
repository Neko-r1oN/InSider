using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPlayer : MonoBehaviour
{
    Animator animator;
    private OpenBox openBox;

    //[SerializeField] AudioClip Oh;

    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    bool Once;
    bool Mimic;
    public AudioSource damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<AudioSource>();
        //表示切り替え時間を指定
        _repeatSpan = 4.5f;
        _timeElapsed = 0;

        Mimic = OpenManager.Instance.isMimic;
        Once = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        _timeElapsed += Time.deltaTime;     //時間をカウントする

      
        if (_timeElapsed >= _repeatSpan + 0.5f && !Once)
        {//時間経過でテキスト表示
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
