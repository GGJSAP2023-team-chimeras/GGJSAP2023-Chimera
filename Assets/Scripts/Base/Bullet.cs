using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

public class Bullet : MonoBehaviour
{
    //速度
    [SerializeField] private float speed = 15.0f;
    [SerializeField] private float destroyTime = 2.5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        //自分の向いている方向に進む
        Vector3 velocity = transform.rotation * new Vector3(speed, 0, 0);
        rb.velocity = new Vector2(velocity.x, velocity.y);
    }
}
