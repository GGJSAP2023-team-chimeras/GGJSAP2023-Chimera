using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="damageAnim">ダメージのアニメーションをするか</param>
    /// <param name="damage">ダメージ量</param>
    public void ReceiveDamage(bool damageAnim, int damage);
}
