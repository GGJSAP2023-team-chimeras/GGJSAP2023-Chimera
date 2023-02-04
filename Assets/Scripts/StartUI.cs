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

    /// <summary>
    /// �Q�[���̏���������
    /// </summary>
    public void InitGame()
    {
        Manager.ResultManager.Instance.NumOfLayers = 0;
        Manager.MapManager.Instance.NumOfPrevRoutes = 0;
        Manager.MapManager.Instance.PrevRouteIndex = 0;
    }

    public void OnPressStartButton()
    {
        Manager.SceneManager.ChangeScene(1);
    }

    public void OnPressBookButton()
    {
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
