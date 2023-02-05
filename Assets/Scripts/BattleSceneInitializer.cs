using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneInitializer : MonoBehaviour
{
    private void Awake()
    {
        Manager.BattleSceneManager.Instance.InitBattleScene();
    }
}
