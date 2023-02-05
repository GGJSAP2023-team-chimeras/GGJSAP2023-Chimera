using BodyParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParts
{
    public  void HeadSkill(PartsType.EachPartsType headType = PartsType.EachPartsType.None);
    public  void BodySkill(PartsType.EachPartsType bodyType = PartsType.EachPartsType.None);
    public  void FootSkill(PartsType.EachPartsType footType = PartsType.EachPartsType.None);
}
