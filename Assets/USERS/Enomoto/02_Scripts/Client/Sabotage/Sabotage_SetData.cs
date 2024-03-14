using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Sabotage_SetData
{
    /// <summary>
    /// プレイヤーID
    /// </summary>
    public int playerID { get; set; }    // プレイヤーID

    /// <summary>
    /// サボタージュのID
    /// </summary>
    public int sabotageID { get; set; }  // サボタージュのID

    /// <summary>
    /// 選択したブロックについているID
    /// </summary>
    public List<int> objID { get; set; }

    /// <summary>
    /// 爆弾のID
    /// </summary>
    public List<int> bombID { get; set; }
}
