using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodyParts
{
    public class PartsManager : SingletonMonoBehaviour<PartsManager>
    {
        [Header("êi—Ù"), NamedArray(new string[] { "“ª", "‘Ì", "‹r" })]
        [SerializeField] private GameObject[] kirinBodyParts;
        [Header("‰»ŽÖ"), NamedArray(new string[] { "“ª", "‘Ì", "‹r" })]
        [SerializeField] private GameObject[] kadaBodyParts;
        [Header("àÑ"), NamedArray(new string[] { "“ª", "‘Ì", "‹r" })]
        [SerializeField] private GameObject[] bakuBodyParts;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PartsTypeObject(PartsType.EachPartsType eachPartsType,PartsType.BodyPartsType bodyPartsType)
        {
            switch (eachPartsType)
            {
                case PartsType.EachPartsType.Kirin:
                    //kirinBodyParts[(int)bodyPartsType]
                    break;
                case PartsType.EachPartsType.Kada:
                    //kadaBodyParts[(int)bodyPartsType]
                    break;
                case PartsType.EachPartsType.Baku:
                    //bakuBodyParts[(int)bodyPartsType]   
                    break;
                default:
                    break;
            }
        }
    }
}