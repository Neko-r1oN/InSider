/////////////////////////////////////////////
//
//���U���gUI�𓮂����X�N���v�g
//Auther : Kawaguchi Kyousuke
//Date 2024.02/27
//
////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������

public class ResultUI : MonoBehaviour
{
    private void Update()
    {
        if (ResultManager.isResult == true)
        {
            MoveResult();
        }
    }
    public void MoveResult()
    {
        this.transform.DOMove(new Vector3(958f, 561f, 0f), 0.35f).SetEase(Ease.OutBounce);
    }

}
