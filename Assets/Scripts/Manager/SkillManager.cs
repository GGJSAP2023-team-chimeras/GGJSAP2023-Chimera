using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMonoBehaviour<SkillManager>
{
    [NamedArray(new string[] { "麒麟", "鬿雀","獏" }),SerializeField] private float[] maxHeadCoolTime;
    public float[] MaxHeadSkillCoolTime { get { return maxHeadCoolTime; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
