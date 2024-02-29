using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject player;
    GameObject mainCamera;
    GameObject subCamera;

    // Start is called before the first frame update
    void Start()
    {
        // �擾����
        player = GameObject.Find("Player");          // �v���C���[
        mainCamera = GameObject.Find("Main Camera"); // ���C���J����
        subCamera = GameObject.Find("SubCamera");    // �T�u�J����

        // �T�u�J�������\���ɂ���
        subCamera.SetActive(false);
    }

    public void SwitchCamera()
    {// �J�����̕\���E��\����؂�ւ���
        if (mainCamera.activeSelf == true)
        {// ���C���J������true�Ȃ�

            // ���C�����\���E�T�u��\������
            mainCamera.SetActive(false);
            subCamera.SetActive(true);
        }
        else
        {// ���C���J������false�Ȃ�

            // ���C����\���E�T�u���\������
            mainCamera.SetActive(true);
            subCamera.SetActive(false);
        }
    }
}