using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            headsIndex = (headsIndex + 1) % heads.Length;
            SetModels(heads, headsIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bodysIndex = (bodysIndex + 1) % bodys.Length;
            SetModels(bodys, bodysIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            legsIndex = (legsIndex + 1) % legs.Length;
            SetModels(legs, legsIndex);
        }
    }

    void SetModels(GameObject[] objs, int index)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(i == index);
        }
    }
}
