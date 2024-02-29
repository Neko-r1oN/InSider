using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class ListenerData
{
    /// <summary>
    /// プレイヤーの名前
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// プレイヤーID (1P,2P・・・)
    /// </summary>
    public int id { get; set; }
}
