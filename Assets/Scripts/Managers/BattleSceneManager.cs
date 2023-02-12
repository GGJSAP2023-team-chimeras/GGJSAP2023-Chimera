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

        // FIXME: �������������Ȃ�Ƃ��Ȃ��
        // �o�g�����I������Ƃ��i�ǂ��炩�����������j�ɌĂ΂��
        public void StopGame()
        {
            // FIXME: IAttackable�g���ċ��ʉ�������
            FindObjectsOfType<AttackPoint>().ToList().ForEach(ap => ap.GetComponent<Collider2D>().enabled = false);
            FindObjectsOfType<Beam>().ToList().ForEach(b => b.GetComponent<Collider2D>().enabled = false);
        }

        /// <summary>
        /// �v���C���[���s�k�����Ƃ��ɌĂ΂��
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
