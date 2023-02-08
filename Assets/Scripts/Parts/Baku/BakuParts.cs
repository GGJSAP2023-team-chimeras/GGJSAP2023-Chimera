using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;
namespace BodyParts
{
    /// <summary>
    /// このクラスをつけたオブジェクトはプレイヤーの子に入れる
    /// </summary>
    public class BakuParts : Parts
    {
        //弾幕発生オブジェクト
        [SerializeField] private GameObject barrageObject;
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
            var obj = Instantiate(barrageObject, transform.position, Quaternion.identity);
            obj.GetComponent<Barrage>().BulletSize = spawnBulletSize;
        }
        public override void BodySkill(PartsType.EachPartsType bodyType = PartsType.EachPartsType.None)
        {
            base.BodySkill(bodyType);
            player.CanJumpFire = true;
        }
        public override void FootSkill(PartsType.EachPartsType footType = PartsType.EachPartsType.None)
        {
            base.FootSkill(footType);
        }
    }
}