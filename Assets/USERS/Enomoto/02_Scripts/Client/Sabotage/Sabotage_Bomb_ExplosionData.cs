using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Sabotage_Bomb_ExplosionData
{
    /// <summary>
    /// 減るターン数
    /// </summary>
    public int subTurnNum { get; set; }

    /// <summary>
    /// 残りのターン数
    /// </summary>
    public int restTurnNum { get; set; }
}
