using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RevisionPosAndDropGoldData
{
    /// <summary>
    /// 送信したプレイヤーのID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// 位置を修正したいプレイヤーオブジェクトのID
    /// </summary>
    public int targetID { get; set; }

    /// <summary>
    /// ブロックに埋まったかどうか
    /// </summary>
    public bool isBuried { get; set; }

    /// <summary>
    /// ダウン状態になるかどうか
    /// </summary>
    public bool isDown { get; set; }

    /// <summary>
    /// 金のドロップ数
    /// </summary>
    public int goldDropNum { get; set; }

    /// <summary>
    /// 目的地のX座標
    /// </summary>
    public float targetPosX { get; set; }

    /// <summary>
    /// 目的地のY座標
    /// </summary>
    public float targetPosY { get; set; }

    /// <summary>
    /// 目的地のZ座標
    /// </summary>
    public float targetPosZ { get; set; }
}
