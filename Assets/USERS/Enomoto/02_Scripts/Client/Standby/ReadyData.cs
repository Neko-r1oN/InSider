using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class ReadyData
{
    ///// <summary>
    ///// �v���C���[��ID
    ///// </summary>
    //public string name { get; set; }

    /// <summary>
    /// �v���C���[��ID
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// �����������Ă��邩�ǂ���
    /// </summary>
    public bool isReady { get; set; }
}
