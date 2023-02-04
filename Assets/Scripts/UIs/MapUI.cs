using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MapUI : MonoBehaviour
{
    // ルート選択ボタンのプレハブ
    public GameObject RouteButtonPrefab;
    // 前のルート選択の画像プレハブ
    // 選択しなくていいのでImageでやる
    public GameObject PrevRouteImagePrefab;
    // 通ったルートの画像
    public Sprite SelectedRouteSprite;

    // ボタンポジション
    // yだけ参照したい
    // xは、前とか現在で書き換えていく
    public List<RouteButtonPositions> RouteButtonPositionsList;

    [System.Serializable]
    public class RouteButtonPositions
    {
        public Vector3[] Positions;
    }

    private void Awake()
    {
        Manager.MapManager.Instance.InitMap();
        this.InitMapUI();
    }

    /// <summary>
    /// ルート選択がされたときの処理
    /// </summary>
    /// <param name="routeIdx">上から何個目か（0始まり）</param>
    public void OnPressRouteButton(int routeIdx)
    {
        Manager.MapManager.Instance.GoNext(routeIdx);
    }

    public void InitMapUI()
    {
        // 次のルート選択のボタン配置
        // 現在地点のやつにしたいので、xの調整
        var routes = Manager.MapManager.Instance.NumOfRoutes;
        var positions = RouteButtonPositionsList[routes - 1]
                        .Positions
                        .Select((v) => new Vector3(0, v.y, 0))
                        .ToArray();

        // デフォルトでの選択状態を簡単に選択するために、逆順でforを回す
        for (int i = positions.Length - 1; i >= 0; i--)
        {
            var idx = i;
            var buttonObj = Instantiate(RouteButtonPrefab,
                                        this.GetComponent<RectTransform>());
            buttonObj.GetComponent<RectTransform>().anchoredPosition = positions[i];

            var button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => OnPressRouteButton(idx));

            var prevRoutes = Manager.MapManager.Instance.NumOfPrevRoutes;

            // 前のルートに応じたルート選択可能判定
            var prevSelectRoute = Manager.MapManager.Instance.PrevRouteIndex;
            if (prevRoutes == 3 && routes == 2)
            {
                // i = i-1 or i だったらenaable
                button.interactable = i == prevSelectRoute - 1 || i == prevSelectRoute;
            }
            else if (prevRoutes == 2 && routes == 3)
            {
                // i = i or i+1 だったらenaable
                button.interactable = i == prevSelectRoute || i == prevSelectRoute + 1;
            }

            // FIXME: 実装やばすぎ
            if (button.interactable)
            {
                button.Select();
            }
        }

        // 初回は前の節ないのでリターン
        if (Manager.MapManager.Instance.NumOfPrevRoutes == 0)
        {
            return;
        }
        // 前の節を表示
        var prevPositions = RouteButtonPositionsList[Manager.MapManager.Instance.NumOfPrevRoutes - 1]
                .Positions
                .Select((v) => new Vector3(-240, v.y, 0))
                .ToArray();
        for (int i = 0; i < prevPositions.Length; i++)
        {
            var image = Instantiate(PrevRouteImagePrefab,
                                    this.transform);
            image.GetComponent<RectTransform>().anchoredPosition = prevPositions[i];
            if (i == Manager.MapManager.Instance.PrevRouteIndex)
            {
                // 前のルートで選択した節の画像を変える
                image.GetComponent<Image>().sprite = SelectedRouteSprite;
            }
        }
    }
}
