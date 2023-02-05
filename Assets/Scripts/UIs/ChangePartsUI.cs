using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BodyParts;

public class ChangePartsUI : SingletonMonoBehaviour<ChangePartsUI>
{
    public GameObject ChangePartsUIObject;
    public Button FirstSelectedButton;

    protected override void Awake()
    {
        base.Awake();
        ChangePartsUIObject.SetActive(false);
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
    }

    /// <summary>
    /// �p�[�c�̍����ւ��Łu�͂��v��I�񂾎�
    /// </summary>
    public void OnPressYesButton()
    {
        // TODO: �p�[�c����ւ�����

        Manager.SceneManager.ChangeScene(1);
    }

    public void OnPressNoButton()
    {
        ChangePartsUIObject.SetActive(false);
    }
}
