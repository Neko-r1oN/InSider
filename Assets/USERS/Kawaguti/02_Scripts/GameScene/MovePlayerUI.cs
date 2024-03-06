using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる

public class MovePlayerUI : MonoBehaviour
{
    /// <summary>
    /// 動かす
    /// </summary>
    public void MoveOrReturn(bool isMove)
    {
        //呪文 [座標を正規化するため]
        Canvas.ForceUpdateCanvases();

        Debug.Log(transform.localPosition.x);
        if (isMove == true)
        {// 動く
            //this.transform.DOLocalMove(new Vector3(-26f, pos.y, pos.z), 0.3f);    
            this.transform.DOLocalMove(new Vector3(-34.3f + 35.0f, transform.localPosition.y, transform.localPosition.z), 0.3f);
        }
        else if(isMove != true)
        {// 元の位置へ戻る
            this.transform.DOLocalMove(new Vector3(-34.3f, transform.localPosition.y, transform.localPosition.z), 0.3f);
        }
    }
}
