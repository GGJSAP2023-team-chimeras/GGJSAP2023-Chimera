using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Sprite bulletSprite;
    //速度
    [SerializeField] private float speed = 15.0f;
    //スケールの倍数
    [Range(0.75f, 1.35f), SerializeField] private float mulSize = 1.0f;
    public float MulSize { get { return mulSize; } set { mulSize = value; } }
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * mulSize;
        if (bulletSprite != null)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bulletSprite;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //自分の向いている方向に進む
        Vector3 velocity = transform.rotation * new Vector3(speed, 0, 0);
        rb.velocity = new Vector2(velocity.x, velocity.y);
    }    
}
