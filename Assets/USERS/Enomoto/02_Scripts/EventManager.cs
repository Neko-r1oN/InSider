using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject goldPrefab;
    [SerializeField] GameObject confusionPrefab;    // �����C�x���g�p
    [SerializeField] GameObject buffPrefab;         // �X�^�~�i����ʂ�����C�x���g�p

    // �i�[�p
    public List<GameObject> confusionObjList;
    public List<GameObject> buffObjList;

    GameObject playerManager;

    /// <summary>
    /// ��������C�x���g��ID (�����̃���)
    /// </summary>
    public enum EventOccurrenceID
    {
        RndFallStones = 0,  // �����_���ɋ󂩂�΂��~���Ă���
        Confusion,          // ������ԂɂȂ�
        SpownEnemys,        // �G���o��
        RiStaminaCn,        // �X�^�~�i�̏���ʂ����炷
        RndSpawnGold,       // �����_���ɃS�[���h���󂩂�~���Ă���
        Decoy,              // �f�R�C
    }

    // �����̃G�t�F�N�g�v���t�@�u
    public GameObject chaosPrefab;

    // �N���������C�x���g��ID
    public int eventNum;

    GameObject uiManager;

    GameObject player;

    // �X�^�~�i�̏���ʂ����炷�C�x���g�ɂȂ������ǂ���
    public bool isEventStamina; 

    private void Start()
    {
        if (EditorManager.Instance.useServer == true)
        {// �T�[�o�[���g�p����ꍇ
            playerManager = GameObject.Find("player-List");
        }
        else
        {
            player = GameObject.Find("Player1");
        }

        uiManager = GameObject.Find("UIManager");

        confusionObjList = new List<GameObject>();
        buffObjList = new List<GameObject>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {// S�L�[�������ƃC�x���g���N����
            switch(eventNum)
            {
                case 0: // RndFallStones

                    // �������e

                    break;
                case 1: // Confusion

                    GameObject childObject = Instantiate(chaosPrefab, player.transform);

                    uiManager.GetComponent<UIManager>().isChaos = true;

                    break;
                case 2: // SpownEnemys

                    // �������e

                    break;
                case 3: // RiStaminaCn

                    // �������e
                    isEventStamina = true;

                    break;
                case 4: // RndSpawnGold

                    // �������e

                    break;
                case 5: // Decoy

                    // �������e

                    break;
            }
        }
    }

    /// <summary>
    /// ������Ԃɂ���
    /// </summary>
    public void SetConfusion()
    {
        PlayerManager manager = playerManager.GetComponent<PlayerManager>();

        for (int i = 0; i < manager.players.Count; i++)
        {
            // �i�[����
            confusionObjList.Add(Instantiate(chaosPrefab, manager.players[i].transform));
        }

        // ������Ԃɂ���t���O(true)
        uiManager.GetComponent<UIManager>().isChaos = true;
    }

    /// <summary>
    /// �����C�x���g���I������
    /// </summary>
    public void EndConfusion()
    {
        foreach (GameObject particl in confusionObjList)
        {
            Destroy(particl);
        }

        confusionObjList = new List<GameObject>();

        // ������Ԃɂ���t���O(false)
        uiManager.GetComponent<UIManager>().isChaos = false;
    }

    /// <summary>
    /// �o�t��������
    /// </summary>
    public void SetBuff()
    {
        RoadManager.Instance.buffStamina = 10;

        PlayerManager manager = playerManager.GetComponent<PlayerManager>();

        for (int i = 0; i < manager.players.Count; i++)
        {
            // �i�[����
            buffObjList.Add(Instantiate(buffPrefab, manager.players[i].transform));
        }
    }

    /// <summary>
    /// �o�t�C�x���g���I������
    /// </summary>
    public void EndBuff()
    {
        foreach (GameObject particl in buffObjList)
        {
            Destroy(particl);
        }

        buffObjList = new List<GameObject>();

        RoadManager.Instance.buffStamina = 0;
    }

    /// <summary>
    /// �G�𐶐�����
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnEnemy(Vector3 pos)
    {
        GameObject enemy = Instantiate(enemyPrefab,pos,Quaternion.identity);

        return enemy;
    }

    /// <summary>
    /// �΂̐���
    /// </summary>
    /// <param name="pos"></param>
    public void GenerateEventStone(Vector3 pos)
    {
        // ��������
        Instantiate(stonePrefab, pos,Quaternion.identity);
    }

    /// <summary>
    /// ���̐���
    /// </summary>
    /// <param name="pos"></param>
    public void GenerateEventGold(Vector3 pos)
    {
        // ��������
        Instantiate(goldPrefab, pos, Quaternion.identity);
    }
}
