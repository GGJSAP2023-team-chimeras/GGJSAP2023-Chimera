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
        [SerializeField] protected PartsType.BodyPartsType bodyPartsType;
        //頭の種類
        [SerializeField] protected PartsType.EachPartsType headType = PartsType.EachPartsType.None;
        //身体の種類
        [SerializeField] protected PartsType.EachPartsType bodyType = PartsType.EachPartsType.None;
        //足の種類
        [SerializeField] protected PartsType.EachPartsType footType = PartsType.EachPartsType.None;
        //プレイヤー
        [SerializeField] protected Player player;
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
            //体の部位ごとに
            switch (bodyPartsType)
            {
                //頭
                case PartsType.BodyPartsType.Head:
                    HeadSkill(headType);
                    break;
                //身体
                case PartsType.BodyPartsType.Body:
                    BodySkill(bodyType);
                    break;
                //脚
                case PartsType.BodyPartsType.Foot:
                    FootSkill(footType);
                    break;
                default:
                    Debug.LogError("何かおかしい、身体パーツエラー");
                    break;
            }
        }
        public virtual void HeadSkill(PartsType.EachPartsType headType = PartsType.EachPartsType.None)
        {
            if (headType == PartsType.EachPartsType.None) return;
            Debug.Log(headType.ToString());
        }
        public virtual void BodySkill(PartsType.EachPartsType bodyType = PartsType.EachPartsType.None)
        {
            if (bodyType == PartsType.EachPartsType.None) return;
            Debug.Log(bodyType.ToString());
        }
        public virtual void FootSkill(PartsType.EachPartsType footType = PartsType.EachPartsType.None)
        {
            if (footType == PartsType.EachPartsType.None) return;
            Debug.Log(footType.ToString());
        }
    }
}