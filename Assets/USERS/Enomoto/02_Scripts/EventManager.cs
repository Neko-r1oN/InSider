using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
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

    // �N���������C�x���g��ID
    public int eventNum;

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

                    // �������e

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
}
