using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public AttackPoint.AttackTarget Target;
    public int AttackPoint = 10;

    public bool IsDamaged = false;
    public IDamageble TargetDamager;

    // ダメージを与える間隔
    [SerializeField]
    private float slipInterval = 1;
    // 前回のダメージからの間隔
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
        // FIXME: 本来はtostringではなく独自に処理を書いた方がいい
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

    // FIXME: アニメーションの終了を検知したほうがいい
    public IEnumerator OnFinishBeam()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.transform.parent.gameObject);
    }
}
