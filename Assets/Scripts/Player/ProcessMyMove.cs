using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProcessMyMove : MonoBehaviour
{
    //攻撃コライダーの配列
    [SerializeField] public CircleCollider2D[] AttackColliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AttackStart()
    {
        AttackColliders.ToList().ForEach(v => v.enabled = true);
        //AttackColliders.enabled = true;
    }
    private void AttackEnd()
    {
        AttackColliders.ToList().ForEach(v => v.enabled = false);
        //AttackColliders.enabled = false;
    }
}
