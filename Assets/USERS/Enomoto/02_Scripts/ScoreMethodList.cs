using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMethodList : MonoBehaviour
{
    // �V���O���g���p
    public static ScoreMethodList Instance;

    // �S�[���h1�ɑ΂��ẴX�R�A�̒l
    const int scoreNum = 1;

    // �T�[�o�[�ɑ��M���邽�߂̃N���X�ϐ�
    AllieScoreData scoreData = new AllieScoreData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(EditorManager.Instance.useServer == false)
        {// �T�[�o�[���g�p���Ȃ��ꍇ
            return;
        }
    }

    /// <summary>
    /// ���Z����X�R�A���T�[�o�[�ɑ��M
    /// </summary>
    public async void SendAddScore()
    {
        if (EditorManager.Instance.useServer == false)
        {// �T�[�o�[���g�p���Ȃ��ꍇ
            return;
        }

        // �T�[�o�[�ɑ��M����ϐ��̒l��ݒ�
        scoreData.playerID = ClientManager.Instance.playerID;   // playerID�͕ϓ�����\�������邽�߂����ő������
        scoreData.allieScore = scoreNum;

        // �T�[�o�[�ɑ��M����
        await ClientManager.Instance.Send(scoreData, 14);
    }

    /// <summary>
    /// ���Z����X�R�A���T�[�o�[�ɑ��M
    /// </summary>
    /// <param name="loseGoldNum">�S�[���h��������</param>
    public async void SendSubScore(int loseGoldNum)
    {
        if (EditorManager.Instance.useServer == false)
        {// �T�[�o�[���g�p���Ȃ��ꍇ
            return;
        }

        // �T�[�o�[�ɑ��M����ϐ��̒l��ݒ�
        scoreData.playerID = ClientManager.Instance.playerID;   // playerID�͕ϓ�����\�������邽�߂����ő������
        scoreData.allieScore = loseGoldNum * scoreNum * -1;

        // �T�[�o�[�ɑ��M����
        await ClientManager.Instance.Send(scoreData, 14);
    }
}
