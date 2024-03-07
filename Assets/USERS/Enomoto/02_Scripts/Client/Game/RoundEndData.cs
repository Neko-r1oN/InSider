using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Action_FillData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// オブジェクトのID
    /// </summary>
    public int objeID { get; set; }
}
