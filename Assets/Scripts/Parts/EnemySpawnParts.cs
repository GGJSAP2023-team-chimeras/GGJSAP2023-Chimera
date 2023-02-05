using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BodyParts 
{
    public class EnemySpawnParts : MonoBehaviour
    {
        [SerializeField] private PartsType.EachPartsType enemyType = PartsType.EachPartsType.Kirin;
        public PartsType.EachPartsType EnemyType { get { return enemyType; } set { enemyType = value; } }
        [SerializeField] private PartsType.BodyPartsType enemySpawnPartsType = PartsType.BodyPartsType.Body;
        public PartsType.BodyPartsType EnemySpawnPartsType { get { return enemySpawnPartsType; } set { enemySpawnPartsType = value; } }
        //破壊するかどうか
        private bool isDestroy = false;
        public bool IsDestroy { get { return isDestroy; } set { isDestroy = value; } }
        private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            //スプライト設定
            spriteRenderer.sprite = PartsManager.Instance.PartsSprite(enemyType, enemySpawnPartsType);
        }

        // Update is called once per frame
        void Update()
        {
            if (isDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}