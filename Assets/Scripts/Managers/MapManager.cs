using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MapManager : SingletonMonoBehaviour<MapManager>
    {
        // 次のルートの個数
        public int NumOfRoutes = 1;
        // そのルートの先にどのボスがいるかを格納
        // {Kirin, Kada} みたいな
        // FIXME: EnemyTypeにする
        public List<int> Routes;

        // 前のルートの個数
        public int NumOfPrevRoutes = 1;
        // 前のルートが上から何番目か
        public int PrevRouteIndex = 0;

        // mapuiから呼び出す
        public void InitMap()
        {
            Routes.Clear();

            // 何ステージ目かの判定
            if (ResultManager.Instance.NumOfLayers != 0 &&
                ResultManager.Instance.NumOfLayers % 5 == 0)
            {
                // 5ステージごとに中ボス戦
                NumOfRoutes = 1;
            }
            else
            {
                // 他はランダムに2か3
                NumOfRoutes = Random.Range(2, 4);
            }

            for (int i = 0; i < NumOfRoutes; i++)
            {
                // FIXME: とりあえず3種類で決め打ち
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

            // 層を進むとインクリメント
            ResultManager.Instance.NumOfLayers++;
            // 選択したルートを保持
            this.PrevRouteIndex = routeIndex;
            // ルートによってボスのタイプを変更
            BattleSceneManager.Instance.BossEnemyType = Routes[routeIndex];
            // シーン遷移
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.ChangeScene(1);
        }
    }
}
