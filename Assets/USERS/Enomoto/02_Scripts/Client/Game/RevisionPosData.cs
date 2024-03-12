using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
internal class RevisionPosData
{
    /// <summary>
    /// ���M�����v���C���[��ID
    /// </summary>
    public int playerID { get; set; }

    /// <summary>
    /// �ʒu���C���������v���C���[�I�u�W�F�N�g��ID
    /// </summary>
    public int targetID { get; set; }

    /// <summary>
    /// �u���b�N�ɖ��܂������ǂ���
    /// </summary>
    public bool isBuried { get; set; }

    /// <summary>
    /// �G�ɐG���ꂽ�̂��ǂ���
    /// </summary>
    public bool isEnemy { get; set; }

    /// <summary>
    /// �ړI�n��X���W
    /// </summary>
    public float targetPosX { get; set; }

    /// <summary>
    /// �ړI�n��Y���W
    /// </summary>
    public float targetPosY { get; set; }

    /// <summary>
    /// �ړI�n��Z���W
    /// </summary>
    public float targetPosZ { get; set; }
}
