using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class LibraryUI : MonoBehaviour
{
    public Button FirstSelectedButton;

    // 決定音
    public AudioSource Source;
    public AudioClip DecisionSound;

    // フェードアウト用パネル
    public Image FadePanel;

    private void Awake()
    {
        FirstSelectedButton.Select();
        FadePanel.gameObject.SetActive(false);
    }

    public void OnPressBackTitleButton()
    {
        // FIXME: actionをとるようにして一つにまとめたい
        StartCoroutine(SoundFinishCoroutine());
    }

    public IEnumerator SoundFinishCoroutine()
    {
        FindObjectsOfType<Button>().ToList().ForEach(button => button.interactable = false);

        Source.loop = false;
        Source.clip = DecisionSound;
        Source.Play();

        FadePanel.gameObject.SetActive(true);
        DOTween.ToAlpha(() => FadePanel.color, color => FadePanel.color = color, 1f, 1f);

        yield return new WaitForSeconds(DecisionSound.length);
        Manager.SceneManager.ChangeScene(0);
    }
}
