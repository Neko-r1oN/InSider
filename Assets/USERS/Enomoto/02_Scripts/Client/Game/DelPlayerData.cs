using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class DelPlayerData
{
    /// <summary>
    /// �v���C���[ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// �^�[��UI���X�V���邩�ǂ���
    /// </summary>
    public bool isUdTurn { get; set; }

    /// <summary>
    /// ���ɍs���ł���v���C���[ID
    /// </summary>
    public int nextPlayerID { get; set; }

    /// <summary>
    /// �X�V��̃��X�i�[���X�g
    /// </summary>
    public List<ListenerData> listeners { get; set; }
}
