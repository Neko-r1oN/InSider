using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class UpdateTurnsData
{
    /// <summary>
    /// 更新後の残りターン数
    /// </summary>
    public int turnNum { get; set; }

    /// <summary>
    /// 次に行動できるプレイヤーのID
    /// </summary>
    public int nextPlayerID { get; set; }
}
