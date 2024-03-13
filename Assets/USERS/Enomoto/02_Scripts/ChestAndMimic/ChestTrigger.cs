using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestTrigger : MonoBehaviour
{
    [SerializeField] GameObject mimicPrefab;
    [SerializeField] List<GameObject> text;
    public bool isMimic;    // �~�~�b�N���ǂ���

    bool isPlayer;  // �v���C���[�����m�������ǂ���

    private void Start()
    {
        isPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isPlayer == true)
        {
            return;
        }

        if (other.gameObject.layer == 3)
        {// �v���C���[�̏ꍇ
            if (other.GetComponent<Player>().playerObjID == ClientManager.Instance.playerID)
            {// �������g�̏ꍇ
                if (ClientManager.Instance.isInsider == false)
                {// Insider�ł͂Ȃ��ꍇ
                    SendRoundEndData();
                }
            }
        }
    }

    private async void SendRoundEndData()
    {
        if (isPlayer == true)
        {
            return;
        }

        Debug.Log("���M����");

        isPlayer = true;

        // �N���X�ϐ����쐬
        RoundEndData roundEndData = new RoundEndData();
        roundEndData.isMimic = isMimic;
        roundEndData.openPlayerID = ClientManager.Instance.playerID;

        if (isMimic == true)
        {// ���g���~�~�b�N�̏ꍇ
            Debug.Log("�~�~�b�N");
        }
        else
        {// �󔠂̏ꍇ
            Debug.Log("��");
        }

        // �T�[�o�[�ɑ��M����
        await ClientManager.Instance.Send(roundEndData, 4);
    }
}
