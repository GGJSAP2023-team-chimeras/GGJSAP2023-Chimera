using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class Muzzle : MonoBehaviour
{
    public GameObject BigBallet;
    public AttackPoint.AttackTarget Target;

    private void Start()
    {
        // �^�[�Q�b�g��������
        // FIXME: �����񉻂̏�����Ǝ�����������������
        var target = GameObject.FindGameObjectWithTag(Target.ToString());
        SpawnBullet(target.transform);
    }

    /// <summary>
    /// �e�𔭎�
    /// </summary>
    /// <param name="target">�^�[�Q�b�g</param>
    public void SpawnBullet(Transform target)
    {
        var dir = target.position - this.transform.position;
        var targetDirection = Quaternion.FromToRotation(Vector3.right, dir);
        var bullet = Instantiate(BigBallet, this.transform.position, targetDirection);
        // �^�[�Q�b�g�̐ݒ�
        bullet.GetComponent<AttackPoint>().Target = this.Target;
        Destroy(gameObject);
    }
}
