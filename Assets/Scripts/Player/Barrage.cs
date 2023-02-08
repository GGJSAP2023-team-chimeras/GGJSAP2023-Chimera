using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    [Range(5,20),SerializeField] private int bulletCount = 20;
    [SerializeField] private GameObject bulletObject;
    private float bulletSize = 0.0f;
    public float BulletSize { get { return bulletSize; } set { bulletSize = value; } }
    //ç≈ëÂäpìx
    private float maxRadius = 360;
    //åªç›ÇÃäpìx
    private float degree = 0;
    //äpìxÇÃä‘äu
    private int between = 30;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletSpawn());
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator BulletSpawn()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (maxRadius >= degree)
            {
                degree = i * between;
            }
            var obj = Instantiate(bulletObject, transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, degree);
            obj.transform.localScale = Vector3.one * bulletSize;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
