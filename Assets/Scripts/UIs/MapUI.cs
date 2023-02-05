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

    // ���[�g�Ԃ̉摜
    public Sprite[] PathSprites;
    public Image PathImage;

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

    /// <summary>
    /// ���[�g�I�������ꂽ�Ƃ��̏���
    /// </summary>
    /// <param name="routeIdx">�ォ�牽�ڂ��i0�n�܂�j</param>
    public void OnPressRouteButton(int routeIdx)
    {
        Manager.MapManager.Instance.GoNext(routeIdx);
    }

    public void InitMapUI()
    {
        var routes = Manager.MapManager.Instance.NumOfRoutes;
        var prevRoutes = Manager.MapManager.Instance.NumOfPrevRoutes;
        var prevSelectRoute = Manager.MapManager.Instance.PrevRouteIndex;

        // �p�X�摜�I��
        if (prevRoutes == 0)
        {
            PathImage.sprite = null;
            PathImage.enabled = false;
        }
        else
        {
            PathImage.sprite = PathSprites.Where(s => s.name == $"{prevRoutes}_{routes}@2x").First();
        }
        // ���̃��[�g�I���̃{�^���z�u
        // ���ݒn�_�̂�ɂ������̂ŁAx�̒���
        var positions = RouteButtonPositionsList[routes - 1]
                        .Positions
                        .Select((v) => new Vector3(93.7f, v.y, 0))
                        .ToArray();

        // �f�t�H���g�ł̑I����Ԃ��ȒP�ɑI�����邽�߂ɁA�t����for����
        for (int i = positions.Length - 1; i >= 0; i--)
        {
            var idx = i;
            var buttonObj = Instantiate(RouteButtonPrefab,
                                        this.GetComponent<RectTransform>());
            buttonObj.GetComponent<RectTransform>().anchoredPosition = positions[i];

            var button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => OnPressRouteButton(idx));

            // �O�̃��[�g�ɉ��������[�g�I���\����
            if (prevRoutes == 3 && routes == 2)
            {
                // i = i-1 or i ��������enaable
                button.interactable = i == prevSelectRoute - 1 || i == prevSelectRoute;
            }
            else if (prevRoutes == 2 && routes == 3)
            {
                // i = i or i+1 ��������enaable
                button.interactable = i == prevSelectRoute || i == prevSelectRoute + 1;
            }

            // FIXME: ������΂���
            if (button.interactable)
            {
                button.Select();
            }
        }

        // ����͑O�̐߂Ȃ��̂Ń��^�[��
        if (Manager.MapManager.Instance.NumOfPrevRoutes == 0)
        {
            return;
        }
        // �O�̐߂�\��
        var prevPositions = RouteButtonPositionsList[Manager.MapManager.Instance.NumOfPrevRoutes - 1]
                .Positions
                .Select((v) => new Vector3(-79f, v.y, 0))
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
