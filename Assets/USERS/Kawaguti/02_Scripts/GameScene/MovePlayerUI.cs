using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween‚ğg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ğ“ü‚ê‚é

public class MovePlayerUI : MonoBehaviour
{
    /// <summary>
    /// “®‚©‚·
    /// </summary>
    public void MoveOrReturn(bool isMove)
    {
        //ô•¶ [À•W‚ğ³‹K‰»‚·‚é‚½‚ß]
        Canvas.ForceUpdateCanvases();

        Debug.Log(transform.localPosition.x);
        if (isMove == true)
        {// “®‚­
            //this.transform.DOLocalMove(new Vector3(-26f, pos.y, pos.z), 0.3f);    
            this.transform.DOLocalMove(new Vector3(-34.3f + 35.0f, transform.localPosition.y, transform.localPosition.z), 0.3f);
        }
        else if(isMove != true)
        {// Œ³‚ÌˆÊ’u‚Ö–ß‚é
            this.transform.DOLocalMove(new Vector3(-34.3f, transform.localPosition.y, transform.localPosition.z), 0.3f);
        }
    }
}
