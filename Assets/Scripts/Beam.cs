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
        Debug.Log(collision.tag);
        Debug.Log(collision.name);
        // FIXME: 本来はtostringではなく独自に処理を書いた方がいい
        if (collision.CompareTag(Target.ToString()))
        {
            collision.GetComponentInParent<IDamageble>().ReceiveDamage(false, AttackPoint);
        }
    }

    // FIXME: アニメーションの終了を検知したほうがいい
    public IEnumerator OnFinishBeam()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.transform.parent.gameObject);
    }
}
