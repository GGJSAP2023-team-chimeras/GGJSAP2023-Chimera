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
    [SerializeField] public AttackTarget Target = AttackTarget.Player;
    private string targetAttackTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        //攻撃を与える目標設定
        switch (Target)
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
    private void AddDamage(GameObject colObject)
    {
        if (colObject.CompareTag(targetAttackTag))
        {
            var damage = colObject.transform.parent.GetComponent<IDamageble>();
            if (damage != null)
            {
                damage.ReceiveDamage(true, attackPoint);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddDamage(collision.gameObject);
    }
    private void OnParticleCollision(GameObject other)
    {
        AddDamage(other);
    }
}
