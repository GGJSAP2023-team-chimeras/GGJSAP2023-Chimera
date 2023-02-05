using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    [SerializeField] private int bulletCount = 20;
    [SerializeField] private GameObject bulletObject;
    // Start is called before the first frame update
    void Start()
    {
        float halfRadius = 180;
        float degree = 0;
        for (int i = 0; i < bulletCount; i++)
        {
            if(halfRadius >= degree)
            {
                degree = i * 30;
            }
            var obj = Instantiate(bulletObject, transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, degree);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
