using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        // FIXME: 命名もう少しなんとかならんか
        // バトルが終わったとき（どちらかが勝った時）に呼ばれる
        public void StopGame()
        {
            // FIXME: IAttackable使って共通化したい
            FindObjectsOfType<AttackPoint>().ToList().ForEach(ap => ap.GetComponent<Collider2D>().enabled = false);
            FindObjectsOfType<Beam>().ToList().ForEach(b => b.GetComponent<Collider2D>().enabled = false);
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
