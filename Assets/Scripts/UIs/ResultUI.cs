using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ResultUIオブジェクト(空オブジェクト)にアタッチ
public class ResultUI : SingletonMonoBehaviour<ResultUI>
{
    // ScoreManagerを参照し、リザルトをUIとして出す
    public GameObject ResultUIObject;
    public TMP_Text scoreText;

    protected override void Awake()
    {
        base.Awake();
        // リザルトを隠す
        ResultUIObject.SetActive(false);
    }

    public void ShowResult()
    {
        // 到達した層をスコアとして出す
        scoreText.text = Manager.ResultManager.Instance.NumOfLayers.ToString();
        ResultUIObject.SetActive(true);
    }

    // タイトルに戻る処理
    public void OnPressBackTitleButton()
    {
        Manager.SceneManager.ChangeScene(0);
    }
}
