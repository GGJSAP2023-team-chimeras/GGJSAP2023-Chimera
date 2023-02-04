using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;

public class KirinHead : Parts
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void HeadSkill(PartsType.HeadType headType = PartsType.HeadType.None)
    {
        base.HeadSkill(headType);
    }
    public override void BodySkill(PartsType.BodyType bodyType = PartsType.BodyType.None)
    {
        base.BodySkill(bodyType);
    }
    public override void FootSkill(PartsType.FootType footType = PartsType.FootType.None)
    {
        base.FootSkill(footType);
    }
}
