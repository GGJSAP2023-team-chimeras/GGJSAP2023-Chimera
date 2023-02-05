using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BodyParts.PartsType;

public class SpriteModelChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// モデルの組み合わせが発見かを返す
    /// 未発見 : false
    /// 発見 : true
    /// </summary>
    /// <param name="headType"></param>
    /// <param name="bodyType"></param>
    /// <param name="legType"></param>
    /// <returns></returns>
    public static bool GetCheckModel(EachPartsType headType, EachPartsType bodyType, EachPartsType legType)
    {
        int index = GetModelIndex(headType, bodyType, legType);
        if (PlayerPrefs.GetInt($"ModelDiscovered{index}", 0) > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// モデルの組み合わせを発見済みにする
    /// </summary>
    /// <param name="headType"></param>
    /// <param name="bodyType"></param>
    /// <param name="legType"></param>
    public static void SetCheckModel(EachPartsType headType, EachPartsType bodyType, EachPartsType legType)
    {
        int index = GetModelIndex(headType, bodyType, legType);
        // 発見済みにする
        // 未発見 : 0
        // 発見 : 1
        PlayerPrefs.SetInt($"ModelDiscovered{index}", 1);
    }

    /// <summary>
    /// モデルの組み合わせの通し番号を取得する
    /// </summary>
    /// <param name="headType"></param>
    /// <param name="bodyType"></param>
    /// <param name="legType"></param>
    /// <returns></returns>
    public static int GetModelIndex(EachPartsType headType, EachPartsType bodyType, EachPartsType legType)
    {
        return (int)headType + (int)bodyType * 4 + (int)legType * 16;
    }

    /// <summary>
    /// 発見情報を全て消去
    /// </summary>
    public static void DeleteCheckModelData()
    {
        for (int i = 0; i < 64; i++)
        {
            PlayerPrefs.SetInt($"ModelDiscovered{i}", 0);
        }
    }
}
