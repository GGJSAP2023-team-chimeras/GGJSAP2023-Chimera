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
        // FIXME: scoreManager����Ƃ��Ă���
        scoreText.text = "1";
    }

    // �^�C�g���ɖ߂鏈��
    public void OnPressBackTitleButton()
    {
        Manager.SceneManager.ChangeScene(0);
    }
}
