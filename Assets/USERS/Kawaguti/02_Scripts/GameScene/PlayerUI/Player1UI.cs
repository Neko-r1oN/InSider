using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween‚ðŽg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ð“ü‚ê‚é

public class Player1UI : MonoBehaviour
{
    bool isCalledOnce = false;
  
    void Update()
    {

        if(!isCalledOnce && ScoreUI.turnnum == 1)
        {
            isCalledOnce = true;
            MyTurn();
        }
        if(isCalledOnce && ScoreUI.turnnum != 1)
        {
            isCalledOnce = false;
            NotTurn();
        }

    }
    public void NotTurn()
    {
        this.transform.DOLocalMove(new Vector3(-26f, 38.79f, -170.3505f), 0.3f);
    }
    public void MyTurn()
    {
        this.transform.DOLocalMove(new Vector3(20f, 38.79f, -170.3505f), 0.3f);
    }
}
