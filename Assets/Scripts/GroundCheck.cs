using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
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