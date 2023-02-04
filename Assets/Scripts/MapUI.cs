using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MapUI : MonoBehaviour
{
    // ���[�g�I���{�^���̃v���n�u
    public GameObject RouteButtonPrefab;
    // �O�̃��[�g�I���̉摜�v���n�u
    // �I�����Ȃ��Ă����̂�Image�ł��
    public GameObject PrevRouteImagePrefab;
    // �ʂ������[�g�̉摜
    public Sprite SelectedRouteSprite;

    // �{�^���|�W�V����
    // y�����Q�Ƃ�����
    // x�́A�O�Ƃ����݂ŏ��������Ă���
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
        // ���̃��[�g�I���̃{�^���z�u
        // ���ݒn�_�̂�ɂ������̂ŁAx�̒���
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
            // �ŏ��̃{�^����I����Ԃ�
            if (i == 0)
            {
                button.GetComponent<Button>().Select();
            }
        }
        // �O�̐߂�\��
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
                // �O�̃��[�g�őI�������߂̉摜��ς���
                image.GetComponent<Image>().sprite = SelectedRouteSprite;
            }
        }
    }
}
