using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RoundRadyData
{
    /// <summary>
    /// ��E
    /// </summary>
    public bool isInsider { get; set; }

    /// <summary>
    /// ��s�̃v���C���[ID
    /// </summary>
    public int advancePlayerID { get; set; }

    /// <summary>
    /// �ő�^�[����
    /// </summary>
    public int turnMaxNum { get; set; }

    /// <summary>
    /// ���݂̃��E���h��
    /// </summary>
    public int roundNum { get; set; }

    /// <summary>
    /// �S�Ẵv���C���[�̃X�R�A���X�g
    /// </summary>
    public List<int> scoreList { get; set; }
}
