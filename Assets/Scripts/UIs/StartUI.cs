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
    /// �I���{�^���������Ƃ��Ɋm�F�p���[�_�����o���H
    /// </summary>
    public GameObject ConfirmDialogue;
    public Button FirstSelectedButton;

    // ���艹
    public AudioClip DecisionSound;
    public AudioSource Source;

    // �t�F�[�h�A�E�g�p�p�l��
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
        // �{�^����S��disabled��
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
        //// ���m�F�p���[�_���o��
        //ConfirmDialogue.SetActive(true);
        // TODO: ��U���[�_���o������Quit����
        StartCoroutine(SoundFinishCoroutine(
            () => OnPressDialogueExitButton()
            ));
    }

    public void OnPressDialogueExitButton()
    {
        Application.Quit();
    }
}
