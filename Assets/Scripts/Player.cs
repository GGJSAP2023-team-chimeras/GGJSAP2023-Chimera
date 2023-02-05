using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BodyParts;

namespace Players
{
    /// <summary>
    /// プレイヤーの基底クラス
    /// </summary>
    public class Player : MonoBehaviour,IDamageble
    {
        #region//インスペクターで設定する
        [Header("歩行速度"), SerializeField] private float walkSpeed;
        [Header("ジャンプ速度"), SerializeField] private float jumpSpeed;
        [Header("重力"), SerializeField] private float gravityPower = -9.8f;
        [Header("ジャンプ制限時間"), SerializeField] private float jumpLimitTime;
        [Header("ジャンプの高さ"), SerializeField] private float jumpHeight;
        [Header("接地判定"), SerializeField] private GroundCheck ground;
        [Header("頭をぶつけた判定"), SerializeField] private GroundCheck head;
        [Header("ダッシュの速さ表現"), SerializeField] private AnimationCurve dashCurve;
        [Header("ジャンプの速さ表現"), SerializeField] private AnimationCurve jumpCurve;
        [Header("ジャンプする時に鳴らすSE"), SerializeField] private AudioClip jumpSE;
        [Header("やられた時のSE"), SerializeField] private AudioClip downSE;
        [Header("コンティニューしたときのSE"), SerializeField] private AudioClip continueSE;
        [Header("最大体力"), Range(10, 50), SerializeField] private int maxHP = 50;
        [Header("ダメージ時点滅持続時間"), Range(0.2f, 1.0f), SerializeField] private float maxFlashTime = 1.0f;
        #endregion
        #region//プライベート変数

        private Animator anim = null;
        private Rigidbody2D rb = null;
        private SpriteRenderer sr = null;
        //地面判定
        private bool isGround = false;
        //頭が天井に当たった
        private bool isHead = false;
        //ジャンプ中
        private bool isJump = false;
        //ダウン中
        private bool isDown = false;
        //コンティニュー
        private bool isDamage = false;
        //点滅続行時間
        private float flashTime = 0.0f;
        //絵が見えている時間
        private float blinkTime = 0.0f;
        //ジャンプしているときの位置
        private float jumpPos = 0.0f;
        //ジャンプの最大時間
        private float jumpTime = 0.0f;
        //速度上昇時間
        private float dashTime = 0.0f;
        //反転した場合の前回の方向
        private float beforeKey = 0.0f;
        //体力
        private int currentHP = 0;
        private Vector2 movePos = Vector2.zero;
        private GGJSAP2023A inputs;
        private int groundAnimHash = Animator.StringToHash("ground");
        private int walkAnimHash = Animator.StringToHash("Walk");
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
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            inputs.Player.Move.performed += OnMove;
            inputs.Player.Move.canceled += OnMove;
            currentHP = maxHP;
        }
        private void Update()
        {
            if (isDamage)
            {
                //明滅　ついている時に戻る
                if (blinkTime > 0.2f)
                {
                    sr.enabled = true;
                    blinkTime = 0.0f;
                }
                //明滅　消えているとき
                else if (blinkTime > 0.1f)
                {
                    sr.enabled = false;
                }
                //明滅　ついているとき
                else
                {
                    sr.enabled = true;
                }

                //maxFlashTime秒たったら明滅終わり
                if (flashTime > maxFlashTime)
                {
                    isDamage = false;
                    blinkTime = 0f;
                    flashTime = 0f;
                    sr.enabled = true;
                }
                else
                {
                    blinkTime += Time.deltaTime;
                    flashTime += Time.deltaTime;
                }
            }
        }
        void FixedUpdate()
        {
            //ダウンしていないとき、ゲームオーバーしていないとき
            if (!isDown/* && !GameManager.instance.isGameOver*/)
            {
                //接地判定を得る
                isGround = ground.IsGround();
                isHead = head.IsGround();

                //各種座用軸の速度を求める
                float xSpeed = GetXSpeed();
                float ySpeed = GetYSpeed();
                //アニメーション設定
                //SetAnimation();
                rb.velocity = new Vector2(xSpeed, ySpeed);
            }
            else
            {
                rb.velocity = new Vector2(0, gravityPower);
            }
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            movePos = context.ReadValue<Vector2>();
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
            return xSpeed;
        }

        /// <summary>
        /// Y成分で必要な計算をし、速度を返す。
        /// </summary>
        /// <returns>Y軸の速さ</returns>
        private float GetYSpeed()
        {
            float ySpeed = gravityPower;
            //地面にいるとき
            if (isGround)
            {
                if (movePos.y > 0)
                {
                    if (!isJump)
                    {
                        //ジャンプの音
                        //GameManager.instance.PlaySE(jumpSE);
                    }
                    ySpeed = jumpSpeed;
                    jumpPos = transform.position.y; //ジャンプした位置を記録する
                    isJump = true;
                    jumpTime = 0.0f;
                }
                else
                {
                    isJump = false;
                }
            }
            //ジャンプ中
            else if (isJump)
            {
                //上方向キーを押しているか
                bool pushUpKey = movePos.y > 0;
                //現在の高さが飛べる高さより下か
                bool canHeight = jumpPos + jumpHeight > transform.position.y;
                //ジャンプ時間が長くなりすぎてないか
                bool canTime = jumpLimitTime > jumpTime;

                if (pushUpKey && canHeight && canTime && !isHead)
                {
                    ySpeed = jumpSpeed;
                    jumpTime += Time.deltaTime;
                }
                else
                {
                    isJump = false;
                    jumpTime = 0.0f;
                }
            }

            if (isJump)
            {
                ySpeed *= jumpCurve.Evaluate(jumpTime);
            }
            return ySpeed;

        }
        /// <summary>
        /// アニメーションを設定する
        /// </summary>
        private void SetAnimation()
        {
            anim.SetBool("jump", isJump);
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
                    //anim.SetTrigger();
                }
                currentHP -= damage;
                if (!isDamage)
                    isDamage = true;
            }
        }

        /// <summary>
        /// コンティニューする
        /// </summary>
        public void ContinuePlayer()
        {
            isDown = false;
            isJump = false;
        }
    }
}