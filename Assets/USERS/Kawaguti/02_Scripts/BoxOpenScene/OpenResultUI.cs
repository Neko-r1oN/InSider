using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween‚ðŽg‚¤‚Æ‚«‚Í‚±‚Ìusing‚ð“ü‚ê‚é

public class OpenResultUI : MonoBehaviour
{
    [SerializeField] GameObject logo;




    // Start is called before the first frame update
    void Start()
    {
        logo = GetComponent<GameObject>();

        //this.transform.DOLocalMove(new Vector3(-387.8f, 286.15f, 0f),5.0f);
        Invoke("move", 17f);
    }

    // Update is called once per frame
    void Update()
    {
        //logo.material.color = Color.Lerp(logo.material.color, new Color(1, 1, 1.0f, 1), 0.350f * Time.deltaTime);
    }
    private void move()
    {
        this.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 3.0f);
        Invoke("ChangeScene", 9f);
    }
    private void ChangeScene()
    {
        Initiate.Fade("StandbyScene_copy", Color.black, 1.0f);
    }
}
