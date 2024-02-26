using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween‚ðŽg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ð“ü‚ê‚é


public class NameSpace : MonoBehaviour
{
    private void Update()
    {
        if (TitleManager.isStart == true)
        {
            MoveName();
        }
    }

    public void MoveName()
    {
        this.transform.DOMove(new Vector3(950f, 500f, 0f), 0.1f).SetEase(Ease.OutBounce);
    }
    
}
