using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RevisionPosData
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
    /// 敵に触れられたのかどうか
    /// </summary>
    public bool isEnemy { get; set; }

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
