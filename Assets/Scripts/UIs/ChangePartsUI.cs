using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePartsUI : SingletonMonoBehaviour<ChangePartsUI>
{
    public GameObject ChangePartsUIObject;
    public Button FirstSelectedButton;

    public BodyParts.PartsType.BodyPartsType dropBodyParts = BodyParts.PartsType.BodyPartsType.Body;
    public BodyParts.PartsType.EachPartsType dropParts = BodyParts.PartsType.EachPartsType.Kirin;

    protected override void Awake()
    {
        base.Awake();
        ChangePartsUIObject.SetActive(false);
        dropParts = Manager.BattleSceneManager.Instance.BossEnemyType;
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
        GameObject.FindWithTag("Player").GetComponent<Players.Player>().SetParts(dropBodyParts, dropParts);
        // FIXME: Debug
        //Manager.SceneManager.ChangeScene(1);
        ChangePartsUIObject.SetActive(false);

    }

    public void OnPressNoButton()
    {
        ChangePartsUIObject.SetActive(false);
    }
}
