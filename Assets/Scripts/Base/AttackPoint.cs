using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

public class AttackPoint : MonoBehaviour
{
    public enum AttackTarget
    {
        Player,
        Enemy
    }
    //攻撃力
    [SerializeField] private int attackPoint;
    //攻撃の目標を設定
    [SerializeField] private AttackTarget attackTarget = AttackTarget.Player;
    private string targetAttackTag = "Player";
    private Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        //攻撃を与える目標設定
        switch (attackTarget)
        {
            case AttackTarget.Player:
                targetAttackTag = "Player";
                break;
            case AttackTarget.Enemy:
                targetAttackTag = "Enemy";
                break;
            default:
                Debug.LogError("攻撃の目標を設定してください。攻撃判定クラスのエラーです。");
                break;
        }
        bullet = GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        //攻撃の目標であった場合
        if (collision.gameObject.CompareTag(targetAttackTag))
        {
            var damage = collision.transform.parent.GetComponent<IDamageble>();
            if (damage != null)
            {
                damage.ReceiveDamage(true, attackPoint);
            }
        }
    }
}
