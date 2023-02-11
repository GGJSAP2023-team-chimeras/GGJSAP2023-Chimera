using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    public AttackPoint.AttackTarget Target;

    //���˂���^�C�v
    public enum FireType
    {
        FireFlower, //�ԉ΂̂悤�ɓ����ɏo��
        Barrage,    //�e���ŏ��X��
    }
    //���˂��鋅��
    [SerializeField] private int bulletCount = 20;
    //���e�I�u�W�F�N�g
    [SerializeField] private GameObject bulletObject;
    //�������e�̃^�C�~���O�̎��
    [SerializeField] private FireType fireType = FireType.Barrage;
    //�e�̃T�C�Y
    [SerializeField]
    private float bulletSize = 0.15f;
    public float BulletSize { get { return bulletSize; } set { bulletSize = value; } }
    //�ő�p�x
    private float maxRadius = 360;
    //���݂̊p�x
    private float degree = 0;
    //�p�x�̊Ԋu
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
            //���X�ɏo���^�C�v�ł���Α҂�
            if (fireType == FireType.Barrage)
                yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}