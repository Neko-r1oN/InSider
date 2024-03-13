using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Sabotage_SetData
{
    /// <summary>
    /// �v���C���[ID
    /// </summary>
    public int playerID { get; set; }    // �v���C���[ID

    /// <summary>
    /// �T�{�^�[�W����ID
    /// </summary>
    public int sabotageID { get; set; }  // �T�{�^�[�W����ID

    /// <summary>
    /// ����������W�̃��X�g
    /// </summary>
    List<float> posX { get; set; }
    List<float> posY { get; set; }
    List<float> posZ { get; set; }

    /// <summary>
    /// ����������I�u�W�F�N�g�ɂ���ID
    /// </summary>
    public List<int> objID { get; set; }
}
