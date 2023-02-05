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


    // TODO: �p�[�c�������ɂƂ肽��
    /// <summary>
    /// �h���b�v�����p�[�c��\������
    /// </summary>
    /// <param name="bodyPartsType">�g�̂̂ǂ̃p�[�c��</param>
    public void ShowPartsUI(PartsType.BodyPartsType bodyPartsType)
    {
        ChangePartsUIObject.SetActive(true);
        FirstSelectedButton.Select();

        // ���݂̃p�[�c��\��
        var parts = Players.Player.BodyPartsTypes;
        BeforeModel.SetModelPartsAll(parts[0], parts[1], parts[2]);

        var newParts = parts;
        newParts[(int)dropBodyParts] = dropParts;

        AfterModel.SetModelPartsAll(parts[0], parts[1], parts[2]);
        AfterModel.SetModelParts(dropBodyParts, dropParts);
        // ���̃p�[�c
        var isCheck = SpriteModelChecker.GetCheckModel(newParts[0], newParts[1], newParts[2]);
        if (isCheck)
        {
            AfterModel.SetColor(Color.black);
        }
    }

    /// <summary>
    /// �p�[�c�̍����ւ��Łu�͂��v��I�񂾎�
    /// </summary>
    public void OnPressYesButton()
    {
        // TODO: �p�[�c����ւ�����
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
