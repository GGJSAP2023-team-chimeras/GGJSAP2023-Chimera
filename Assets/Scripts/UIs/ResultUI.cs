using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
using System.Linq;

// ResultUIオブジェクト(空オブジェクト)にアタッチ
public class ResultUI : SingletonMonoBehaviour<ResultUI>
{
    // ScoreManagerを参照し、リザルトをUIとして出す
    public GameObject ResultUIObject;
    public TMP_Text scoreText;

    // 最初に選択されているボタン
    public Button FirstSelectedButton;

    // リザルト出すときに鳴らす音
    public AudioClip ResultSound;
    public AudioSource Source;
    // 決定音
    public AudioClip DecisionSound;

    // フェードアウト用パネル
    public Image FadePanel;

    protected override void Awake()
    {
        base.Awake();
        // リザルトを隠す
        ResultUIObject.SetActive(false);

        FadePanel.gameObject.SetActive(false);
    }

    public void ShowResult()
    {
        FirstSelectedButton.Select();
        // 到達した層をスコアとして出す
        scoreText.text = Manager.ResultManager.Instance.NumOfLayers.ToString();
        ResultUIObject.SetActive(true);

        Source.loop = false;
        Source.clip = ResultSound;
        Source.Play();
    }

    // タイトルに戻る処理
    public void OnPressBackTitleButton()
    {
        StartCoroutine(SoundFinishCoroutine(() => Manager.SceneManager.ChangeScene(0)));
    }

    public IEnumerator SoundFinishCoroutine(Action action)
    {
        // ボタンを全部disabledに
        FindObjectsOfType<Button>().ToList().ForEach(button => button.interactable = false);
        Source.loop = false;
        Source.clip = DecisionSound;
        Source.Play();

        FadePanel.gameObject.SetActive(true);
        DOTween.ToAlpha(() => FadePanel.color, color => FadePanel.color = color, 1f, 1f);

        yield return new WaitForSeconds(DecisionSound.length);
        action();
    }
}
