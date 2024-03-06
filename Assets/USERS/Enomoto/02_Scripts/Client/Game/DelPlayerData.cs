using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class DelPlayerData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// ターンUIを更新するかどうか
    /// </summary>
    public bool isUdTurn { get; set; }

    /// <summary>
    /// 次に行動できるプレイヤーID
    /// </summary>
    public int nextPlayerID { get; set; }

    /// <summary>
    /// 更新後のリスナーリスト
    /// </summary>
    public List<ListenerData> listeners { get; set; }
}
