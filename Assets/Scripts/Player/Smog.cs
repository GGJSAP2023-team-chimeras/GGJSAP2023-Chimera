using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemys;

public class Smog : MonoBehaviour
{
    [SerializeField] private int attackPoint;
    //ÉXÉPÅ[ÉãÇÃî{êî
    [Range(0.75f,1.35f),SerializeField] private float mulSize = 1.0f;
    public float MulSize { get { return mulSize; } set { mulSize = value; } }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * mulSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var damage = collision.gameObject.GetComponent<IDamageble>();
            if (damage != null)
            {
                damage.ReceiveDamage(true,attackPoint);
            }
        }
    }
}
