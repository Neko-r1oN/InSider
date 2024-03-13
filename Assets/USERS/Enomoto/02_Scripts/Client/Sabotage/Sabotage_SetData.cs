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
    /// 生成する座標のリスト
    /// </summary>
    List<float> posX { get; set; }
    List<float> posY { get; set; }
    List<float> posZ { get; set; }

    /// <summary>
    /// 生成したらオブジェクトにつけるID
    /// </summary>
    public List<int> objID { get; set; }
}
