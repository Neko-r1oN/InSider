using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class AllieScoreData
{
    /// <summary>
    /// �Ώۂ̃v���C���[��ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// ���M���F��������X�R�A�̒l || ��M���F�\������X�R�A�̒l
    /// </summary>
    public int allieScore { get; set; }
}
