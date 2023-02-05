using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager>
    {
        public GameObject ResultUIObject;

        // FIXME: EnemyTypeŒ^‚É‚·‚é
        public BodyParts.PartsType.EachPartsType BossEnemyType;

        /// <summary>
        /// ƒV[ƒ“‘JˆÚ‚Åíœ‚µ‚Ä‚Ù‚µ‚­‚È‚¢
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

        /// <summary>
        /// ƒvƒŒƒCƒ„[‚ª”s–k‚µ‚½‚Æ‚«‚ÉŒÄ‚Î‚ê‚é
        /// </summary>
        public void FinishGame()
        {
            ResultUI.Instance.ShowResult();
        }
    }
}
