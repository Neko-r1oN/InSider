using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������

public class MovePlayerUI : MonoBehaviour
{
    Vector3 startPos;

    private void Start()
    {
        
        startPos = this.transform.position;

        Debug.Log(startPos);
    }

    /// <summary>
    /// ������
    /// </summary>
    public void MoveOrReturn(bool isMove,int indexNum)
    {
        //Vector3 pos = new Vector3();

        //if(indexNum == 0)
        //{
        //    pos = new Vector3(-26f, 38.79f, -170.3505f);
        //}
        //else if(indexNum == 1)
        //{
        //    pos = new Vector3(-26f, -35.3f, -170.3505f);
        //}
        //else if (indexNum == 2)
        //{
        //    pos = new Vector3(-26f, -111.4199f, -170.3505f);
        //}
        //else if (indexNum == 3)
        //{
        //    pos = new Vector3(-26f, -189.3f, -170.3505f);
        //}
        //else if (indexNum == 4)
        //{
        //    pos = new Vector3(-26f, -267.5f, -170.3505f);
        //}
        //else if (indexNum == 5)
        //{
        //    pos = new Vector3(-26f, -342.9f, -170.3505f);

        //}
        
        //����
        Canvas.ForceUpdateCanvases();

        Debug.Log(transform.localPosition);
        if (isMove == true)
        {// ����
            //this.transform.DOLocalMove(new Vector3(-26f, pos.y, pos.z), 0.3f);    
            this.transform.DOLocalMove(new Vector3(transform.localPosition.x + 35.0f, transform.localPosition.y, transform.localPosition.z), 0.3f);
        }
        else if(isMove !=true)
        {// ���̈ʒu�֖߂�
            //this.transform.DOLocalMove(new Vector3(20f, pos.y, pos.z), 0.3f);

            this.transform.DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), 0.3f);
        }
    }
}
