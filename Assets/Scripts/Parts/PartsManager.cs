using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodyParts
{
    public class PartsManager : SingletonMonoBehaviour<PartsManager>
    {
        [Header("�i��"), NamedArray(new string[] { "��", "��", "�r" })]
        [SerializeField] private GameObject[] kirinBodyParts;
        [Header("����"), NamedArray(new string[] { "��", "��", "�r" })]
        [SerializeField] private GameObject[] kadaBodyParts;
        [Header("��"), NamedArray(new string[] { "��", "��", "�r" })]
        [SerializeField] private GameObject[] bakuBodyParts;
        [Header("�i�ق̃X�v���C�g"), NamedArray(new string[] { "��", "��", "�r" })]
        [SerializeField] private Sprite[] kirinBodyPartsSpriteList;
        [Header("���ւ̃X�v���C�g"), NamedArray(new string[] { "��", "��", "�r" })]
        [SerializeField] private Sprite[] kadaBodyPartsSpriteList;
        [Header("�т̃X�v���C�g"), NamedArray(new string[] { "��", "��", "�r" })]
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
        /// �����̃p�[�c�̃^�C�v�ŏo���p�[�c���ς��
        /// </summary>
        /// <param name="eachPartsType">�G�̌^</param>
        /// <param name="bodyPartsType">�̂̂ǂ̃p�[�c��</param>
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
        /// �v���C���[�̃X�v���C�g�ύX
        /// </summary>
        /// <param name="eachPartsType">�G�̌^</param>
        /// <param name="bodyPartsType">�̂̂ǂ̃p�[�c���ǂ���</param>
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