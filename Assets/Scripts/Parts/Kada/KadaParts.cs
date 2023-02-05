using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;
namespace BodyParts
{
    /// <summary>
    /// このクラスをつけたオブジェクトはプレイヤーの子に入れる
    /// </summary>
    public class KadaParts : Parts
    {
        [SerializeField] private GameObject beamObject;
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
            player.GravityPower = player.BeforeGravityPower * 1.25f;
        }
        public override void FootSkill(PartsType.EachPartsType footType = PartsType.EachPartsType.None)
        {
            base.FootSkill(footType);
            player.WalkSpeed = player.BeforeWalkSpeed * 1.25f;
        }
    }
}