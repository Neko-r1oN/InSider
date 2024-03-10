using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // �v���C���[�I�u�W�F�N�g�̃��X�g
    public List<GameObject> players;

    // �g���K�[�̃��X�g (������)
    public List<GameObject> triggerList;

    // Start is called before the first frame update
    void Start()
    {
        if (EditorManager.Instance.useServer == false)
        {// �T�[�o�[���g�p���Ȃ��ꍇ
            return;
        }

        for (int i = 0;i < players.Count;i++)
        {
            if(i >= ClientManager.Instance.playerNameList.Count)
            {// ���݂��Ȃ��v���C���[�̏ꍇ
                // �j������
                Destroy(players[i]);

                // ���X�g����v�f���폜
                players.RemoveAt(i);

                continue;
            }
            else
            {
                // ID��ݒ肷��
                players[i].GetComponent<Player>().playerObjID = i;
            }

            if (i != ClientManager.Instance.playerID)
            {// ���g�̃v���C���[ID�ƈ�v���Ȃ��ꍇ
                // �g���K�[�p�̃I�u�W�F�N�g��j������
                Destroy(triggerList[i]);

                // �������Ă��邷�ׂẴR���C�_�[���擾����
                Collider[] CollArray = players[i].GetComponents<BoxCollider>();

                foreach (Collider collider in CollArray)
                {
                    // �R���C�_�[��j������
                    Destroy(collider);
                }
            }

            // �A�N�e�B�u��
            players[i].SetActive(true);
        }
    }

    private void Update()
    {
        for(int i = 0;i < players.Count; i++)
        {
            Player player = players[i].GetComponent<Player>();

            if (player.mode == Player.PLAYER_MODE.DOWN)
            {
                player.BlinkPlayer();

                if (player.cnt >= 200)
                {
                    player.RecoverPlayer();
                }
            }
        }
    }
}
