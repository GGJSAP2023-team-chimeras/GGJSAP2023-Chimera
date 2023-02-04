using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ResultUI�I�u�W�F�N�g(��I�u�W�F�N�g)�ɃA�^�b�`
public class ResultUI : SingletonMonoBehaviour<ResultUI>
{
    // ScoreManager���Q�Ƃ��A���U���g��UI�Ƃ��ďo��
    public GameObject ResultUIObject;
    public TMP_Text scoreText;

    protected override void Awake()
    {
        base.Awake();
        // ���U���g���B��
        ResultUIObject.SetActive(false);
    }

    public void ShowResult()
    {
        // ���B�����w���X�R�A�Ƃ��ďo��
        scoreText.text = Manager.ResultManager.Instance.NumOfLayers.ToString();
        ResultUIObject.SetActive(true);
    }

    // �^�C�g���ɖ߂鏈��
    public void OnPressBackTitleButton()
    {
        Manager.SceneManager.ChangeScene(0);
    }
}
