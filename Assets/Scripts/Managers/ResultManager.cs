using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ResultManager : SingletonMonoBehaviour<ResultManager>
    {
        /// <summary>
        /// �V�[���J�ڂō폜���Ăق����Ȃ�
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            if (this != Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        public int NumOfLayers = 0;
    }
}
