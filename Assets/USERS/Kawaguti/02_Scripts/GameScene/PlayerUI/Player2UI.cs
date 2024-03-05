using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween‚ðŽg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ð“ü‚ê‚é

public class Player2UI : MonoBehaviour
{
    bool isCalledOnce = false;

    void Update()
    {

        if(!isCalledOnce && ScoreUI.turnnum == 2)
        {
            isCalledOnce = true;
            MyTurn();
        }
        if(isCalledOnce && ScoreUI.turnnum != 2)
        {
            isCalledOnce = false;
            NotTurn();
        }

    }
    private void NotTurn()
    {
        this.transform.DOLocalMove(new Vector3(-26f, -35.3f, -170.3505f), 0.3f);
    }
    private void MyTurn()
    {
        this.transform.DOLocalMove(new Vector3(20f, -35.3f, -170.3505f), 0.3f);
    }
}
