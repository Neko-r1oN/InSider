using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class AllieScoreData
{
    /// <summary>
    /// 対象のプレイヤーのID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// 送信時：加減するスコアの値 || 受信時：表示するスコアの値
    /// </summary>
    public int allieScore { get; set; }
}
