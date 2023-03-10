using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ResultManager : SingletonMonoBehaviour<ResultManager>
    {
        /// <summary>
        /// シーン遷移で削除してほしくない
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
