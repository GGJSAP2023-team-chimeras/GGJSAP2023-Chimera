﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BodyParts;
using System;
using System.Linq;

namespace Players
{
    /// <summary>
    /// プレイヤーの基底クラス
    /// </summary>
    public class Player : MonoBehaviour, IDamageble
    {
        // 体の部位を設定
        [SerializeField]
        GameObject[] heads;
        [SerializeField]
        GameObject[] bodys;
        [SerializeField]
        GameObject[] legs;

        #region//インスペクターで設定する
        [Header("ジャンプ速度"), SerializeField] private float jumpPower;
        [Header("ジャンプ制限時間"), SerializeField] private float jumpLimitTime;
        [Header("ジャンプの高さ"), SerializeField] private float jumpHeight;
        [Header("接地判定"), SerializeField] private GroundCheck ground;
        [Header("頭をぶつけた判定"), SerializeField] private GroundCheck head;
        [Header("ダッシュの速さ表現"), SerializeField] private AnimationCurve dashCurve;
        [Header("ジャンプする時に鳴らすSE"), SerializeField] private AudioClip jumpSE;
        [Header("やられた時のSE"), SerializeField] private AudioClip downSE;
        [Header("コンティニューしたときのSE"), SerializeField] private AudioClip continueSE;
        [Header("最大体力"), Range(10, 50), SerializeField] private int maxHP = 50;
        [Header("ダメージ時点滅持続時間"), Range(0.2f, 1.0f), SerializeField] private float maxFlashTime = 1.0f;
        //体の部位ごとに
        [NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] public static PartsType.EachPartsType[] BodyPartsTypes = new PartsType.EachPartsType[Enum.GetValues(typeof(PartsType.EachPartsType)).Length];
        //体の部位のスプライト
        [NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] private SpriteRenderer[] spriteBodyRenderer = new SpriteRenderer[Enum.GetValues(typeof(PartsType.BodyPartsType)).Length];
        [SerializeField] private HPGauge playerGauge;

        [Header("歩行速度"), SerializeField] private float walkSpeed;
        public float WalkSpeed { get { return walkSpeed; } set { walkSpeed = value; } }
        [Header("重力"), SerializeField] private float gravityPower = -9.8f;
        public float GravityPower { get { return gravityPower; } set { gravityPower = value; } }
        #endregion
        #region プロパティ

        //ジャンプしているときの位置
        private float jumpPos = 0.0f;
        public float JumpPos { get { return jumpPos; } set { jumpPos = value; } }

        //踏んだ時のジャンプか
        private bool isAnotherJump;
        public bool IsAnotherJump { get { return isAnotherJump; } set { isAnotherJump = value; } }

        //踏んだ時にはねる
        private float anotherJumpHeight = 5.0f;
        public float AnotherJumpHeight { get { return anotherJumpHeight; } set { anotherJumpHeight = value; } }
        private bool canDoubleJump = false;
        public bool CanDoubleJump { get { return canDoubleJump; } set { canDoubleJump = value; } }
        //ジャンプ開始
        private bool jumpStart = false;
        public bool JumpStart { get { return jumpStart; } set { jumpStart = value; } }

        //元の重力
        private float beforeGravityPower = 0.0f;
        public float BeforeGravityPower { get { return beforeGravityPower; } }
        //元の歩行速度
        private float beforeWalkSpeed = 0.0f;
        public float BeforeWalkSpeed { get { return beforeWalkSpeed; } }

        //ジャンプ中でも攻撃を放てる
        private bool canJumpFire = false;
        public bool CanJumpFire { get { return canJumpFire; } set { canJumpFire = value; } }
        #endregion
        #region//プライベート変数
        //各アクティブスキル
        private bool activeHeadSkill = false;
        //ジャンプ中
        private bool isJump = false;
        //ダブルジャンプ中
        private bool doubleJump = false;
        //攻撃を放てる
        private bool isFire = false;
        private Animator anim = null;
        private Rigidbody2D rb = null;
        private SpriteRenderer sr = null;
        //地面判定
        private bool isGround = false;
        //頭が天井に当たった
        private bool isHead = false;
        //ダウン中
        private bool isDown = false;
        //コンティニュー
        private bool isDamage = false;
        //点滅続行時間
        private float flashTime = 0.0f;
        //絵が見えている時間
        private float blinkTime = 0.0f;
        //ジャンプの最大時間
        private float jumpTime = 0.0f;
        //速度上昇時間
        private float dashTime = 0.0f;
        //反転した場合の前回の方向
        private float beforeKey = 0.0f;
        //体力
        private int currentHP = 0;
        //移動インプット
        private Vector2 movePos = Vector2.zero;
        //プレイヤーインプットシステム
        private GGJSAP2023A inputs;
        //アニメーションアニメーターハッシュ
        private int groundAnimHash = Animator.StringToHash("ground");
        private int walkAnimHash = Animator.StringToHash("Walk");
        private int damageAnimHash = Animator.StringToHash("Damage");
        private int jumpAnimHash = Animator.StringToHash("Jump");
        #endregion
        protected virtual void Awake()
        {
            // Input Actionインスタンス生成
            inputs = new GGJSAP2023A();
        }
        protected virtual void OnEnable()
        {
            // 有効化する必要がある
            inputs.Enable();
        }
        void Start()
        {
            //コンポーネントのインスタンスを捕まえる
            //anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            inputs.Player.Move.performed += OnMove;
            inputs.Player.Move.canceled += OnMove;
            inputs.Player.Fire.performed += OnFire;
            inputs.Player.Fire.canceled += OnFire;
            inputs.Player.Jump.started += OnJump;
            inputs.Player.Attack.performed += OnAttack;
            currentHP = maxHP;
            beforeGravityPower = gravityPower;
            beforeWalkSpeed = walkSpeed;

            // パーツの初期化処理
            for (int i = 0; i < BodyPartsTypes.Length; i++)
            {
                Debug.Log(BodyPartsTypes[i]);
                SetParts((PartsType.BodyPartsType)i, BodyPartsTypes[i]);
            }
        }
        private void Update()
        {
            if (isDamage)
            {
                flashTime += Time.deltaTime;
                //maxFlashTime秒たったら明滅終わり
                if (flashTime > maxFlashTime)
                {
                    isDamage = false;
                }
            }
            //PartsPickUP();
        }
        void FixedUpdate()
        {
            //ダウンしていないとき、ゲームオーバーしていないとき
            if (!isDown/* && !GameManager.instance.isGameOver*/)
            {
                //接地判定を得る
                isGround = ground.IsGround();
                isHead = head.IsGround();

                //各種座標軸の速度を求める
                float xSpeed = GetXSpeed();
                SearchLimitY();
                //アニメーション設定
                //SetAnimation();
                rb.velocity = new Vector2(xSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, gravityPower);
            }
            anim.SetBool("Ground", isGround);
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            if (!isDamage)
                movePos = context.ReadValue<Vector2>();
        }
        private void OnFire(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            isFire = true;
            if (context.canceled)
            {
                isFire = false;
            }
        }
        private void OnActiveHeadSkill(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!activeHeadSkill)
            {
                activeHeadSkill = true;
            }
            if (BodyPartsTypes[(int)PartsType.BodyPartsType.Head] == PartsType.EachPartsType.Kirin)
            {

            }
            else if (BodyPartsTypes[(int)PartsType.BodyPartsType.Head] == PartsType.EachPartsType.Kijaku)
            {

            }
        }
        private void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!jumpStart)
                {
                    jumpStart = true;
                    //ジャンプの音
                    //GameManager.instance.PlaySE(jumpSE);
                }
                if (isJump && canDoubleJump)
                {
                    doubleJump = true;
                }
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
        }
        /// <summary>
        /// 近距離
        /// </summary>
        /// <param name="context"></param>
        private void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(ArmAttack());
            }
        }
        /// <summary>
        /// X成分で必要な計算をし、速度を返す。
        /// </summary>
        /// <returns>X軸の速さ</returns>
        private float GetXSpeed()
        {
            float xSpeed;
            if (movePos.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                dashTime += Time.deltaTime;
                xSpeed = walkSpeed;
            }
            else if (movePos.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                dashTime += Time.deltaTime;
                xSpeed = -walkSpeed;
            }
            else
            {
                xSpeed = 0.0f;
                dashTime = 0.0f;
            }

            //前回の入力からダッシュの反転を判断して速度を変える
            if (movePos.x > 0 && beforeKey < 0)
            {
                dashTime = 0.0f;
            }
            else if (movePos.x < 0 && beforeKey > 0)
            {
                dashTime = 0.0f;
            }

            beforeKey = movePos.x;
            xSpeed *= dashCurve.Evaluate(dashTime);
            // FIXME: ここじゃない
            anim.SetFloat(walkAnimHash, dashCurve.Evaluate(dashTime));
            return xSpeed;
        }

        /// <summary>
        /// Y成分で必要な計算をし、高さ制限
        /// </summary>
        /// <returns>Y軸の速さ</returns>
        private void SearchLimitY()
        {
            //二段ジャンプした後に敵を踏むと通常より弱めになる。
            anotherJumpHeight = doubleJump ? 5.0f : 2.0f;
            //ジャンプ中に敵を踏んだ場合
            if (isAnotherJump)
            {
                //現在の高さが飛べる高さより上か
                bool canHeight = jumpPos + anotherJumpHeight <= transform.position.y;
                //ジャンプ時間が長くなっているか
                bool canTime = jumpLimitTime <= jumpTime;
                if (canHeight && canTime && !isHead)
                {
                    gravityPower = beforeGravityPower * 1.05f;
                }
            }
            //今の高さ
            else if (isGround)
            {
                jumpPos = transform.position.y; //ジャンプした位置を記録する
            }
            //ジャンプ中
            if (isJump)
            {
                //現在の高さが飛べる高さより上か
                bool canHeight = jumpPos + jumpHeight <= transform.position.y;
                //ジャンプ時間が長くなりすぎてるか
                bool canTime = jumpLimitTime <= jumpTime;

                if (canHeight && canTime && !isHead)
                {
                    gravityPower = beforeGravityPower * 1.05f;
                }
            }
        }
        /// <summary>
        /// アニメーションを設定する
        /// </summary>
        private void SetAnimation()
        {
            anim.SetBool(jumpAnimHash, isJump || isAnotherJump);
            anim.SetBool(groundAnimHash, isGround);
            anim.SetFloat(walkAnimHash, walkSpeed);
        }
        /// <summary>
        /// やられた時の処理 
        /// </summary>
        /// <param name="damageAnim"></param>
        public void ReceiveDamage(bool damageAnim, int damage)
        {
            if (isDown)
            {
                return;
            }
            else
            {
                if (damageAnim)
                {
                    //アニメーション再生

                    //anim.SetTrigger(damageAnimHash);
                }
                playerGauge.GaugeReduction(damage, currentHP, maxHP);
                currentHP -= damage;
                if (!isDamage)
                {
                    isDamage = true;
                }
                if (currentHP <= 0)
                {
                    Debug.Log("死亡");
                }
            }
        }

        //パーツが違うことによって出来る物をすべて無効に
        private void PartsSkillReset()
        {
            canDoubleJump = false;
            gravityPower = beforeGravityPower;
            walkSpeed = beforeWalkSpeed;
            canJumpFire = false;
            jumpStart = false;
        }
        /// <summary>
        /// コンティニューする
        /// </summary>
        public void ContinuePlayer()
        {
            isDown = false;
            isJump = false;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {

            //地面についたらジャンプ可能に
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJump = false;
                doubleJump = false;
                jumpStart = false;
                isAnotherJump = false;
            }
        }

        private IEnumerator ArmAttack()
        {
            anim.SetLayerWeight(1, 1f);
            anim.Play("ArmAttack");
            yield return new WaitForSeconds(0.5f);
            anim.SetLayerWeight(1, 0f);
            anim.Play("ArmIdle");
        }
        public void SetParts(PartsType.BodyPartsType bodyPartsType, PartsType.EachPartsType partsType)
        {
            BodyPartsTypes[(int)bodyPartsType] = partsType;
            SpriteModelChecker.SetCheckModel(BodyPartsTypes[0], BodyPartsTypes[1], BodyPartsTypes[2]);
            switch (bodyPartsType)
            {
                case PartsType.BodyPartsType.Head:
                    for (int i = 0; i < heads.Length; i++)
                    {
                        heads[i].SetActive(i == (int)partsType);
                    }
                    break;
                case PartsType.BodyPartsType.Body:
                    for (int i = 0; i < heads.Length; i++)
                    {
                        bodys[i].SetActive(i == (int)partsType);
                    }
                    break;
                case PartsType.BodyPartsType.Foot:
                    for (int i = 0; i < heads.Length; i++)
                    {
                        legs[i].SetActive(i == (int)partsType);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}