using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class MoveData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// (移動するとき)目的地のX座標
    /// </summary>
    public float targetPosX { get; set; }

    /// <summary>
    /// (移動するとき)目的地のY座標
    /// </summary>
    public float targetPosY { get; set; }

    /// <summary>
    /// (移動するとき)目的地のZ座標
    /// </summary>
    public float targetPosZ { get; set; }
}
