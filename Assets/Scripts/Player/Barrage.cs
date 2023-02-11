using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    public AttackPoint.AttackTarget Target;

    //発射するタイプ
    public enum FireType
    {
        FireFlower, //花火のように同時に出す
        Barrage,    //弾幕で徐々に
    }
    //発射する球数
    [SerializeField] private int bulletCount = 20;
    //放つ弾オブジェクト
    [SerializeField] private GameObject bulletObject;
    //放たれる弾のタイミングの種類
    [SerializeField] private FireType fireType = FireType.Barrage;
    //弾のサイズ
    [SerializeField]
    private float bulletSize = 0.15f;
    public float BulletSize { get { return bulletSize; } set { bulletSize = value; } }
    //最大角度
    private float maxRadius = 360;
    //現在の角度
    private float degree = 0;
    //角度の間隔
    private int between = 30;
    // Start is called before the first frame update
    void Start()
    {
        between = Mathf.FloorToInt(maxRadius / bulletCount);
        StartCoroutine(BulletSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator BulletSpawn()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            if (maxRadius > degree)
            {
                degree = i * between;
            }
            var obj = Instantiate(bulletObject, transform.position, Quaternion.identity);
            obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, obj.transform.rotation.y, degree);
            obj.transform.localScale = Vector3.one * bulletSize;
            obj.GetComponent<AttackPoint>().Target = this.Target;
            //徐々に出すタイプであれば待つ
            if (fireType == FireType.Barrage)
                yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}