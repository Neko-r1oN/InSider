using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicManager : MonoBehaviour
{
    [SerializeField] List<GameObject> chests;
    
    /// <summary>
    /// �~�~�b�N���ǂ�����ݒ肷��
    /// </summary>
    /// <param name="isMimicList"></param>
    public void SetMimic(List<bool> isMimicList)
    {
        for(int i = 0;i < chests.Count;i++)
        {
            chests[i].GetComponent<ChestTrigger>().isMimic = isMimicList[i];
        }
    }
}
