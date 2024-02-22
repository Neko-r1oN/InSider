using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������


public class TitleLogo : MonoBehaviour
{
    [SerializeField] Renderer logo;


    [Header("1���[�v�̒���(�b�P��)")]
    [SerializeField]
    [Range(0.1f, 5.0f)]
    float duration = 1.0f;


    [Header("���[�v�J�n���̐F")]
    [SerializeField]
    Color32 startColor = new Color32(255, 255, 255, 0);
    //���[�v�I��(�܂�Ԃ�)���̐F��0�`255�܂ł̐����Ŏw��B
    [Header("���[�v�I�����̐F")]
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
