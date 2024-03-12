//*****************************
//  落石・金のイベントで使用
//*****************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Event_RndFallData
{
    /// <summary>
    /// どのパネルの上に生成するか判別する
    /// </summary>
    public int panelID { get; set; }

    /// <summary>
    /// 座標に加算する値
    /// </summary>
    public float addPosX { get; set; }
    public float addPosZ { get; set; }
}
