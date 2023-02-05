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
        public int NumOfPrevRoutes = 0;
        // 前のルートが上から何番目か
        public int PrevRouteIndex = 0;

        /// <summary>
        /// シーン遷移で削除してほしくない
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
            else if(NumOfPrevRoutes == 2)
            {
                // 前のルートが2だったときは3
                NumOfRoutes = 3;
            }else if(NumOfPrevRoutes == 3)
            {
                // 前のルートが3だったときは2
                NumOfRoutes = 2;
            }
            else
            {
                // それ以外はランダムに2か3
                NumOfRoutes = Random.Range(2, 4);
            }

            // それぞれのルートに敵のタイプを振り分け
            for (int i = 0; i < NumOfRoutes; i++)
            {
                // FIXME: とりあえず3種類で決め打ち
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
            // 層を進むとインクリメント
            ResultManager.Instance.NumOfLayers++;
            // 現在のルート数を前のやつに
            NumOfPrevRoutes = NumOfRoutes;
            // 選択したルートを保持
            PrevRouteIndex = routeIndex;
            // ルートによってボスのタイプを変更
            BattleSceneManager.Instance.BossEnemyType = Routes[routeIndex];
            // シーン遷移
            // FIXME: あらゆるシーンが呼ばれるたびに毎回呼ばれてる
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.ChangeScene(2);
        }
    }
}
