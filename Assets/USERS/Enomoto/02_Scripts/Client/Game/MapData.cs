using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class MapData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// ウソをつくかどうか
    /// </summary>
    public bool isLie { get; set; }
}
