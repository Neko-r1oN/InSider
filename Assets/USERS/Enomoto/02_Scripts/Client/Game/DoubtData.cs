using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class DoubtData
{
    /// <summary>
    /// �v���C���[ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// �^����v���C���[��ID
    /// </summary>
    public int targetID { get; set; }
}