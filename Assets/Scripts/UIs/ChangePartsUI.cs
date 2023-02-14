using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BodyParts;
using System.Linq;
using System;
using DG.Tweening;

public class ChangePartsUI : SingletonMonoBehaviour<ChangePartsUI>
{
    // ���艹
    public AudioClip DecisionSound;
    public AudioSource Source;

    // �t�F�[�h�A�E�g�p�p�l��
    public Image FadePanel;

    public GameObject ChangePartsUIObject;
    public Button FirstSelectedButton;

    public BodyParts.PartsType.BodyPartsType dropBodyParts = BodyParts.PartsType.BodyPartsType.Body;
    public BodyParts.PartsType.EachPartsType dropParts = BodyParts.PartsType.EachPartsType.Kirin;

    public SpriteModelChanger BeforeModel;
    public SpriteModelChanger AfterModel;

    protected override void Awake()
    {
        base.Awake();
        ChangePartsUIObject.SetActive(false);

        // �t�F�[�_�[���\����
        FadePanel.gameObject.SetActive(false);
    }

    public void DebugShowPartsUI()
    {
        ShowPartsUI(PartsType.BodyPartsType.Body);
    }


    // TODO: �p�[�c�������ɂƂ肽��
    /// <summary>
    /// �h���b�v�����p�[�c��\������
    /// </summary>
    /// <param name="bodyPartsType">�g�̂̂ǂ̃p�[�c��</param>
    public void ShowPartsUI(PartsType.BodyPartsType bodyPartsType)
    {
        dropParts = Manager.BattleSceneManager.Instance.BossEnemyType;
        dropBodyParts = bodyPartsType;
        ChangePartsUIObject.SetActive(true);
        FirstSelectedButton.Select();

        // ���݂̃p�[�c��\��
        var parts = Players.Player.BodyPartsTypes;
        BeforeModel.SetModelPartsAll(parts[0], parts[1], parts[2]);

        var newParts = (PartsType.EachPartsType[])parts.Clone();
        newParts[(int)dropBodyParts] = dropParts;

        AfterModel.SetModelPartsAll(parts[0], parts[1], parts[2]);
        AfterModel.SetModelParts(dropBodyParts, dropParts);
        // ���̃p�[�c
        var isCheck = SpriteModelChecker.GetCheckModel(newParts[0], newParts[1], newParts[2]);
        if (!isCheck)
        {
            AfterModel.SetColor(Color.black);
        }
    }

    /// <summary>
    /// �p�[�c�̍����ւ��Łu�͂��v��I�񂾎�
    /// </summary>
    public void OnPressYesButton()
    {
        StartCoroutine(
            SoundFinishCoroutine(
                () =>
                    {
                        //GameObject.FindWithTag("Player").GetComponent<Players.Player>().SetParts(dropBodyParts, dropParts);
                        FindObjectOfType<Players.Player>().SetParts(dropBodyParts, dropParts);
                        Manager.SceneManager.ChangeScene(1);
                    }
                )
            );
    }

    /// <summary>
    /// �p�[�c�̍����ւ��Łu�������v��I�񂾎�
    /// </summary>
    public void OnPressNoButton()
    {
        StartCoroutine(
            SoundFinishCoroutine(() => Manager.SceneManager.ChangeScene(1))
            );
    }

    public IEnumerator SoundFinishCoroutine(Action action)
    {
        // �{�^����S��disabled��
        FindObjectsOfType<Button>().ToList().ForEach(button => button.interactable = false);

        FadePanel.gameObject.SetActive(true);
        DOTween.ToAlpha(() => FadePanel.color, color => FadePanel.color = color, 1f, 1f);

        Source.loop = false;
        Source.clip = DecisionSound;
        Source.Play();

        yield return new WaitForSeconds(DecisionSound.length);

        action();
    }
}
