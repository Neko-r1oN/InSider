using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Action_NothingData
{
    /// <summary>
    /// �v���C���[ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// �X�^�~�i�̉񕜗�
    /// </summary>
    public int addStamina { get; set; }

    /// <summary>
    /// ���v�̃X�^�~�i��
    /// </summary>
    public int totalStamina { get; set; }
}
