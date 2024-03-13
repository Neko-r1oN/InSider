using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    Animator animator;

    [SerializeField] GameObject Mimic;
    [SerializeField] GameObject Chest;
    [SerializeField] GameObject Gold;

    public AudioSource eat;

    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    bool Once;
    public bool isMimic;

    // Start is called before the first frame update
    void Start()
    {
        eat = GetComponent<AudioSource>();
        //表示切り替え時間を指定
        _repeatSpan = 2.0f;
        _timeElapsed = 0;

        isMimic = OpenManager.Instance.isMimic;
        Once = false;
        Gold.SetActive(false);
        animator = GetComponent<Animator>();

       
    }

    // Update is called once per frame
    void Update()
    {
        _timeElapsed += Time.deltaTime;     //時間をカウントする

        //if (!isMimic)
        //{
        //    Mimic.SetActive(false);
        //    Chest.SetActive(true);
        //}
        //else if (isMimic)
        //{
        //    Mimic.SetActive(true);
        //    Chest.SetActive(false);
        //}

        if (_timeElapsed  >= _repeatSpan +3.0f && !Once)
        {//時間経過でアニメーション
            Anim();
            eat.Play();
            Once = true;
        }


    }
    private void Anim()
    {
        if (!isMimic)
        {
            animator.SetTrigger("isOpen");
            Gold.SetActive(true);
        }
        else if (isMimic)
        {
            animator.SetTrigger("isAttack");
        }
    }
}


