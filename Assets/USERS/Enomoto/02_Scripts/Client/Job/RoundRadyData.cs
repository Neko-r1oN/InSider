using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RoundRadyData
{
    /// <summary>
    /// 役職
    /// </summary>
    public bool isInsider { get; set; }

    /// <summary>
    /// 先行のプレイヤーID
    /// </summary>
    public int advancePlayerID { get; set; }

    /// <summary>
    /// 最大ターン数
    /// </summary>
    public int turnMaxNum { get; set; }

    /// <summary>
    /// 現在のラウンド数
    /// </summary>
    public int roundNum { get; set; }

    /// <summary>
    /// 全てのプレイヤーのスコアリスト
    /// </summary>
    public List<int> scoreList { get; set; }
}
