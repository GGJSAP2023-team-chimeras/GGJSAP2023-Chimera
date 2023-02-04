using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePartsUI : SingletonMonoBehaviour<ChangePartsUI>
{
    public GameObject ChangePartsUIObject;
    public Button FirstSelectedButton;

    protected override void Awake()
    {
        base.Awake();
        ChangePartsUIObject.SetActive(false);
    }

    // TODO: パーツを引数にとりたい
    /// <summary>
    /// ドロップしたパーツを表示する
    /// </summary>
    public void ShowPartsUI()
    {
        ChangePartsUIObject.SetActive(true);
        FirstSelectedButton.Select();
    }

    /// <summary>
    /// パーツの差し替えで「はい」を選んだ時
    /// </summary>
    public void OnPressYesButton()
    {
        // TODO: パーツ入れ替え処理

        Manager.SceneManager.ChangeScene(1);
    }

    public void OnPressNoButton()
    {
        ChangePartsUIObject.SetActive(false);
    }
}
