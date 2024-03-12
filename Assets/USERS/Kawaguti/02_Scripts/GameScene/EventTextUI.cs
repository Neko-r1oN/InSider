using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる

public class EventTextUI : MonoBehaviour
{
    [SerializeField] Text eventText;    // イベント名のテキスト
    [SerializeField] Text supText;      // イベント内容の説明

    public IEnumerator PanelAnim(int eventID)
    {
        switch(eventID)
        {
            case 101:
                eventText.text = "『落石』イベント発生！！";
                supText.text = "頭上注意！！";
                break;
            case 102:
                eventText.text = "『デバフ』イベント発生！！";
                supText.text = "一定時間の間、意図した道に切り開けなくなる！！";
                break;
            case 103:
                eventText.text = "『敵の出現』イベント発生！！";
                supText.text = "敵が出現！逃げ回れ！！";
                break;
            case 104:
                eventText.text = "『バフ』イベント発生！！";
                supText.text = "一定時間の間、全てのプレイヤーのスタミナ消費量が減る";
                break;
            case 105:
                eventText.text = "『金の出現』イベント発生！！";
                supText.text = "一定時間の間、ランダムに金が降ってくる！！";
                break;
            case 106:
                eventText.text = "イベント発生！！";
                supText.text = "";
                break;
        }

        transform.rotation = Quaternion.Euler(90, 0, 0);

        yield return new WaitForSeconds(0.5f);

        yield return transform.DORotate(new Vector3(0, 0, 0), 3.0f).WaitForCompletion();
        yield return new WaitForSeconds(2.5f);
        transform.DORotate(new Vector3(90, 0, 0), 0.7f).OnComplete(SetActive);
    }

    private void SetActive()
    {
        this.gameObject.SetActive(false);
    }
}


