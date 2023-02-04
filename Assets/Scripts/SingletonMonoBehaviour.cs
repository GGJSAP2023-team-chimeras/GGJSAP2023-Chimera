using UnityEngine;
using System;

/// <summary>
/// Singleton��: SingletonMonoBehaviour<GameManager>(GameManager�̏ꍇ)�̂悤�Ɏg��
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    //���̃N���X���p�������N���X�̃C���X�^���X��ݒ肵�Ď����ȊO�̎����̃N���X��T���A�������ꍇ�͎����������B
    private static T instance;
    public static T Instance
    {
        get
        {
            //�C���X�^���X��������Ă��Ȃ������ꍇ
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
    }
    /// <summary>
    /// �C���X�^���X�̗L���R��
    /// </summary>
    /// <returns></returns>
    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}