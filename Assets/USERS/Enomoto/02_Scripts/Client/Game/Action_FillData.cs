using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Action_FillData
{
    /// <summary>
    /// �v���C���[ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// ���ɍs���ł���v���C���[ID
    /// </summary>
    public int nextPlayerID { get; set; }

    /// <summary>
    /// �I�u�W�F�N�g��ID
    /// </summary>
    public int objeID { get; set; }
}
