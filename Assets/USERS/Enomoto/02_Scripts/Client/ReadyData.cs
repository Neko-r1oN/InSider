using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class ReadyData
{
    /// <summary>
    /// プレイヤーの名前
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 準備完了しているかどうか
    /// </summary>
    public bool isReady { get; set; }
}
