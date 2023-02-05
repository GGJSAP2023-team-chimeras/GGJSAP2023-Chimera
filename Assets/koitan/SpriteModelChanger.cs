using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodyParts;
using static BodyParts.PartsType;

public class SpriteModelChanger : MonoBehaviour
{
    [SerializeField]
    GameObject[] heads;
    [SerializeField]
    GameObject[] bodys;
    [SerializeField]
    GameObject[] legs;
    private int headsIndex;
    private int bodysIndex;
    private int legsIndex;
    EachPartsType headType;
    EachPartsType bodyType;
    EachPartsType legType;


    // Start is called before the first frame update
    void Start()
    {
        /*
        SetModels(heads, headsIndex);
        SetModels(bodys, bodysIndex);
        SetModels(legs, legsIndex);
        */
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        headsIndex = (headsIndex + 1) % heads.Length;
    //        SetModels(heads, headsIndex);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        bodysIndex = (bodysIndex + 1) % bodys.Length;
    //        SetModels(bodys, bodysIndex);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        legsIndex = (legsIndex + 1) % legs.Length;
    //        SetModels(legs, legsIndex);
    //    }
    //}

    void SetModels(GameObject[] objs, int index)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(i == index);
        }
        //Debug.Log($"GetModelIndex = {GetModelIndex()}");
    }

    /// <summary>
    /// パーツをすべて入れ替える
    /// </summary>
    /// <param name="headEachType"></param>
    /// <param name="bodyEachType"></param>
    /// <param name="legEachType"></param>
    public void SetModelPartsAll(EachPartsType headEachType, EachPartsType bodyEachType, EachPartsType legEachType)
    {
        SetModelParts(BodyPartsType.Head, headEachType);
        SetModelParts(BodyPartsType.Body, bodyEachType);
        SetModelParts(BodyPartsType.Foot, legEachType);
    }

    /// <summary>
    /// 図鑑用
    /// </summary>
    /// <param name="h"></param>
    /// <param name="b"></param>
    /// <param name="l"></param>
    public void SetModelPartsAllLibrary(int h, int b, int l)
    {
        SetModels(heads, h);
        SetModels(bodys, b);
        SetModels(legs, l);
    }

    public void SetColor(Color color)
    {
        foreach (var sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = color;
        }
    }

    /// <summary>
    /// 一部のパーツを入れ替える
    /// </summary>
    /// <param name="bodyPartsType"></param>
    /// <param name="eachPartsType"></param>
    public void SetModelParts(BodyPartsType bodyPartsType, EachPartsType eachPartsType)
    {
        switch (bodyPartsType)
        {
            case BodyPartsType.Head:
                headType = eachPartsType;
                SetModels(heads, (int)headType);
                break;
            case BodyPartsType.Body:
                bodyType = eachPartsType;
                SetModels(bodys, (int)bodyType);
                break;
            case BodyPartsType.Foot:
                legType = eachPartsType;
                SetModels(bodys, (int)legType);
                break;
        }
    }

    /// <summary>
    /// パーツの種類を取得する
    /// </summary>
    /// <param name="eachPartsType"></param>
    /// <returns></returns>
    EachPartsType GetEachPartsType(BodyPartsType eachPartsType)
    {
        switch (eachPartsType)
        {
            case BodyPartsType.Head:
                return headType;
            case BodyPartsType.Body:
                return bodyType;
            case BodyPartsType.Foot:
                return legType;
        }
        return EachPartsType.None;
    }

    int GetModelIndex()
    {
        return headsIndex + 4 * bodysIndex + 16 * legsIndex;
    }
}
