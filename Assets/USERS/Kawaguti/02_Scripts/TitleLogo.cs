using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������


public class TitleLogo : MonoBehaviour
{
    [SerializeField] TitleLogo logo;


    // Start is called before the first frame update
    void Start()
    {
        
        this.transform.DOLocalMove(new Vector3(-387.8f, 286.15f, 0f),5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
