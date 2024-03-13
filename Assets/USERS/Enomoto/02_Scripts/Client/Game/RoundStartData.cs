using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RoundStartData
{
    // 宝箱の中身がミミックかどうか
    public List<bool> isMimicList { get; set; }
}
