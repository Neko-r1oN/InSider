using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Action_MiningData
{
    /// <summary>
    /// �v���C���[ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// �I�u�W�F�N�g��ID
    /// </summary>
    public int objeID { get; set; }

    /// <summary>
    /// �v���t�@�uID
    /// </summary>
    public int prefabID { get; set; }

    /// <summary>
    /// ��]�x
    /// </summary>
    public int rotY { get; set; }

    /// <summary>
    /// ���𔭌@�ł��邩�ǂ���
    /// </summary>
    public bool isGetGold { get; set; }

    /// <summary>
    /// �Ώۂ̃u���b�N���C�x���g�u���b�N���ǂ���
    /// </summary>
    public bool isEventBlock { get; set; }
}
