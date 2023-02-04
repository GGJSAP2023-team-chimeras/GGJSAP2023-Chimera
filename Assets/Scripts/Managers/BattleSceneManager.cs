using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class BattleSceneManager : SingletonMonoBehaviour<BattleSceneManager>
    {
        public GameObject ResultUIObject;

        // FIXME: EnemyTypeå^Ç…Ç∑ÇÈ
        public int BossEnemyType;

        public void InitBattleScene()
        {
            Debug.Log(BossEnemyType);
        }

        public void FinishGame()
        {
            ResultUI.Instance.ShowResult();
        }
    }
}
