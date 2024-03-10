using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class StageData
{
    /// <summary>
    /// オブジェクトのID
    /// </summary>
    public int objeID { get; set; }

    /// <summary>
    /// X座標
    /// </summary>
    public float posX { get; set; }

    /// <summary>
    /// Y座標
    /// </summary>
    public float posY { get; set; }

    /// <summary>
    /// Z座標
    /// </summary>
    public float posZ { get; set; }

    /// <summary>
    /// オブジェクトの種類のID
    /// </summary>
    public enum ObjeType
    {
        RoadPanel_Type_I = 0,       // I字の道パネル
        RoadPanel_Type_L,           // L字の道パネル
        RoadPanel_Type_T,           // T字の道パネル
        RoadPanel_Type_Cross,       // 十字の道パネル
        RoadPanel_Type_Dust,        // ゴミのような道パネル
        StartAndGoalPanel,          // スタート地点、ゴール用の道パネル
        Block,                      // ブロック
        EventBlock,                 // イベントブロック
    }

    /// <summary>
    /// オブジェクトの種類のID
    /// </summary>
    public ObjeType typeID { get; set; }
}
