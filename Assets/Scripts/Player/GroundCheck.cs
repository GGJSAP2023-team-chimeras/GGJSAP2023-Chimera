using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemys;

namespace Players
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private Player player;
        //�n�ʂ̃^�O
        private string groundTag = "Ground";
        //�n�ʔ���
        private bool isGround = false;
        //�n�ʂɂ��Ă���
        private bool isGroundEnter, isGroundStay, isGroundExit;

        public bool IsGround()
        {
            if (isGroundEnter || isGroundStay)
            {
                isGround = true;
            }
            else if (isGroundExit)
            {
                isGround = false;
            }
            isGroundEnter = false;
            isGroundStay = false;
            isGroundExit = false;
            return isGround;
        }

        private void OnTriggerEnter2D(Collider2D Collision)
        {
            if (Collision.gameObject.CompareTag(groundTag))
            {
                isGroundEnter = true;
            }
            //if (Collision.gameObject.CompareTag("Enemy"))
            //{
            //    var enemyBase = Collision.gameObject.GetComponent<EnemyBase>();
            //    //����Â������̂ɑ΂��ē���Â�������ʒm����
            //    enemyBase.PlayerStepOn = true;        
            //    //�W�����v�����ʒu���L�^����
            //    player.JumpPos = transform.position.y; 
            //}
        }

        private void OnTriggerStay2D(Collider2D Collision)
        {
            if (Collision.gameObject.CompareTag(groundTag))
            {
                isGroundStay = true;
            }
        }

        private void OnTriggerExit2D(Collider2D Collision)
        {
            if (Collision.gameObject.CompareTag(groundTag))
            {
                isGroundExit = true;
            }
        }

    }
}