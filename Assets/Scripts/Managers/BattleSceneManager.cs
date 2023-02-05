using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager>
    {
        public GameObject ResultUIObject;

        public BodyParts.PartsType.EachPartsType BossEnemyType;

        /// <summary>
        /// シーン遷移で削除してほしくない
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
        /// プレイヤーが敗北したときに呼ばれる
        /// </summary>
        public void FinishGame()
        {
            ResultUI.Instance.ResultUIObject.SetActive(false);
            ResultUI.Instance.ShowResult();
        }
    }
}
