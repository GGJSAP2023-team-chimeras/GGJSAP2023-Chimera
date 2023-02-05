using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager>
    {
        public GameObject ResultUIObject;

        // FIXME: EnemyType�^�ɂ���
        public BodyParts.PartsType.EachPartsType BossEnemyType;

        /// <summary>
        /// �V�[���J�ڂō폜���Ăق����Ȃ�
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
        /// �v���C���[���s�k�����Ƃ��ɌĂ΂��
        /// </summary>
        public void FinishGame()
        {
            ResultUI.Instance.ShowResult();
        }
    }
}
