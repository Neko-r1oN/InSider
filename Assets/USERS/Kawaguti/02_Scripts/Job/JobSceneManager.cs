using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSceneManager : MonoBehaviour
{
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    private bool InSiderJodge = true;  //内通者判定(動作確認用)

    //テキスト
    [SerializeField] GameObject InSider;
    [SerializeField] GameObject Excavator;
    [SerializeField] Text YourText;
    [SerializeField] Text InSiderText;
    [SerializeField] Text ExcavatorText;

    //初めの色
    [SerializeField] Color32 startColor = new Color32(255, 255, 255, 0);

    //プレイヤー
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject Player3;
    [SerializeField] GameObject Player4;
    [SerializeField] GameObject Player5;
    [SerializeField] GameObject Player6;

    private void Start()
    {
        //表示切り替え時間を指定
        _repeatSpan = 0.5f;  
        _timeElapsed = 0;

        JobJadge();

        //テキストカラーを透明にする
        YourText.color = startColor;
        InSiderText.color = startColor;
        ExcavatorText.color = startColor;

        Invoke("SceneChange", 3.0f);
    }
    private void Update()
    {
        _timeElapsed += Time.deltaTime;     //時間をカウントする

        //経過時間が繰り返す間隔を経過したら
        if (_timeElapsed >= _repeatSpan)
        {//時間経過でテキスト表示
            YourText.color = Color.Lerp(YourText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
        }    
        if (_timeElapsed >= _repeatSpan+1.0f)
        {//時間経過でテキスト表示(役職)
            InSiderText.color = Color.Lerp(InSiderText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
            ExcavatorText.color = Color.Lerp(ExcavatorText.color, new Color(1, 1, 1.0f, 1), 0.50f * Time.deltaTime);
        }
    }

    //内通者判定関数(サーバー完成次第撤去)
    private void JobJadge()
    {
        if (InSiderJodge == false)
        {
            InSider.SetActive(false);
            Excavator.SetActive(true);
        }
        if (InSiderJodge == true)
        {
            InSider.SetActive(true);
            Excavator.SetActive(false);
        }
    }
    //シーン切り替え
    public void SceneChange()
    {
        //SceneManager.LoadScene("");
    }
}
