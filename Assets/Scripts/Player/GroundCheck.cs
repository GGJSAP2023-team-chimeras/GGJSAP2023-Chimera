using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemys;

namespace Players
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private Player player;
        //地面のタグ
        private string groundTag = "Ground";
        //地面判定
        private bool isGround = false;
        //地面についている
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
            //    //踏んづけたものに対して踏んづけた事を通知する
            //    enemyBase.PlayerStepOn = true;        
            //    //ジャンプした位置を記録する
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