using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������


public class JudgeLogo : MonoBehaviour
{
    [SerializeField] Text logo;


    

    // Start is called before the first frame update
    void Start()
    {
        logo = GetComponent<Text>();

        //this.transform.DOLocalMove(new Vector3(-387.8f, 286.15f, 0f),5.0f);
        Invoke("move", 7f);
    }
   
    private void move()
    {
        if (OpenManager.Instance.isMimic == true)
        {
            logo.text = "�n�Y��";
        }
        else
        {
            logo.text = "������";
        }

        this.transform.DOLocalMove(new Vector3(-580.67f, 412f, 772f), 5.0f);
    }
}
