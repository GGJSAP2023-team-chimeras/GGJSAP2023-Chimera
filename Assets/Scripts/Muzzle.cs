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
        // ターゲットを見つける
        // FIXME: 文字列化の処理を独自実装した方が早い
        var target = GameObject.FindGameObjectWithTag(Target.ToString());
        SpawnBullet(target.transform);
    }

    /// <summary>
    /// 弾を発射
    /// </summary>
    /// <param name="target">ターゲット</param>
    public void SpawnBullet(Transform target)
    {
        var dir = target.position - this.transform.position;
        var targetDirection = Quaternion.FromToRotation(Vector3.right, dir);
        var bullet = Instantiate(BigBallet, this.transform.position, targetDirection);
        // ターゲットの設定
        bullet.GetComponent<AttackPoint>().Target = this.Target;
        Destroy(gameObject);
    }
}
