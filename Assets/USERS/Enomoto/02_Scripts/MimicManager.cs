using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicManager : MonoBehaviour
{
    [SerializeField] List<GameObject> chests;
    [SerializeField] List<GameObject> goldImg;
    [SerializeField] List<GameObject> mimicImg;
    [SerializeField] List<GameObject> minerImg1;
    [SerializeField] List<GameObject> minerImg2;
    [SerializeField] List<GameObject> minerImg3;

    List<bool> mimicList;

    /// <summary>
    /// ミミックかどうかを設定する
    /// </summary>
    /// <param name="isMimicList"></param>
    public void SetMimic(List<bool> isMimicList)
    {
        mimicList = isMimicList;

        for (int i = 0;i < chests.Count;i++)
        {
            chests[i].GetComponent<ChestTrigger>().isMimic = isMimicList[i];
        }
    }

    /// <summary>
    /// 宝箱の地図の結果を共有
    /// </summary>
    /// <param name="isLie"></param>
    public void SetImg(bool isLie,int playerID)
    {
        minerImg1[playerID].SetActive(true);
        minerImg2[playerID].SetActive(true);
        minerImg3[playerID].SetActive(true);

        for (int i = 0; i < chests.Count; i++)
        {
            if (isLie)
            {// ウソをつく場合
                if(chests[i].GetComponent<ChestTrigger>().isMimic)
                {// ミミックの場合
                    goldImg[i].SetActive(true);
                }
                else
                {// 宝箱の場合
                    mimicImg[i].SetActive(true);
                }
            }
            else
            {// ウソをつかない場合
                if (chests[i].GetComponent<ChestTrigger>().isMimic)
                {// ミミックの場合
                    mimicImg[i].SetActive(true);
                }
                else
                {// 宝箱の場合
                    goldImg[i].SetActive(true);
                }
            }
        }

    }
}
