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
                // �E����
                modelTransform.rotation = Quaternion.Euler(0, 180, 0);
                // �ړ�
                transform.position += Vector3.right * speedX * Time.deltaTime;
                // �A�j���[�V����
                animator.SetBool("Run", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                // ������
                modelTransform.rotation = Quaternion.Euler(0, 0, 0);
                // �ړ�
                transform.position += Vector3.left * speedX * Time.deltaTime;
                // �A�j���[�V����
                animator.SetBool("Run", true);
            }
            else
            {
                // �A�j���[�V����
                animator.SetBool("Run", false);
            }

            // �W�����v
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Ground", false);
                animator.Play("Jump");
                rb.velocity = new Vector2(0, speedY);
            }

            // �_���[�W
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.Play("Damage");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.Play("Die");
            }

            // �r�U��
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
