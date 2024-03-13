using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Sabotage_Bomb_ExplosionData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int subTurnNum { get; set; }

    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int restTurnNum { get; set; }
}
