using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Action_NothingData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// スタミナの回復量
    /// </summary>
    public int addStamina { get; set; }

    /// <summary>
    /// 合計のスタミナ量
    /// </summary>
    public int totalStamina { get; set; }
}
