﻿using System.Collections;
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
            Wait,        //待機
            Damage,      //ダメージ
            ShortAttack,  //近距離攻撃
            RangeAttack, //遠距離攻撃
            Freeze,      //硬直
            Death        //死亡
        }
        //最大体力
        [Range(5,50),SerializeField] protected int maxHP = 10;
        //手に入れる体パーツ
        [SerializeField] protected PartsType.BodyPartsType bodyPartsType = PartsType.BodyPartsType.Head;
        //体のパーツの種類
        [Header("敵の種類でもあり、パーツの型"),SerializeField] protected PartsType.EachPartsType eachPartsType = PartsType.EachPartsType.Baku;
        //最大待機時間
        [Range(0.2f,3.0f),SerializeField] protected float maxWaitTime = 1.0f;
        //遠距離攻撃で放つオブジェクト
        [SerializeField] private GameObject rangeAttackObject;
        [Range(1.0f,10.0f),SerializeField] private float walkSpeed = 5.0f;
        //攻撃をするために近距離に詰めに行く
        [SerializeField] private AnimationCurve shortAttackMove;
        //限界の範囲まで
        [SerializeField] private Transform leftTransform;
        [SerializeField] private Transform rightTransform;
        [SerializeField] private Transform UpTransform;
        [SerializeField] private Transform downTransform;
        [SerializeField] private HPGauge enemyGauge;
        //プレイヤーが敵を踏んだ
        protected bool playerStepOn =false;
        public bool PlayerStepOn { get { return playerStepOn; } set { playerStepOn = value; } }
        //プレイヤー
        protected Player player;
        //アニメーターハッシュ
        protected int armAttackAnimHash = Animator.StringToHash("ArmAttack");
        protected int headAttackAnimHash = Animator.StringToHash("HeadAttack");
        protected int walkAnimHash = Animator.StringToHash("WalkSpeed");
        protected int shortAttackAnimHash = Animator.StringToHash("ShortAttack");
        protected int rangeAttackAnimHash = Animator.StringToHash("RangeAttack");
        protected int damageAnimHash = Animator.StringToHash("Damage");
        //攻撃された
        protected bool isAttack = false;
        //待機時間
        protected float waitTime = 0.0f;
        //凍結時間
        protected float freezeTime = 0.0f;
        protected float maxFreezeTime = 2.0f;
        //近距離攻撃の最大追跡時間
        protected float chaseTime = 0.0f;
        protected float maxChaseTime = 2.0f;
        //攻撃する、待機するまでの目的地からの距離
        private float arrivalDistance = 0.1f;
        private bool isArrival = false;
        //今の位置
        private Vector2 startPosition;
        //目的地
        private Vector2 destination;
        //現在の状態
        private EnemyState enemyState;
        private int enemyHP = 0;
        private Animator anim;
        //前フレームの位置
        private Vector3 beforePosition;
        private Vector2 currentVelocity;
        private readonly float smoothTime = 2.5f;

        void Start()
        {
            //初期位置を設定
            startPosition = transform.position;
            SetDestination(startPosition);
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
            anim = GetComponent<Animator>();
        }
        void Update()
        {
            beforePosition = transform.position;
            /* if (anim != null)
            {
                if (beforePosition != transform.position)
                {
                    anim.SetFloat(walkAnimHash, 1);
                }
                else
                {
                    anim.SetFloat(walkAnimHash, 0);
                }
            }*/
            StateMove();
            //範囲内を出た
            if((transform.position.y < downTransform.position.y || transform.position.y > UpTransform.position.y)
                || (transform.position.x < leftTransform.position.x || transform.position.x > rightTransform.position.x))
            {
                CreateRandomPosition();
            }
        }
        public void ReceiveDamage(bool playDamageAnim, int damage)
        {
            //体力ゲージに反映
            enemyGauge.GaugeReduction(damage, enemyHP, maxHP);
            //体力を減少
            enemyHP -= damage;
            //ダメージ音
            //GameManager.Instance.PlaySE(damageSE);
            //ダメージを受けた時アニメーションするか
            if (playDamageAnim)
            {
                //ダメージのアニメーション
                anim.SetTrigger(damageAnimHash);
            }
            if (enemyHP <= 0)
            {
                SetState(EnemyState.Death);
            }
        }
        //状態設定
        public void SetState(EnemyState state)
        {
            enemyState = state;
            if (state == EnemyState.Death) return; 
            //待機や攻撃後の硬直
            else if (state == EnemyState.Wait || state == EnemyState.Freeze)
            {
                /*if(anim != null)
                    anim.SetFloat(walkAnimHash, 0.0f);*/
                transform.position = beforePosition;
                isArrival = true;
            }
            else if(state == EnemyState.Move || state == EnemyState.ShortAttack || state == EnemyState.RangeAttack)
            {
                //移動
                isArrival = false;
                startPosition = transform.position;
                //ランダムに設定して移動
                if (state == EnemyState.Move)
                {
                    arrivalDistance = 0.25f;
                    CreateRandomPosition();
                }
                else if (state == EnemyState.ShortAttack)
                {
                    //到着する距離を変更
                    arrivalDistance = 1.25f;
                }
                else if(state == EnemyState.RangeAttack) 
                {
                    var random = UnityEngine.Random.Range(0, 2);
                    //左か右の限界地ぎりぎりにランダムに移動
                    if(random == 1)
                    {
                        destination = new Vector2(leftTransform.position.x + 1.25f,player.transform.position.y);
                    }
                    else if (random == 0)
                    {
                        destination = new Vector2(rightTransform.position.x - 1.25f, player.transform.position.y);
                    }
                }
            }
            else if(state == EnemyState.Damage)
            {
                isArrival = true;
                SetState(EnemyState.Move);
            }
        }
        protected virtual void StateMove()
        {
            //死んだらパーツを出して死ぬ
            if (enemyState == EnemyState.Death)
            {
                ChangePartsUI.Instance.ShowPartsUI(bodyPartsType);
                Destroy(gameObject);
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
                //到着していない場合
                if (!isArrival)
                {
                    //ランダムに定めた目的地を移動する
                    transform.position = Vector2.MoveTowards(transform.position, destination, walkSpeed * Time.deltaTime);
                    Vector2 direction = new Vector3(destination.x,destination.y , 1) - transform.position;
                    var lookRotation = Quaternion.FromToRotation(Vector3.up, direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
                }
                if (Vector2.Distance(transform.position, destination) <= arrivalDistance)
                {
                    SetState(EnemyState.Wait);
                }
            }
            else if(enemyState == EnemyState.RangeAttack)
            {
                if (!isArrival)
                {
                    //ランダムに設定した目的地に向けて進む
                    transform.position = Vector2.MoveTowards(transform.position, destination, walkSpeed * Time.deltaTime);
                }
                //、止まったら攻撃を放つ
                if (Vector2.Distance(transform.position, destination) < arrivalDistance)
                {
                    /*if(anim != null)
                        anim.SetTrigger(armAttackAnimHash);*/
                    Instantiate(rangeAttackObject,transform.position,Quaternion.identity);
                    isArrival = true;
                    SetState(EnemyState.Freeze);
                }
            }
            else if(enemyState == EnemyState.ShortAttack)
            {
                if (!isArrival)
                {
                    chaseTime += Time.deltaTime;
                    if (chaseTime < maxChaseTime)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, walkSpeed * Time.deltaTime);
                    }
                    else
                    {
                        CreateRandomPosition();
                        SetState(EnemyState.Move);
                    }                        
                }
                //到着したら放つ
                if (Vector2.Distance(transform.position, player.gameObject.transform.position) <= arrivalDistance)
                {
                    /*if (anim != null)
                        anim.SetTrigger(headAttackAnimHash);*/
                    isArrival = true;
                    SetState(EnemyState.Freeze);
                }
            }
            else if(enemyState == EnemyState.Freeze)
            {
                freezeTime += Time.deltaTime;
                if(freezeTime >= maxFreezeTime)
                {
                    freezeTime = 0;
                    SetState(EnemyState.Move);
                }
            }
        }
        /// <summary>
        /// ランダムに目的地作成
        /// </summary>
        public void CreateRandomPosition()
        {            
            //　ランダムなVector2の値を得る
            var randDestination = new Vector2(UnityEngine.Random.Range(leftTransform.position.x,rightTransform.position.x),
                UnityEngine.Random.Range(downTransform.position.y, UpTransform.position.y));
            //　現在地にランダムな位置を足して目的地とする
            SetDestination(new Vector2(randDestination.x, randDestination.y));
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