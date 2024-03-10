using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class StageData
{
    /// <summary>
    /// �I�u�W�F�N�g��ID
    /// </summary>
    public int objeID { get; set; }

    /// <summary>
    /// X���W
    /// </summary>
    public float posX { get; set; }

    /// <summary>
    /// Y���W
    /// </summary>
    public float posY { get; set; }

    /// <summary>
    /// Z���W
    /// </summary>
    public float posZ { get; set; }

    /// <summary>
    /// �I�u�W�F�N�g�̎�ނ�ID
    /// </summary>
    public enum ObjeType
    {
        RoadPanel_Type_I = 0,       // I���̓��p�l��
        RoadPanel_Type_L,           // L���̓��p�l��
        RoadPanel_Type_T,           // T���̓��p�l��
        RoadPanel_Type_Cross,       // �\���̓��p�l��
        RoadPanel_Type_Dust,        // �S�~�̂悤�ȓ��p�l��
        StartAndGoalPanel,          // �X�^�[�g�n�_�A�S�[���p�̓��p�l��
        Block,                      // �u���b�N
        EventBlock,                 // �C�x���g�u���b�N
    }

    /// <summary>
    /// �I�u�W�F�N�g�̎�ނ�ID
    /// </summary>
    public ObjeType typeID { get; set; }
}
