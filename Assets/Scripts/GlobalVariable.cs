using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariable : MonoBehaviour
{
    public static float sfxVol;
    public static float bgmVol;

    public static int Gemstone;
    public static bool[] Collect = new bool[8];

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            bool isExistSave = PlayerPrefs.HasKey("PlayerX");
            if (isExistSave)
            {
                sfxVol = PlayerPrefs.GetFloat("sfxVol");
                bgmVol = PlayerPrefs.GetFloat("bgmVol");
                Gemstone = PlayerPrefs.GetInt("gemstone");
            }
            else
            {
                sfxVol = 0.5f;
                bgmVol = 0.5f;
                Gemstone = 0;

                PlayerPrefs.SetInt("gemstone", Gemstone);
                PlayerPrefs.SetFloat("sfxVol", sfxVol);
                PlayerPrefs.SetFloat("bgmVol", bgmVol);

                PlayerPrefs.Save();
            }
        }
    }
}
