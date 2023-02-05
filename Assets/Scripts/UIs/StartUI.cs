using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    /// <summary>
    /// �I���{�^���������Ƃ��Ɋm�F�p���[�_�����o���H
    /// </summary>
    public GameObject ConfirmDialogue;
    public Button FirstSelectedButton;

    private void Awake()
    {
        ConfirmDialogue.SetActive(false);
        FirstSelectedButton.Select();
    }

    public void OnPressStartButton()
    {
        Manager.SceneManager.ChangeScene(1);
    }

    public void OnPressBookButton()
    {
        Manager.SceneManager.ChangeScene(3);

        Debug.Log("press book");
    }

    public void OnPressExitButton()
    {
        //// ���m�F�p���[�_���o��
        //ConfirmDialogue.SetActive(true);
        // TODO: ��U���[�_���o������Quit����
        OnPressDialogueExitButton();
    }

    public void OnPressDialogueExitButton()
    {
        Application.Quit();
    }
}
