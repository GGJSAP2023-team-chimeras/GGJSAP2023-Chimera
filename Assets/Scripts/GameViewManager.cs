using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���v���C���[�i�E�B���h�E�j�̐ݒ�
/// </summary>
public class GameViewManager : MonoBehaviour
{
    private void Awake()
    {
# if UNITY_STANDALONE
        Screen.SetResolution(1280, 720, false);
# endif
    }
}
