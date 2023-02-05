using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    /// <summary>
    /// 終了ボタン押したときに確認用モーダルを出す？
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
        //// 一回確認用モーダル出す
        //ConfirmDialogue.SetActive(true);
        // TODO: 一旦モーダル出さずにQuit処理
        OnPressDialogueExitButton();
    }

    public void OnPressDialogueExitButton()
    {
        Application.Quit();
    }
}
