using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RoundEndData
{
    //************************************************************************************************************
    //
    //  送信するもの：ミミックかどうか、宝箱を開けるプレイヤーID(採掘者のみ)
    //  受信するもの：合計スコアリスト、加減するスコアリスト、ミミックかどうか、役職リスト、宝箱を開けるプレイヤーID(採掘者のみ)
    //
    //************************************************************************************************************

    // 合計スコアのリスト
    public List<int> totalScore { get; set; }

    // 加減するスコア
    public List<int> allieScore { get; set; }

    // ミミックかどうか
    public bool isMimic { get; set; }

    // InsiderのプレイヤーID
    public List<int> insiderID { get; set; }

    // 宝箱をあけるプレイヤーのID
    public int openPlayerID { get; set; }

    // ターン終了によりラウンドが終了したのかどうか
    public bool isTurnEnd { get; set; }
}

