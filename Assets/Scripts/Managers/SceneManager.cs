using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public static class SceneManager
    {
        public static void ChangeScene(int sceneIdx)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIdx);
        }
    }
}
