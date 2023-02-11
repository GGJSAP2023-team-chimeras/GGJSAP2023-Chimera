using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public AttackPoint.AttackTarget Target;
    public int AttackPoint = 10;

    private void Start()
    {
        StartCoroutine(OnFinishBeam());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // FIXME: �{����tostring�ł͂Ȃ��Ǝ��ɏ�������������������
        if (collision.CompareTag(Target.ToString()))
        {
            collision.GetComponentInParent<IDamageble>().ReceiveDamage(false, AttackPoint);
        }
    }

    // FIXME: �A�j���[�V�����̏I�������m�����ق�������
    public IEnumerator OnFinishBeam()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.transform.parent.gameObject);
    }
}
