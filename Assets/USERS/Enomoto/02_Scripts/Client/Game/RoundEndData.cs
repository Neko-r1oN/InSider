using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RoundEndData
{
    //************************************************************************************************************
    //
    //  ���M������́F�~�~�b�N���ǂ����A�󔠂��J����v���C���[ID(�̌@�҂̂�)
    //  ��M������́F���v�X�R�A���X�g�A��������X�R�A���X�g�A�~�~�b�N���ǂ����A��E���X�g�A�󔠂��J����v���C���[ID(�̌@�҂̂�)
    //
    //************************************************************************************************************

    // ���v�X�R�A�̃��X�g
    public List<int> totalScore { get; set; }

    // ��������X�R�A
    public List<int> allieScore { get; set; }

    // �~�~�b�N���ǂ���
    public bool isMimic { get; set; }

    // Insider�̃v���C���[ID
    public List<int> insiderID { get; set; }

    // �󔠂�������v���C���[��ID
    public int openPlayerID { get; set; }

    // �^�[���I���ɂ�胉�E���h���I�������̂��ǂ���
    public bool isTurnEnd { get; set; }
}

