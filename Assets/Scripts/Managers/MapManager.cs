using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MapManager : SingletonMonoBehaviour<MapManager>
    {
        // ���̃��[�g�̌�
        public int NumOfRoutes = 1;
        // ���̃��[�g�̐�ɂǂ̃{�X�����邩���i�[
        // {Kirin, Kada} �݂�����
        // FIXME: EnemyType�ɂ���
        public List<int> Routes;

        // �O�̃��[�g�̌�
        public int NumOfPrevRoutes = 0;
        // �O�̃��[�g���ォ�牽�Ԗڂ�
        public int PrevRouteIndex = 0;

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

        // mapui����Ăяo��
        public void InitMap()
        {
            Routes.Clear();

            // ���X�e�[�W�ڂ��̔���
            if (ResultManager.Instance.NumOfLayers != 0 &&
                ResultManager.Instance.NumOfLayers % 5 == 0)
            {
                // 5�X�e�[�W���Ƃɒ��{�X��
                NumOfRoutes = 1;
            }
            else if(NumOfPrevRoutes == 2)
            {
                // �O�̃��[�g��2�������Ƃ���3
                NumOfRoutes = 3;
            }else if(NumOfPrevRoutes == 3)
            {
                // �O�̃��[�g��3�������Ƃ���2
                NumOfRoutes = 2;
            }
            else
            {
                // ����ȊO�̓����_����2��3
                NumOfRoutes = Random.Range(2, 4);
            }

            // ���ꂼ��̃��[�g�ɓG�̃^�C�v��U�蕪��
            for (int i = 0; i < NumOfRoutes; i++)
            {
                // FIXME: �Ƃ肠����3��ނŌ��ߑł�
                var rand = Random.Range(0, 3);
                Routes.Add(rand);
            }
        }

        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene nextScene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            BattleSceneManager.Instance.InitBattleScene();
        }

        public void GoNext(int routeIndex)
        {
            // �w��i�ނƃC���N�������g
            ResultManager.Instance.NumOfLayers++;
            // ���݂̃��[�g����O�̂��
            NumOfPrevRoutes = NumOfRoutes;
            // �I���������[�g��ێ�
            PrevRouteIndex = routeIndex;
            // ���[�g�ɂ���ă{�X�̃^�C�v��ύX
            BattleSceneManager.Instance.BossEnemyType = Routes[routeIndex];
            // �V�[���J��
            // FIXME: ������V�[�����Ă΂�邽�тɖ���Ă΂�Ă�
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.ChangeScene(2);
        }
    }
}
