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

    /// <summary>
    /// ゲームの初期化処理
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
