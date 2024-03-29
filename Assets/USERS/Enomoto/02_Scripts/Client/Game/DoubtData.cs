using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class DoubtData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// 疑われるプレイヤーのID
    /// </summary>
    public int targetID { get; set; }
}