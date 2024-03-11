using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingUI : MonoBehaviour
{
    //***************************************
    //  3DãÛä‘ÇâÒì]Ç∑ÇÈ [3Å`]
    //***************************************

    private const float DURATION = 1f;

    void Start()
    {
        Image[] circles = GetComponentsInChildren<Image>();
        for (var i = 0; i < circles.Length; i++)
        {
            var angle = 2 * Mathf.PI * i / circles.Length;
            circles[i].rectTransform.anchoredPosition3D = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * 50f;
            circles[i].rectTransform.DOLocalRotate(-Vector3.up * (360f / circles.Length), DURATION).SetLoops(-1);
            circles[i].color = new Color(1f, 1f, 1f, 0.7f);
        }

        GetComponent<Transform>().DOLocalRotate(Vector3.up * (360f / circles.Length), DURATION).SetLoops(-1);
    }
}
