using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Action_MiningData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// オブジェクトのID
    /// </summary>
    public int objeID { get; set; }

    /// <summary>
    /// プレファブID
    /// </summary>
    public int prefabID { get; set; }

    /// <summary>
    /// 回転度
    /// </summary>
    public int rotY { get; set; }

    /// <summary>
    /// 金を発掘できるかどうか
    /// </summary>
    public bool isGetGold { get; set; }

    /// <summary>
    /// 対象のブロックがイベントブロックかどうか
    /// </summary>
    public bool isEventBlock { get; set; }
}
