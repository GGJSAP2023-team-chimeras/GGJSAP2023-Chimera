using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public AttackPoint.AttackTarget Target;
    public int AttackPoint = 10;

    public bool IsDamaged = false;
    public IDamageble TargetDamager;

    // �_���[�W��^����Ԋu
    [SerializeField]
    private float slipInterval = 1;
    // �O��̃_���[�W����̊Ԋu
    private float elapsedTime;

    private void Start()
    {
        StartCoroutine(OnFinishBeam());
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (IsDamaged && elapsedTime >= slipInterval)
        {
            TargetDamager.ReceiveDamage(false, AttackPoint);
            elapsedTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // FIXME: �{����tostring�ł͂Ȃ��Ǝ��ɏ�������������������
        if (collision.CompareTag(Target.ToString()))
        {
            TargetDamager = collision.GetComponentInParent<IDamageble>();
            IsDamaged = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.ToString()))
        {
            TargetDamager = null;
            IsDamaged = false;
            elapsedTime = 0;
        }
    }

    // FIXME: �A�j���[�V�����̏I�������m�����ق�������
    public IEnumerator OnFinishBeam()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.transform.parent.gameObject);
    }
}
