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
        public int NumOfPrevRoutes = 1;
        // �O�̃��[�g���ォ�牽�Ԗڂ�
        public int PrevRouteIndex = 0;

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
            else
            {
                // ���̓����_����2��3
                NumOfRoutes = Random.Range(2, 4);
            }

            for (int i = 0; i < NumOfRoutes; i++)
            {
                // FIXME: �Ƃ肠����3��ނŌ��ߑł�
                var rand = Random.Range(0, 3);
                Routes.Add(rand);
            }
        }

        public void OnSceneLoaded(UnityEngine.SceneManagement.Scene nextScene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Debug.Log(nextScene.name);
            Debug.Log(mode);
            BattleSceneManager.Instance.InitBattleScene();
        }

        public void GoNext(int routeIndex)
        {
            Debug.Log("next");
            Debug.Log(routeIndex);

            // �w��i�ނƃC���N�������g
            ResultManager.Instance.NumOfLayers++;
            // �I���������[�g��ێ�
            this.PrevRouteIndex = routeIndex;
            // ���[�g�ɂ���ă{�X�̃^�C�v��ύX
            BattleSceneManager.Instance.BossEnemyType = Routes[routeIndex];
            // �V�[���J��
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.ChangeScene(1);
        }
    }
}
