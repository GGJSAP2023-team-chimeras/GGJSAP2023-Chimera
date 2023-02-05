using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodyParts
{
    public class PartsManager : SingletonMonoBehaviour<PartsManager>
    {
        [Header("麒麟"), NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] private GameObject[] kirinBodyParts;
        [Header("化蛇"), NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] private GameObject[] kadaBodyParts;
        [Header("獏"), NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] private GameObject[] bakuBodyParts;
        [Header("麒麟のスプライト"), NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] private Sprite[] kirinBodyPartsSpriteList;
        [Header("化蛇のスプライト"), NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] private Sprite[] kadaBodyPartsSpriteList;
        [Header("獏のスプライト"), NamedArray(new string[] { "頭", "体", "脚" })]
        [SerializeField] private Sprite[] bakuBodyPartsSpriteList;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// 自分のパーツのタイプで出すパーツが変わる
        /// </summary>
        /// <param name="eachPartsType">敵の型</param>
        /// <param name="bodyPartsType">体のどのパーツか</param>
        /// <returns></returns>
        public GameObject PartsTypeObject(PartsType.EachPartsType eachPartsType,PartsType.BodyPartsType bodyPartsType)
        {
            GameObject obj =null;
            switch (eachPartsType)
            {
                case PartsType.EachPartsType.Kirin:
                    obj = kirinBodyParts[(int)bodyPartsType];
                    break;
                case PartsType.EachPartsType.Kada:
                    obj = kadaBodyParts[(int)bodyPartsType];
                    break;
                case PartsType.EachPartsType.Baku:
                    obj = bakuBodyParts[(int)bodyPartsType];
                    break;
                default:
                    obj = null;
                    break;
            }
            return obj;
        }
        /// <summary>
        /// プレイヤーのスプライト変更
        /// </summary>
        /// <param name="eachPartsType">敵の型</param>
        /// <param name="bodyPartsType">体のどのパーツかどうか</param>
        /// <returns></returns>
        public Sprite PartsSprite(PartsType.EachPartsType eachPartsType, PartsType.BodyPartsType bodyPartsType)
        {
            Sprite obj = null;
            switch (eachPartsType)
            {
                case PartsType.EachPartsType.Kirin:
                    obj = kirinBodyPartsSpriteList[(int)bodyPartsType];
                    break;
                case PartsType.EachPartsType.Kada:
                    obj = kadaBodyPartsSpriteList[(int)bodyPartsType];
                    break;
                case PartsType.EachPartsType.Baku:
                    obj = kirinBodyPartsSpriteList[(int)bodyPartsType];
                    break;
                default:
                    obj = null;
                    break;
            }
            return obj;
        }
    }
}