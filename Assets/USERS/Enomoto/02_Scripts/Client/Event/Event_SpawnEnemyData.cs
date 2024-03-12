//*****************************
//  落石・金のイベントで使用
//*****************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class Event_SpawnEnemyData
{
    /// <summary>
    /// どのパネルの上に生成するか判別する
    /// </summary>
    public int panelID { get; set; }
}
