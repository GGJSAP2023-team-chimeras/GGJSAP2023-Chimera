using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;
namespace BodyParts
{
    /// <summary>
    /// ���̃N���X�������I�u�W�F�N�g�̓v���C���[�̎q�ɓ����
    /// </summary>
    public class KirinParts : Parts
    {
        //�傫�����Ń_���[�W��^����o��������I�u�W�F�N�g
        [SerializeField] private GameObject spawnSmogObject;
        //���ň����I�u�W�F�N�g
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