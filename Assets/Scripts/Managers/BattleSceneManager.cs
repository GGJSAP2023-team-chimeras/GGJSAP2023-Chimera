using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager>
    {
        public GameObject ResultUIObject;

        public BodyParts.PartsType.EachPartsType BossEnemyType;

        public bool IsFinishGame = false;

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
            if (IsFinishGame)
            {
                return;
            }
            ResultUI.Instance.ResultUIObject.SetActive(false);
            ResultUI.Instance.ShowResult();

            IsFinishGame = true;
        }
    }
}
