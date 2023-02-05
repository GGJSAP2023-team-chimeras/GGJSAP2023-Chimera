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

    // TODO: �p�[�c�������ɂƂ肽��
    /// <summary>
    /// �h���b�v�����p�[�c��\������
    /// </summary>
    public void ShowPartsUI()
    {
        ChangePartsUIObject.SetActive(true);
        FirstSelectedButton.Select();
    }

    /// <summary>
    /// �p�[�c�̍����ւ��Łu�͂��v��I�񂾎�
    /// </summary>
    public void OnPressYesButton()
    {
        // TODO: �p�[�c����ւ�����
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
