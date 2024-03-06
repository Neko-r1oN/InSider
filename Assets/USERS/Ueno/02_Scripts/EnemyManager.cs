using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] [Tooltip("�v���C���[�}�l�[�W���[")] GameObject playerManager;

    List<GameObject> targetList;

    NavMeshAgent navMeshAgent;

    public bool touch;

    // Start is called before the first frame update
    void Start()
    {
        targetList = playerManager.GetComponent<PlayerManager>().players;

        // NavMeshAgent��ێ����Ă���
        navMeshAgent = GetComponent<NavMeshAgent>();

        touch = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ��ԒZ���������i�[
        float shortDis = 10000f;

        // �^�[�Q�b�g�̃C���f�b�N�X�ԍ�
        int indexNum = 0;

        for (int i = 0; i < targetList.Count; i++)
        {
            Vector3 targetPos = targetList[i].transform.position;

            Vector3 enemyPos = transform.position;

            float distance = Vector3.Distance(targetPos, enemyPos);

            if (targetList[i].GetComponent<Player>().mode != Player.PLAYER_MODE.DOWN)
            {
                if (distance < shortDis)
                {
                    indexNum = i;
                    shortDis = distance;
                }
            }
        }

        // �v���C���[��ڎw���Đi��
        navMeshAgent.destination = targetList[indexNum].transform.position;
    }

    public void CreateEnemy(float posX, float posY, float posZ)
    {
        this.gameObject.SetActive(true);

        this.gameObject.transform.position = new Vector3(posX,posY,posZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("�^�b�`���ꂽ");

            other.GetComponent<Player>().mode = Player.PLAYER_MODE.DOWN;

            touch = true;

            //player.GetComponent<Player>().TouchPlayer();

            Debug.Log(other.GetComponent<Player>().mode);
        }
    }
}
