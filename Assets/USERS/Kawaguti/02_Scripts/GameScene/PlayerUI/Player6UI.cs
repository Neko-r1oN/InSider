using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������

public class Player6UI : MonoBehaviour
{
    bool isCalledOnce = false;

    void Update()
    {

        if (!isCalledOnce && ScoreUI.turnnum ==6)
        {
            isCalledOnce = true;
            MyTurn();
        }
        if (isCalledOnce && ScoreUI.turnnum != 6)
        {
            isCalledOnce = false;
            NotTurn();
        }

    }
    private void NotTurn()
    {
        this.transform.DOLocalMove(new Vector3(-26f, -342.9f, -170.3505f), 0.3f);
    }
    private void MyTurn()
    {
        this.transform.DOLocalMove(new Vector3(20f, -342.9f, -170.3505f), 0.3f);
    }
}