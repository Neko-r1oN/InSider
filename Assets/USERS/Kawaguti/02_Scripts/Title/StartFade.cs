using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる


public class StartFade : MonoBehaviour
{
    [SerializeField] Renderer logo;

    [Header("ループ開始時の色")]
    [SerializeField]
    Color32 startColor = new Color32(255, 255, 255, 255);
    //ループ終了(折り返し)時の色を0～255までの整数で指定。
    [Header("ループ終了時の色")]
    [SerializeField]
    Color32 endColor = new Color32(255, 255, 255, 0);

    // Start is called before the first frame update
    void Start()
    {
        logo = GetComponent<Renderer>();
        logo.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        logo.material.color = Color.Lerp(logo.material.color, new Color(1, 1, 1.0f, 0), 0.1380f * Time.deltaTime);
    }
}
