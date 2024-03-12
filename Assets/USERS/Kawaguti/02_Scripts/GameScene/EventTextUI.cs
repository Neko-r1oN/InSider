using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTween���g���Ƃ��͂���using������

public class EventTextUI : MonoBehaviour
{
    [SerializeField] Text eventText;    // �C�x���g���̃e�L�X�g
    [SerializeField] Text supText;      // �C�x���g���e�̐���

    public IEnumerator PanelAnim(int eventID)
    {
        switch(eventID)
        {
            case 101:
                eventText.text = "�w���΁x�C�x���g�����I�I";
                supText.text = "���㒍�ӁI�I";
                break;
            case 102:
                eventText.text = "�w�f�o�t�x�C�x���g�����I�I";
                supText.text = "��莞�Ԃ̊ԁA�Ӑ}�������ɐ؂�J���Ȃ��Ȃ�I�I";
                break;
            case 103:
                eventText.text = "�w�G�̏o���x�C�x���g�����I�I";
                supText.text = "�G���o���I�������I�I";
                break;
            case 104:
                eventText.text = "�w�o�t�x�C�x���g�����I�I";
                supText.text = "��莞�Ԃ̊ԁA�S�Ẵv���C���[�̃X�^�~�i����ʂ�����";
                break;
            case 105:
                eventText.text = "�w���̏o���x�C�x���g�����I�I";
                supText.text = "��莞�Ԃ̊ԁA�����_���ɋ����~���Ă���I�I";
                break;
            case 106:
                eventText.text = "�C�x���g�����I�I";
                supText.text = "";
                break;
        }

        transform.rotation = Quaternion.Euler(90, 0, 0);

        yield return new WaitForSeconds(0.5f);

        yield return transform.DORotate(new Vector3(0, 0, 0), 3.0f).WaitForCompletion();
        yield return new WaitForSeconds(2.5f);
        transform.DORotate(new Vector3(90, 0, 0), 0.7f).OnComplete(SetActive);
    }

    private void SetActive()
    {
        this.gameObject.SetActive(false);
    }
}


