using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreasureManager : MonoBehaviour
{

    // �����_���֐�
    System.Random rnd = new System.Random();

    // �����_���Ȑ��l�����邽�߂̕ϐ�
    int rand;

    int treasureCount = 0; // �󔠂̐����J�E���g

    int mimicCount = 0;    // �~�~�b�N�̐����J�E���g;

    // Start is called before the first frame update
    void Start()
    {
        rand = rnd.Next(0, 3); // 0�`2�̐��l������
    }

    // Update is called once per frame
    void Update()
    {
        if(rand <= 1)
        {//�����_���̐��l��1�ȉ��Ȃ�
            if (treasureCount <= 2)
            {// �󔠂̌���2�ȉ��Ȃ�
                // �^�O��ݒ�
                this.gameObject.tag = "Treasure";

                // �󔠃J�E���g�𑝂₷(���𑝂₷)
                treasureCount++;
            }
            
        }
        else if(rand >= 2)
        {// �����_���̐��l��2�ȏ�Ȃ�
            if(mimicCount <= 1)
            {// �~�~�b�N�̌���1�ȉ��Ȃ�
                // �^�O��ݒ�
                this.gameObject.tag = "Mimic";

                // �~�~�b�N�J�E���g�𑝂₷(���𑝂₷)
                mimicCount++;
            }
        }
    }
}
