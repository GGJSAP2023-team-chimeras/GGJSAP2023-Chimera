using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    [SerializeField] private int bulletCount = 20;
    [SerializeField] private GameObject bulletObject;
    private float bulletSize = 0.0f;
    public float BulletSize { get { return bulletSize; } set { bulletSize = value; } }
    // Start is called before the first frame update
    void Start()
    {
        float radius = 360;
        float degree = 0;
        int between = 30;
        for (int i = 0; i < bulletCount; i++)
        {
            if(radius >= degree)
            {
                degree = i * between;
            }
            var obj = Instantiate(bulletObject, transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, degree);
            obj.transform.localScale = Vector3.one * bulletSize;
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
