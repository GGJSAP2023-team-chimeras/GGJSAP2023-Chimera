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
    /// ���f���̑g�ݍ��킹����������Ԃ�
    /// ������ : false
    /// ���� : true
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
    /// ���f���̑g�ݍ��킹�𔭌��ς݂ɂ���
    /// </summary>
    /// <param name="headType"></param>
    /// <param name="bodyType"></param>
    /// <param name="legType"></param>
    public static void SetCheckModel(EachPartsType headType, EachPartsType bodyType, EachPartsType legType)
    {
        int index = GetModelIndex(headType, bodyType, legType);
        // �����ς݂ɂ���
        // ������ : 0
        // ���� : 1
        PlayerPrefs.SetInt($"ModelDiscovered{index}", 1);
    }

    /// <summary>
    /// ���f���̑g�ݍ��킹�̒ʂ��ԍ����擾����
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
    /// ��������S�ď���
    /// </summary>
    public static void DeleteCheckModelData()
    {
        for (int i = 0; i < 64; i++)
        {
            PlayerPrefs.SetInt($"ModelDiscovered{i}", 0);
        }
    }
}
