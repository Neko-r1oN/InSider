using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class UpdateTurnsData
{
    /// <summary>
    /// �X�V��̎c��^�[����
    /// </summary>
    public int turnNum { get; set; }

    /// <summary>
    /// ���ɍs���ł���v���C���[��ID
    /// </summary>
    public int nextPlayerID { get; set; }
}
