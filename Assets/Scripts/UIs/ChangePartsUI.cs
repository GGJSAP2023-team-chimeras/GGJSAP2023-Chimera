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
    // 決定音
    public AudioClip DecisionSound;
    public AudioSource Source;

    // フェードアウト用パネル
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

        // フェーダーを非表示に
        FadePanel.gameObject.SetActive(false);
    }

    public void DebugShowPartsUI()
    {
        ShowPartsUI(PartsType.BodyPartsType.Body);
    }


    // TODO: パーツを引数にとりたい
    /// <summary>
    /// ドロップしたパーツを表示する
    /// </summary>
    /// <param name="bodyPartsType">身体のどのパーツか</param>
    public void ShowPartsUI(PartsType.BodyPartsType bodyPartsType)
    {
        dropParts = Manager.BattleSceneManager.Instance.BossEnemyType;
        dropBodyParts = bodyPartsType;
        ChangePartsUIObject.SetActive(true);
        FirstSelectedButton.Select();

        // 現在のパーツを表示
        var parts = Players.Player.BodyPartsTypes;
        BeforeModel.SetModelPartsAll(parts[0], parts[1], parts[2]);

        var newParts = (PartsType.EachPartsType[])parts.Clone();
        newParts[(int)dropBodyParts] = dropParts;

        AfterModel.SetModelPartsAll(parts[0], parts[1], parts[2]);
        AfterModel.SetModelParts(dropBodyParts, dropParts);
        // 次のパーツ
        var isCheck = SpriteModelChecker.GetCheckModel(newParts[0], newParts[1], newParts[2]);
        if (!isCheck)
        {
            AfterModel.SetColor(Color.black);
        }
    }

    /// <summary>
    /// パーツの差し替えで「はい」を選んだ時
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
    /// パーツの差し替えで「いいえ」を選んだ時
    /// </summary>
    public void OnPressNoButton()
    {
        StartCoroutine(
            SoundFinishCoroutine(() => Manager.SceneManager.ChangeScene(1))
            );
    }

    public IEnumerator SoundFinishCoroutine(Action action)
    {
        // ボタンを全部disabledに
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
