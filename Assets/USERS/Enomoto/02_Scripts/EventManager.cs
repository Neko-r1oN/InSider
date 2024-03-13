using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject goldPrefab;

    // �����̍ۂ̍����e�N�X�`���̃��X�g
    //GameObject chaos;

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
        player = GameObject.Find("Player1");

        //chaos = GameObject.Find("ChaosList");

        uiManager = GameObject.Find("UIManager");

        //chaos = GameObject.Find("Chaos");

        //chaos.SetActive(false);
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
