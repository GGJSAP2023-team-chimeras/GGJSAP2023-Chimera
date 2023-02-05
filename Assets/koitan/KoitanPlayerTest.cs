using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanTest
{
    public class KoitanPlayerTest : MonoBehaviour
    {
        [SerializeField]
        Transform modelTransform;
        [SerializeField]
        Animator animator;
        [SerializeField]
        Rigidbody2D rb;
        [SerializeField]
        float speedX;
        [SerializeField]
        float speedY;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                // 右向き
                modelTransform.rotation = Quaternion.Euler(0, 180, 0);
                // 移動
                transform.position += Vector3.right * speedX * Time.deltaTime;
                // アニメーション
                animator.SetBool("Run", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                // 左向き
                modelTransform.rotation = Quaternion.Euler(0, 0, 0);
                // 移動
                transform.position += Vector3.left * speedX * Time.deltaTime;
                // アニメーション
                animator.SetBool("Run", true);
            }
            else
            {
                // アニメーション
                animator.SetBool("Run", false);
            }

            // ジャンプ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Ground", false);
                animator.Play("Jump");
                rb.velocity = new Vector2(0, speedY);
            }

            // ダメージ
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.Play("Damage");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.Play("Die");
            }

            // 腕攻撃
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(ArmAttack());
            }
        }

        private IEnumerator ArmAttack()
        {
            animator.SetLayerWeight(1, 1f);
            animator.Play("ArmAttack");
            yield return new WaitForSeconds(0.5f);
            animator.SetLayerWeight(1, 0f);
            animator.Play("ArmIdle");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            animator.SetBool("Ground", true);
        }
    }
}
