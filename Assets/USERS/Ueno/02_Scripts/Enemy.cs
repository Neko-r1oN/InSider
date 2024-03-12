using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject playerManager;

    List<GameObject> targetList;

    NavMeshAgent navMeshAgent;

    // ��������Ă���O�b�ҋ@����
    bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("player-List");

        // new����
        targetList = new List<GameObject>();

        // Player
        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            targetList = playerManager.GetComponent<PlayerManager>().players;
        }
        else
        {// �T�[�o�[���g�p���Ȃ�
            targetList.Add(GameObject.Find("Player1"));
        }

        // NavMeshAgent��ێ����Ă���
        navMeshAgent = GetComponent<NavMeshAgent>();

        isStart = false;

        Invoke("StartMove", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart == false)
        {
            return;
        }

        // ��ԒZ���������i�[
        float shortDis = 10000f;

        // �^�[�Q�b�g�̃C���f�b�N�X�ԍ�
        int indexNum = 0;

        // �^�[�Q�b�g�����݂��邩�ǂ���
        bool isTurget = true;

        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].GetComponent<Player>().isInvincible == false)
            {// �v���C���[�����G��ԈȊO�̏ꍇ

                isTurget = true;

                Vector3 targetPos = targetList[i].transform.position;

                Vector3 enemyPos = transform.position;

                float distance = Vector3.Distance(targetPos, enemyPos);

                if (distance < shortDis)
                {
                    indexNum = i;
                    shortDis = distance;
                }
            }
            else
            {
                isTurget = false;
            }
        }

        if (isTurget == true)
        {// �^�[�Q�b�g������ꍇ
            // �v���C���[��ڎw���Đi��
            navMeshAgent.destination = targetList[indexNum].transform.position;
        }
        else
        {// �^�[�Q�b�g�����Ȃ��ꍇ
            navMeshAgent.destination = this.transform.position;
        }
    }

    /// <summary>
    /// Update�̏������J�n
    /// </summary>
    private void StartMove()
    {
        isStart = true;
    }
}
