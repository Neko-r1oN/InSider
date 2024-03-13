using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTween‚ðŽg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ð“ü‚ê‚é


public class JudgeLogo : MonoBehaviour
{
    [SerializeField] Text logo;


    

    // Start is called before the first frame update
    void Start()
    {
        logo = GetComponent<Text>();

        //this.transform.DOLocalMove(new Vector3(-387.8f, 286.15f, 0f),5.0f);
        Invoke("move", 7f);
    }
   
    private void move()
    {
        if (OpenManager.Instance.isMimic == true)
        {
            logo.text = "ƒnƒYƒŒ";
        }
        else
        {
            logo.text = "‚ ‚½‚è";
        }

        this.transform.DOLocalMove(new Vector3(-580.67f, 412f, 772f), 5.0f);
    }
}
