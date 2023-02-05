using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BodyParts;
using System.Linq;

public class ChangePartsUI : SingletonMonoBehaviour<ChangePartsUI>
{
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
        dropParts = Manager.BattleSceneManager.Instance.BossEnemyType;
    }


    // TODO: パーツを引数にとりたい
    /// <summary>
    /// ドロップしたパーツを表示する
    /// </summary>
    /// <param name="bodyPartsType">身体のどのパーツか</param>
    public void ShowPartsUI(PartsType.BodyPartsType bodyPartsType)
    {
        ChangePartsUIObject.SetActive(true);
        FirstSelectedButton.Select();

        // 現在のパーツを表示
        var parts = Players.Player.BodyPartsTypes;
        BeforeModel.SetModelPartsAll(parts[0], parts[1], parts[2]);

        var newParts = parts;
        newParts[(int)dropBodyParts] = dropParts;

        AfterModel.SetModelPartsAll(parts[0], parts[1], parts[2]);
        AfterModel.SetModelParts(dropBodyParts, dropParts);
        // 次のパーツ
        var isCheck = SpriteModelChecker.GetCheckModel(newParts[0], newParts[1], newParts[2]);
        if (isCheck)
        {
            AfterModel.SetColor(Color.black);
        }
    }

    /// <summary>
    /// パーツの差し替えで「はい」を選んだ時
    /// </summary>
    public void OnPressYesButton()
    {
        // TODO: パーツ入れ替え処理
        GameObject.FindWithTag("Player").GetComponent<Players.Player>().SetParts(dropBodyParts, dropParts);
        // FIXME: Debug
        Manager.SceneManager.ChangeScene(1);
        ChangePartsUIObject.SetActive(false);

    }

    public void OnPressNoButton()
    {
        ChangePartsUIObject.SetActive(false);
    }
}
