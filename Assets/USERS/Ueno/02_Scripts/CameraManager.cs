using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    GameObject player;
    GameObject mainCamera;
    GameObject subCamera;

    public CinemachineVirtualCameraBase vcam1;
    public CinemachineVirtualCameraBase vcam2;

    // Start is called before the first frame update
    void Start()
    {
        // �擾����
        mainCamera = GameObject.Find("Main Camera"); // ���C���J����
        subCamera = GameObject.Find("SubCamera");    // �T�u�J����

        // �T�u�J�������\���ɂ���
        //subCamera.SetActive(false);
    }

    public void ShakeCamera()
    {
        if (vcam1.Priority == 1)
        {// vcam1�̗D��x��1�Ȃ�
            var impulseSource = vcam1.GetComponent<CinemachineImpulseSource>();

            impulseSource.GenerateImpulse();
        }
        else if (vcam2.Priority == 1)
        {// vcam2�̗D��x��1�Ȃ�
            var impulseSource2 = vcam2.GetComponent<CinemachineImpulseSource>();

            impulseSource2.GenerateImpulse();
        }
    }

    /// <summary>
    /// �T�{�^�[�W���̍ۂ̃J�����؂�ւ�
    /// </summary>
    public void SabotageChengeCam()
    {
        // �v���C���[�Ǐ]�J��������S�̃J�����ɐ؂�ւ�
        vcam1.Priority = 1; // �S�̃J����
        vcam2.Priority = 0; // �Ǐ]�J����
    }

    public void SwitchCamera()
    {// �J�����̕\���E��\����؂�ւ���
        if (vcam1.Priority == 0)
        {// ���C���J������true�Ȃ�

            // ���C�����\���E�T�u��\������
            //mainCamera.SetActive(false);
            //subCamera.SetActive(true);

            vcam1.Priority = 1;
            vcam2.Priority = 0;
        }
        else
        {// ���C���J������false�Ȃ�

            // ���C����\���E�T�u���\������
            //mainCamera.SetActive(true);
            //subCamera.SetActive(false);

            vcam1.Priority = 0;
            vcam2.Priority = 1;
        }
    }
}
