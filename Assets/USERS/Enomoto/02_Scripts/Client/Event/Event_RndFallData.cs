//*****************************
//  ���΁E���̃C�x���g�Ŏg�p
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
    /// �ǂ̃p�l���̏�ɐ������邩���ʂ���
    /// </summary>
    public int panelID { get; set; }

    /// <summary>
    /// ���W�ɉ��Z����l
    /// </summary>
    public float addPosX { get; set; }
    public float addPosZ { get; set; }
}
