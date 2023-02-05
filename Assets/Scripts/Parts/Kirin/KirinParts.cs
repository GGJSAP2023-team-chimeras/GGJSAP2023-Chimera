using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;
namespace BodyParts
{
    /// <summary>
    /// このクラスをつけたオブジェクトはプレイヤーの子に入れる
    /// </summary>
    public class KirinParts : Parts
    {
        //大きい煙でダメージを与える出現させるオブジェクト
        [SerializeField] private GameObject spawnSmogObject;
        //頭で扱うオブジェクト
        [SerializeField] private GameObject smogObject;
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
            smogObject.SetActive(true);
        }
        public override void BodySkill(PartsType.EachPartsType bodyType = PartsType.EachPartsType.None)
        {
            base.BodySkill(bodyType);
            if (player.JumpStart)
            {
                var obj = Instantiate(spawnSmogObject, player.transform.position, Quaternion.identity);
                Destroy(obj.gameObject, 0.3f);
                player.JumpStart = false;
            }
        }
        public override void FootSkill(PartsType.EachPartsType footType = PartsType.EachPartsType.None)
        {
            base.FootSkill(footType);
            player.CanDoubleJump = true;
        }
    }
}