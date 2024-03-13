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
    bool isOK;

    private void Start()
    {
        isOK = false;
    }

    private void Update()
    {
        if (ResultManager.isResult == true && isOK == false)
        {
            isOK = true;
            StartCoroutine(MoveResult());
        }
    }
    IEnumerator MoveResult()
    {
        this.transform.DOMove(new Vector3(958f, 561f, 0f), 0.35f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(5f);

        // �ڑ����I�����^�C�g����ʂ֑J��
        ClientManager.Instance.DisconnectButton();
    }
}
