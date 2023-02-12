using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
using System.Linq;

// ResultUI�I�u�W�F�N�g(��I�u�W�F�N�g)�ɃA�^�b�`
public class ResultUI : SingletonMonoBehaviour<ResultUI>
{
    // ScoreManager���Q�Ƃ��A���U���g��UI�Ƃ��ďo��
    public GameObject ResultUIObject;
    public TMP_Text scoreText;

    // �ŏ��ɑI������Ă���{�^��
    public Button FirstSelectedButton;

    // ���U���g�o���Ƃ��ɖ炷��
    public AudioClip ResultSound;
    public AudioSource Source;
    // ���艹
    public AudioClip DecisionSound;

    // �t�F�[�h�A�E�g�p�p�l��
    public Image FadePanel;

    protected override void Awake()
    {
        base.Awake();
        // ���U���g���B��
        ResultUIObject.SetActive(false);

        FadePanel.gameObject.SetActive(false);
    }

    public void ShowResult()
    {
        FirstSelectedButton.Select();
        // ���B�����w���X�R�A�Ƃ��ďo��
        scoreText.text = Manager.ResultManager.Instance.NumOfLayers.ToString();
        ResultUIObject.SetActive(true);

        Source.loop = false;
        Source.clip = ResultSound;
        Source.Play();
    }

    // �^�C�g���ɖ߂鏈��
    public void OnPressBackTitleButton()
    {
        StartCoroutine(SoundFinishCoroutine(() => Manager.SceneManager.ChangeScene(0)));
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
}
