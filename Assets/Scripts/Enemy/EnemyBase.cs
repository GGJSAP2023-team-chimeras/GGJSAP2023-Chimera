using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;
namespace Enemys
{
    public enum EnemyType
    {
        Kirin = 0,  //麒麟
        Kada,       //化蛇
        Baku,       //獏
    }
    public class EnemyBase : MonoBehaviour, IDamageble
    {
        public enum EnemyState
        {
            Walk,    //歩行
            Wait,    //待機
            Chase,   //追跡
            Damage,  //ダメージ
            Attack,  //攻撃
            Freeze,  //硬直
            Death    //死亡
        }
        //最大体力
        [SerializeField] private int maxHP = 0;
        //攻撃力
        [SerializeField] private int attackPoint = 5;
        //手に入れる体パーツ
        [SerializeField] private PartsType.BodyPartsType bodyPartsType = PartsType.BodyPartsType.Head;
        //体のパーツの種類
        [SerializeField] private PartsType.EachPartsType eachPartsType = PartsType.EachPartsType.Baku;
        //敵の種類
        [SerializeField] private EnemyType enemyType = EnemyType.Kada;
        private int attackAnimHash = Animator.StringToHash("Attack");
        private int walkAnimHash = Animator.StringToHash("WalkSpeed");
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ReceiveDamage(bool damageAnim, int damage)
        {

        }
        public void SetState(EnemyState state)
        {
            if (state == EnemyState.Death) return; 
        }
    }
}