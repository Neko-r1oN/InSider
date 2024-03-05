using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // �v���C���[�I�u�W�F�N�g�̃��X�g
    public List<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        if (EditorManager.Instance.useServer == false)
        {// �T�[�o�[���g�p���Ȃ��ꍇ
            return;
        }

        for (int i = 0;i < players.Count;i++)
        {
            if(i >= ClientManager.Instance.playerNum)
            {// ���݂��Ȃ��v���C���[�̏ꍇ
                // �j������
                Destroy(players[i]);
                continue;
            }

            if(i == ClientManager.Instance.playerID)
            {// ���g�̃v���C���[ID�ƈ�v����
                players[i].GetComponent<OtherPlayer>().enabled = false; // �X�N���v�g�𖳌��ɂ���
            }
            else
            {
                // �R���|�[�l���g���폜����
                Destroy(players[i].GetComponent<Player>());

                // �������Ă��邷�ׂẴR���C�_�[���擾����
                Collider[] CollArray = players[i].GetComponents<BoxCollider>();

                foreach(Collider collider in CollArray)
                {
                    // �R���C�_�[��j������
                    Destroy(collider);
                }
            }

            // �A�N�e�B�u��
            players[i].SetActive(true);
        }
    }
}
