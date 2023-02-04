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
    public override void HeadSkill(PartsType.EachPartsType headType = PartsType.EachPartsType.None)
    {
        base.HeadSkill(headType);
    }
    public override void BodySkill(PartsType.EachPartsType bodyType = PartsType.EachPartsType.None)
    {
        base.BodySkill(bodyType);
    }
    public override void FootSkill(PartsType.EachPartsType footType = PartsType.EachPartsType.None)
    {
        base.FootSkill(footType);
    }
}
