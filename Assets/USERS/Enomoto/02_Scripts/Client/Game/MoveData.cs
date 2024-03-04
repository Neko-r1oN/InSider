using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class MoveData
{
    /// <summary>
    /// �v���C���[ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// (�ړ�����Ƃ�)�ړI�n��X���W
    /// </summary>
    public float targetPosX { get; set; }

    /// <summary>
    /// (�ړ�����Ƃ�)�ړI�n��Y���W
    /// </summary>
    public float targetPosY { get; set; }

    /// <summary>
    /// (�ړ�����Ƃ�)�ړI�n��Z���W
    /// </summary>
    public float targetPosZ { get; set; }
}
