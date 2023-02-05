using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryUI : MonoBehaviour
{
    public void OnPressBackTitleButton()
    {
        Manager.SceneManager.ChangeScene(0);
    }
}
