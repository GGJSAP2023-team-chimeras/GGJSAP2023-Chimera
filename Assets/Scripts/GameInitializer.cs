using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        InitGame();
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
}
