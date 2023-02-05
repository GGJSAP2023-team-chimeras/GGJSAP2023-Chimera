using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LibraryManager : MonoBehaviour
{
    [SerializeField]
    SpriteModelChanger spriteModelChangerOrigin;
    [SerializeField]
    Transform pivot;
    [SerializeField]
    GameObject hatena;
    [SerializeField]
    TMP_Text textMesh;
    int scoreCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    /*
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        var smc = Instantiate(spriteModelChangerOrigin, pivot);
                        smc.transform.localPosition = new Vector3(j * 360 + k * 80, i * -150);
                        smc.SetModelPartsAllLibrary(i, k, j);
                    }
                    else
                    {
                        var obj = Instantiate(hatena, pivot);
                        obj.transform.localPosition = new Vector3(j * 360 + k * 80, i * -150 + 75);
                        obj.SetActive(true);
                    }
                    */
                    var smc = Instantiate(spriteModelChangerOrigin, pivot);
                    smc.transform.localPosition = new Vector3(j * 360 + k * 80, i * -150);
                    smc.SetModelPartsAllLibrary(i, k, j);
                    if (SpriteModelChecker.GetCheckModel(i, k, j))
                    {
                        smc.SetColor(Color.white);
                        scoreCount++;
                    }
                    else
                    {
                        if (Random.Range(0f, 1f) < 0.5f)
                        {
                            smc.SetColor(Color.black);
                        }
                        else
                        {
                            scoreCount++;
                        }
                    }
                }
            }
        }

        textMesh.text = $"{(scoreCount * 100) / 64}%";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
