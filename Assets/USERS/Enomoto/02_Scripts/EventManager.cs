using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject goldPrefab;

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

    GameObject player;

    // �V���O���g���p
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // �V�[���J�ڂ��Ă��j�����Ȃ��悤�ɂ���
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player1");
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

                    break;
                case 2: // SpownEnemys

                    // �������e

                    break;
                case 3: // RiStaminaCn

                    // �������e

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
