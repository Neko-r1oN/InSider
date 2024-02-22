using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる


public class TitleLogo : MonoBehaviour
{
    [SerializeField] Renderer logo;


    [Header("1ループの長さ(秒単位)")]
    [SerializeField]
    [Range(0.1f, 5.0f)]
    float duration = 1.0f;


    [Header("ループ開始時の色")]
    [SerializeField]
    Color32 startColor = new Color32(255, 255, 255, 0);
    //ループ終了(折り返し)時の色を0～255までの整数で指定。
    [Header("ループ終了時の色")]
    [SerializeField]
    Color32 endColor = new Color32(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        logo = GetComponent<Renderer>();
        logo.material.color = startColor;
        //this.transform.DOLocalMove(new Vector3(-387.8f, 286.15f, 0f),5.0f);
        this.transform.DOLocalMove(new Vector3(-1019f, -108f,772f), 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        logo.material.color = Color.Lerp(logo.material.color, new Color(1, 1, 1.0f, 1), 0.350f * Time.deltaTime);
    }
}
