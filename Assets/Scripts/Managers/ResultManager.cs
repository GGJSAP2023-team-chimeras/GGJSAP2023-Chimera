using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ResultManager : SingletonMonoBehaviour<ResultManager>
    {
        /// <summary>
        /// ƒV[ƒ“‘JˆÚ‚Åíœ‚µ‚Ä‚Ù‚µ‚­‚È‚¢
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
