using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager>
    {
        public GameObject ResultUIObject;

        // FIXME: EnemyTypeå^Ç…Ç∑ÇÈ
        public int BossEnemyType;

        /// <summary>
        /// ÉVÅ[ÉìëJà⁄Ç≈çÌèúÇµÇƒÇŸÇµÇ≠Ç»Ç¢
        /// </summary>
        protected override  void Awake()
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

        public void InitBattleScene()
        {
            Debug.Log(BossEnemyType);
        }

        public void FinishGame()
        {
            ResultUI.Instance.ShowResult();
        }
    }
}
