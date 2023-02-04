using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームプレイヤー（ウィンドウ）の設定
/// </summary>
public class GameViewManager : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }
}
