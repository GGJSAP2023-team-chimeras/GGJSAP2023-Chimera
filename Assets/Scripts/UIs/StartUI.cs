using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class StartUI : MonoBehaviour
{
    /// <summary>
    /// 終了ボタン押したときに確認用モーダルを出す？
    /// </summary>
    public GameObject ConfirmDialogue;
    public Button FirstSelectedButton;

    // 決定音
    public AudioClip DecisionSound;
    public AudioSource Source;

    // フェードアウト用パネル
    public Image FadePanel;

    private void Awake()
    {
        ConfirmDialogue.SetActive(false);
        FirstSelectedButton.Select();

        FadePanel.gameObject.SetActive(false);
    }

    public void OnPressStartButton()
    {
        StartCoroutine(SoundFinishCoroutine(
            () => Manager.SceneManager.ChangeScene(1)
            ));
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

    public void OnPressBookButton()
    {
        StartCoroutine(SoundFinishCoroutine(
            () => Manager.SceneManager.ChangeScene(3)
            ));

    }

    public void OnPressExitButton()
    {
        //// 一回確認用モーダル出す
        //ConfirmDialogue.SetActive(true);
        // TODO: 一旦モーダル出さずにQuit処理
        StartCoroutine(SoundFinishCoroutine(
            () => OnPressDialogueExitButton()
            ));
    }

    public void OnPressDialogueExitButton()
    {
        Application.Quit();
    }
}
