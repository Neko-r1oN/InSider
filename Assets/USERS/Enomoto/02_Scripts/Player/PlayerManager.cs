using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // �v���C���[�I�u�W�F�N�g�̃��X�g
    public List<GameObject> players;

    // �g���K�[�̃��X�g
    public List<GameObject> triggerList;

    private void Awake()
    {
        if (EditorManager.Instance.useServer == false)
        {// �T�[�o�[���g�p���Ȃ��ꍇ
            return;
        }

        // �C���X�y�N�^�[��Őݒ肵�Ă���I�u�W�F�N�g������̂ŏ���������
        players = new List<GameObject>();

        Debug.Log("�v���C���[�l���F" + ClientManager.Instance.playerNameList.Count);

        // �q�I�u�W�F�N�g�̐����̔z����쐬
        GameObject[] children = new GameObject[this.transform.childCount];

        // �q�I�u�W�F�N�g�����Ɏ擾����
        for (int i = 0; i < children.Length; i++)
        {
            // �q�I�u�W�F�N�g���擾
            Transform childTransform = this.transform.GetChild(i);

            // Player�I�u�W�F�N�g�����X�g�ɒǉ�����
            players.Add(childTransform.gameObject);

            // ID��ݒ肷��
            players[i].GetComponent<Player>().playerObjID = i;

            if (i < ClientManager.Instance.playerNameList.Count && ClientManager.Instance.isConnectList[i] == true)
            {// ���݂���v���C���[�̏ꍇ

                Debug.Log(i + "�ǉ�");

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
            else
            {// ���݂��Ȃ��v���C���[�̏ꍇ

                Debug.Log(i + "�j��");

                Destroy(childTransform.gameObject);
            }
        }

        
    }

    /// <summary>
    /// �_�ł̃R���[�`�����J�n
    /// </summary>
    public IEnumerator StartBlink(int indexNum)
    {
        bool isActive = false;

        for (int i = 0; i < 20; i++)
        {
            players[indexNum].SetActive(isActive);

            // �t���O��؂�ւ�
            isActive = !isActive;

            yield return new WaitForSeconds(0.25f);
        }

        // �_�E����Ԃ���������
        players[indexNum].GetComponent<Player>().RevisionPos(new Vector3(0f, 0.9f, -5f));

        yield return new WaitForSeconds(5f);

        // ���G��Ԃ���������
        players[indexNum].GetComponent<Player>().isInvincible = false;
    }
}
