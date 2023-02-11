using Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodyParts
{
    /// <summary>
    /// 各パーツの基底クラス
    /// </summary>
    public class Parts : MonoBehaviour,IParts
    {
        //体のどのパーツなのか
        [SerializeField] 
        protected PartsType.BodyPartsType bodyPartsType;
        //頭の種類
        [SerializeField]
        protected PartsType.EachPartsType eachPartsType = PartsType.EachPartsType.None;
        //プレイヤー
        [SerializeField] 
        protected Player player;
        //アクティブスキルかどうか
        [SerializeField]
        protected bool isActiveSkill = false;
        //頭の部位のスキルはアクティブスキルで弾幕のサイズを変更する可能性があるため
        protected float spawnBulletSize = 0.5f;
        public float BulletSize { get { return spawnBulletSize; } set { spawnBulletSize = value; } }
        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            EachParts();
        }
        //各パーツごとに行う
        private void EachParts()
        {
            //アクティブスキルではなかった場合
            if (!isActiveSkill)
            {
                //体の部位ごとに
                switch (bodyPartsType)
                {
                    //頭
                    case PartsType.BodyPartsType.Head:
                        HeadSkill(eachPartsType);
                        break;
                    //身体
                    case PartsType.BodyPartsType.Body:
                        BodySkill(eachPartsType);
                        break;
                    //脚
                    case PartsType.BodyPartsType.Foot:
                        FootSkill(eachPartsType);
                        break;
                    default:
                        Debug.LogError("何かおかしい、身体パーツエラー");
                        break;
                }
            }
        }
        /// <summary>
        /// 頭のスキル
        /// </summary>
        /// <param name="headType">頭の部位のタイプ</param>
        /// <param name="bulletSize">弾のサイズ</param>
        public virtual void HeadSkill(PartsType.EachPartsType headType = PartsType.EachPartsType.None)
        {
            if (headType == PartsType.EachPartsType.None) return;
        }
        public virtual void BodySkill(PartsType.EachPartsType bodyType = PartsType.EachPartsType.None)
        {
            if (bodyType == PartsType.EachPartsType.None) return;
        }
        public virtual void FootSkill(PartsType.EachPartsType footType = PartsType.EachPartsType.None)
        {
            if (footType == PartsType.EachPartsType.None) return;
        }
    }
}