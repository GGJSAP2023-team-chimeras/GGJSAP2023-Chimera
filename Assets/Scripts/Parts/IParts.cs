using BodyParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IParts
{
    public  void HeadSkill(PartsType.HeadType headType = PartsType.HeadType.None);
    public  void BodySkill(PartsType.BodyType bodyType = PartsType.BodyType.None);
    public  void FootSkill(PartsType.FootType footType = PartsType.FootType.None);
}
