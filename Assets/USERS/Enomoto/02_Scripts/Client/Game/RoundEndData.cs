using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RoundEndData
{
    /// <summary>
    /// リスナーのリスト
    /// </summary>
    public List<ListenerData> listeners { get; set; }
}
