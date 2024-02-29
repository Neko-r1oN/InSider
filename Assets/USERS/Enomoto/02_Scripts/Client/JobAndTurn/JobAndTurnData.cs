using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class JobAndTurnData
{
    /// <summary>
    /// 役職
    /// </summary>
    public bool isInsider { get; set; }

    /// <summary>
    /// 先行のプレイヤーID
    /// </summary>
    public int advancePlayerID { get; set; }

}
