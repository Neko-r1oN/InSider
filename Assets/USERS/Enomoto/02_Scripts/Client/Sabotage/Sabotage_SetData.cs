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
    /// �I�������u���b�N�ɂ��Ă���ID
    /// </summary>
    public List<int> objID { get; set; }

    /// <summary>
    /// ���e��ID
    /// </summary>
    public List<int> bombID { get; set; }
}
