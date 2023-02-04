using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;
using Players;

namespace Enemys
{   
    public class EnemyBase : MonoBehaviour, IDamageble
    {
        public enum EnemyState
        {
            Move,        //歩行
            Fly,         //飛行
            Wait,        //待機
            Damage,      //ダメージ
            ShortAttack,  //近距離攻撃
            RangeAttack, //遠距離攻撃
            Freeze,      //硬直
            Death        //死亡
        }
        //最大体力
        [Range(5,50),SerializeField] protected int maxHP = 10;
        //攻撃力
        [Range(5,20),SerializeField] protected int attackPoint = 5;
        //手に入れる体パーツ
        [SerializeField] protected PartsType.BodyPartsType bodyPartsType = PartsType.BodyPartsType.Head;
        //体のパーツの種類
        [Header("敵の種類でもあり、パーツの型"),SerializeField] protected PartsType.EachPartsType eachPartsType = PartsType.EachPartsType.Baku;
        //最大待機時間
        [Range(0.2f,3.0f),SerializeField] protected float maxWaitTime = 1.0f;
        //遠距離攻撃で放つオブジェクト
        [SerializeField] private GameObject rangeAttackObject;
        [Range(1.0f,10.0f),SerializeField] private float walkSpeed = 5.0f;
        //プレイヤー
        protected Player player;
        //アニメーターハッシュ
        protected int armAttackAnimHash = Animator.StringToHash("ArmAttack");
        protected int headAttackAnimHash = Animator.StringToHash("HeadAttack");
        protected int walkAnimHash = Animator.StringToHash("WalkSpeed");
        protected int shortAttackAnimHash = Animator.StringToHash("ShortAttack");
        protected int rangeAttackAnimHash = Animator.StringToHash("RangeAttack");
        //攻撃された
        protected bool isAttack = false;
        //待機時間
        protected float waitTime = 0.0f;
        //凍結時間
        protected float freezeTime = 0.0f;
        protected float maxFreezeTime = 0.0f;
        //smoothdampで着く時間
        private float smoothTime = 1.5f;
        private float arrivalDistance = 0.1f;
        //今の位置
        private Vector2 startPosition;
        //目的地
        private Vector2 destination;
        //現在の状態
        private EnemyState enemyState;
        //smoothdampで進む速度
        private Vector2 vecocity = Vector2.zero;
        private Animator anim;
        //前フレームの位置
        private Vector3 beforePosition;

        void Start()
        {
            //　初期位置を設定
            startPosition = transform.position;
            SetDestination(startPosition);
        }
        void Update()
        {
            beforePosition = transform.position;
            if(beforePosition != transform.position)
            {
                anim.SetFloat(walkAnimHash, 1);
            }
            else
            {
                anim.SetFloat(walkAnimHash, 0);
            }
        }
        public void ReceiveDamage(bool damageAnim, int damage)
        {

        }
        public void SetState(EnemyState state)
        {
            enemyState = state;
            if (state == EnemyState.Death) return; 
            else if (state == EnemyState.Wait)
            {
                anim.SetFloat(walkAnimHash, 0.0f);
            }
            else if(state == EnemyState.Move)
            {
                startPosition = transform.position;
                CreateRandomPosition();
            }
            else if (state == EnemyState.RangeAttack)
            {

            }
            else if (state == EnemyState.ShortAttack)
            {

            }
            else if(state == EnemyState.Freeze)
            {

            }
            else if(state == EnemyState.Damage)
            {

            }
        }
        private void StateMove()
        {
            //死んだらパーツを出して死ぬ
            if (enemyState == EnemyState.Death)
            {
                //Instantiate(PartsManager.Instance., transform.position, Quaternion.identity);
            }
            else if (enemyState == EnemyState.Wait)
            {
                waitTime += Time.deltaTime;
                if (waitTime >= maxWaitTime)
                {
                    int random = UnityEngine.Random.Range(0, 10);
                    if (random >= 8)
                    {
                        SetState(EnemyState.RangeAttack);
                    }
                    else if (random >= 6)
                    {
                        SetState(EnemyState.ShortAttack);
                    }
                    else
                    {
                        SetState(EnemyState.Move);
                    }
                }
            }
            else if(enemyState == EnemyState.Move)
            {
                
            }
            else if(enemyState == EnemyState.RangeAttack)
            {
                //アニメーションカーブで行う
                if (Vector2.Distance(transform.position, destination) < arrivalDistance)
                {
                    //anim.SetTrigger(armAttackAnimHash);
                    SetState(EnemyState.Freeze);
                }
            }
            else if(enemyState == EnemyState.ShortAttack)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, walkSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, player.gameObject.transform.position) <= arrivalDistance)
                {
                    anim.SetTrigger(headAttackAnimHash);
                    SetState(EnemyState.Freeze);
                }
            }
            else if(enemyState == EnemyState.Freeze)
            {

            }
        }
        /// <summary>
        /// ランダムに目的地作成
        /// </summary>
        public void CreateRandomPosition()
        {
            //　ランダムなVector2の値を得る
            var randDestination = Random.insideUnitCircle * 10;
            //　現在地にランダムな位置を足して目的地とする
            SetDestination(startPosition + new Vector2(randDestination.x, randDestination.y));
        }

        /// <summary>
        /// 目的地を設定する
        /// </summary>
        /// <param name="position">目的地</param>
        public void SetDestination(Vector2 position)
        {
            destination = position;
        }

        /// <summary>
        /// 目的地を取得する
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDestination()
        {
            return destination;
        }
    }
}