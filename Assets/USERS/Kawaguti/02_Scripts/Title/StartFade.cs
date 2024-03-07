using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������


public class StartFade : MonoBehaviour
{
    [SerializeField] Renderer logo;

    [Header("���[�v�J�n���̐F")]
    [SerializeField]
    Color32 startColor = new Color32(255, 255, 255, 255);
    //���[�v�I��(�܂�Ԃ�)���̐F��0�`255�܂ł̐����Ŏw��B
    [Header("���[�v�I�����̐F")]
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
