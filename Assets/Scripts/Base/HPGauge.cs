using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HPGauge : MonoBehaviour
{
    //�F��ω�������I�u�W�F�N�g�ƐF
    [SerializeField] private Image hpBarFill;
    [SerializeField] private Color color_1, color_2, color_3, color_4;
    [SerializeField] private Image backGauge;
    //���̗̑̓Q�[�W
    [SerializeField] private Slider backHPSlider;
    //�_�ł��邩�ǂ���
    [SerializeField] private bool isFlash = false;
    [SerializeField] private Slider hpSlider;
    //dotween(���̗̑͂̃A�j���[�V����)
    private Tween backGaugeTween;
    private float valueFrom = 1;
    private float valueTo = 1;
    private float alpha_Sin = 0;
    //�ω�����X�s�[�h
    private float sinSpeed = 4;
    void Start()
    {

    }
    private void Update()
    {
        //�댯�Ȃ�_�ł�����
        if (valueTo < 0.2f && isFlash)
        {
            backGauge.color = Color.black;
            alpha_Sin = Mathf.Sin(Time.time * sinSpeed) / 2 + 0.5f;
            ColorCoroutine();
        }
    }
    /// <summary>
    /// �̗̓o�[�̐ݒ�
    /// </summary>
    /// <param name="reducationValue">�_���[�W�l</param>    
    /// <param name="currentHP">���݂̗̑�</param>
    /// <param name="currentMaxHP">���݂̍ő�̗�</param>
    /// <param name="delayTime">�w���Ō���̗̓o�[�̒x������</param>
    public void GaugeReduction(float reducationValue, int currentHP, int currentMaxHP, float delayTime = 0.5f)
    {
        //���݂̗̑�
        valueFrom = (float)currentHP / currentMaxHP;
        //��������̗̑�
        valueTo = (currentHP - reducationValue) / currentMaxHP;
        float mul = 4.0f;
        //�̗͂��������ꍇ
        if (0 <= currentHP && Time.time > 0.01f)
        {
            //1����n�܂��Ă�����x�ŉ�����
            //�F�ω�
            if (valueFrom > 0.75f)
            {
                hpBarFill.color = Color.Lerp(color_2, color_1, (valueFrom - 0.75f) * mul);
            }
            else if (valueFrom > 0.25f)
            {
                hpBarFill.color = Color.Lerp(color_3, color_2, (valueFrom - 0.25f) * mul);
            }
            else
            {
                hpBarFill.color = Color.Lerp(color_4, color_3, valueFrom * mul);
            }
            hpSlider.value = valueTo;
        }
        if (backGaugeTween != null)
        {
            backGaugeTween.Kill();
        }

        // �ԃQ�[�W����
        backGaugeTween = DOTween.To(
            () => valueFrom,
            x =>
            {
                backHPSlider.value = x;
            },
            valueTo,
            delayTime
        );
    }
    //�̗̓o�[��_��
    void ColorCoroutine()
    {
        Color _color = hpBarFill.color;

        _color.a = alpha_Sin;

        hpBarFill.color = _color;
    }
}