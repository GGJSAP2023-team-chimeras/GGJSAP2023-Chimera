using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

public class AttackPoint : MonoBehaviour
{
    //攻撃力
    [SerializeField] private int attackPoint;
    public enum AttackTarget
    {
        Player,
        Enemy
    }
    //攻撃の目標を設定
    [SerializeField] private AttackTarget attackTarget = AttackTarget.Player;
    private string targetAttackTag = "Player";
    private Bullet bullet;
    private Player player;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //攻撃の目標であった場合
        if (collision.gameObject.CompareTag(targetAttackTag))
        {
            var damage = collision.gameObject.GetComponent<IDamageble>();
            if (damage != null)
            {
                damage.ReceiveDamage(true, attackPoint);
                Destroy(gameObject);
            }
        }
        else
        {
            //自分が弾だった場合
            if(bullet != null)
                Destroy(gameObject);
        }
    }
}
