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

    public void OnPressRouteButton(int routeIdx)
    {
        Manager.MapManager.Instance.GoNext(routeIdx);
    }

    public void InitMapUI()
    {
        // 次のルート選択のボタン配置
        // 現在地点のやつにしたいので、xの調整
        var positions = RouteButtonPositionsList[Manager.MapManager.Instance.NumOfRoutes - 1]
                        .Positions
                        .Select((v) => new Vector3(0, v.y, 0))
                        .ToArray();
        for (int i = 0; i < positions.Length; i++)
        {
            var idx = i;
            var button = Instantiate(RouteButtonPrefab,
                                        this.GetComponent<RectTransform>());
            button.GetComponent<RectTransform>().anchoredPosition = positions[i];
            button.GetComponent<Button>()
                .onClick
                .AddListener(() => OnPressRouteButton(idx));
            // 最初のボタンを選択状態に
            if (i == 0)
            {
                button.GetComponent<Button>().Select();
            }
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
