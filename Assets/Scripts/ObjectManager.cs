using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameManager manager;
    public saveManager save;
    float x = 2.5f; // x  위치
    float y = 17.5f; // y 위치

    // 프리팹
    public GameObject prefabA0; // 팬더
    public GameObject prefabA1; // 곰
    public GameObject prefabA2; // 사자
    public GameObject prefabA3; // 미어캣
    public GameObject prefabA4; // 타조
    public GameObject prefabA5; //다람쥐
    public GameObject prefabA6; // 두루미
    public GameObject prefabA7; // 독수리
    public GameObject prefabA8; // 펭귄
    public GameObject prefabA9; // 물범
    public GameObject prefabA10; // 여우

    public GameObject prefabNest; // 둥지
    public GameObject prefabPoop; // 거름
    public GameObject prefabFlower; // 꽃잎

    public GameObject prefabB0; // 팬더
    public GameObject prefabB1; // 곰
    public GameObject prefabB2; // 사자
    public GameObject prefabB3; // 미어캣
    public GameObject prefabB4; // 타조
    public GameObject prefabB5; //다람쥐
    public GameObject prefabB6; // 두루미
    public GameObject prefabB7; // 독수리
    public GameObject prefabB8; // 펭귄
    public GameObject prefabB9; // 물범
    public GameObject prefabB10; // 여우

    // 관리자
    GameObject[] A0; //팬더
    GameObject[] A1; // 곰
    GameObject[] A2; // 사자
    GameObject[] A3; // 미어캣
    GameObject[] A4; // 타조
    GameObject[] A5; // 다람쥐
    GameObject[] A6; // 두루미
    GameObject[] A7; // 독수리
    GameObject[] A8; // 펭귄
    GameObject[] A9; // 물범
    GameObject[] A10; // 여우

    GameObject[] Nest; // 둥지
    GameObject[] Poop; // 거름
    GameObject[] Flower; // 꽃잎

    GameObject[] B0; //팬더
    GameObject[] B1; // 곰
    GameObject[] B2; // 사자
    GameObject[] B3; // 미어캣
    GameObject[] B4; // 타조
    GameObject[] B5; // 다람쥐
    GameObject[] B6; // 두루미
    GameObject[] B7; // 독수리
    GameObject[] B8; // 펭귄
    GameObject[] B9; // 물범
    GameObject[] B10; // 여우

    GameObject[] targetPool;
    void Awake()
    {
        A0 = new GameObject[12];
        A1 = new GameObject[12];
        A2 = new GameObject[12];
        A3 = new GameObject[12];
        A4 = new GameObject[12];
        A5 = new GameObject[12];
        A6 = new GameObject[12];
        A7 = new GameObject[12];
        A8 = new GameObject[12];
        A9 = new GameObject[12];
        A10 = new GameObject[12];

        Nest = new GameObject[12]; // 둥지
        Poop = new GameObject[20]; // 거름
        Flower = new GameObject[6]; // 꽃잎

        B0 = new GameObject[12];
        B1 = new GameObject[12]; 
        B2 = new GameObject[12];
        B3 = new GameObject[12]; 
        B4 = new GameObject[12]; 
        B5 = new GameObject[12];
        B6 = new GameObject[12];
        B7 = new GameObject[12];
        B8 = new GameObject[12];
        B9 = new GameObject[12];
        B10 = new GameObject[12];

        NestGenerate();
        Generate();
    }
    void OnEnable()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
        else
        {
            x = 2.5f;
            for (int i = 0; i < 12; i++)
            {
                if (manager.idlevel_informbool[i])
                {
                    IDLevelEP aaa = null;
                    if (save.IsBaby[i]) //아기
                    {
                        aaa = Nest[i].GetComponent<IDLevelEP>();
                    }
                    else // 어른
                    {
                        if (save.Animal_EP[i] <= 10) // Baby
                        {
                            GameObject animalSet = MakeObj("B" + save.ArrayID[i].ToString());
                            aaa = animalSet.GetComponent<IDLevelEP>();
                            animalSet.transform.position = new Vector2(x, y-2f);
                        }
                        else // Adult
                        {
                            GameObject animalSet = MakeObj("A" + save.ArrayID[i].ToString());
                            aaa = animalSet.GetComponent<IDLevelEP>();
                            animalSet.transform.position = new Vector2(x, y-2f);
                        }
                    }
                    aaa.Num = i;
                    aaa.Animal_Id = save.Animal_Id[i];
                    aaa.Animal_EP = save.Animal_EP[i];
                    aaa.Animal_Damage = save.Animal_Damage[i];
                    aaa.animalSex = save.animal_sex[i];
                    aaa.Is = save.Is[i];
                    aaa.sickpanel.SetActive(save.sickpanel[i]);
                    aaa.hungry = save.hungry[i];
                    manager.idlevel_inform[i] = aaa;
                }
                x += 1f;
            }
        }
    }
    void NestGenerate()
    {
        for (int n = 0; n < Nest.Length; n++)
        {
            Nest[n] = Instantiate(prefabNest);
            IDLevelEP aaa = Nest[n].GetComponent<IDLevelEP>();
            aaa.Num = n;
            Nest[n].transform.position = new Vector2(x,y);
            x += 1f;
        }
    }
    void Generate()
    {
        // 1. Adult 동물생성
        for(int n = 0; n < A0.Length; n++)
        {
            A0[n] = Instantiate(prefabA0);
            A0[n].SetActive(false);
        }
        for (int n = 0; n < A1.Length; n++)
        {
            A1[n] = Instantiate(prefabA1);
            A1[n].SetActive(false);
        }  
        for (int n = 0; n < A2.Length; n++)
        {
            A2[n] = Instantiate(prefabA2);
            A2[n].SetActive(false);
        }
        for (int n = 0; n < A3.Length; n++)
        {
            A3[n] = Instantiate(prefabA3);
            A3[n].SetActive(false);
        }
        for (int n = 0; n < A4.Length; n++)
        {
            A4[n] = Instantiate(prefabA4);
            A4[n].SetActive(false);
        }
        for (int n = 0; n < A5.Length; n++)
        {
            A5[n] = Instantiate(prefabA5);
            A5[n].SetActive(false);
        }
        for (int n = 0; n < A6.Length; n++)
        {
            A6[n] = Instantiate(prefabA6);
            A6[n].SetActive(false);
        }
        for (int n = 0; n < A7.Length; n++)
        {
            A7[n] = Instantiate(prefabA7);
            A7[n].SetActive(false);
        }
        for (int n = 0; n < A8.Length; n++)
        {
            A8[n] = Instantiate(prefabA8);
            A8[n].SetActive(false);
        }
        for (int n = 0; n < A9.Length; n++)
        {
            A9[n] = Instantiate(prefabA9);
            A9[n].SetActive(false);
        }
        for (int n = 0; n < A10.Length; n++)
        {
            A10[n] = Instantiate(prefabA10);
            A10[n].SetActive(false);
        }
        // 2. 기타 생성
        for (int n = 0; n < Poop.Length; n++)
        {
            Poop[n] = Instantiate(prefabPoop);
            Poop[n].SetActive(false);
        }
        for (int n = 0; n < Flower.Length; n++)
        {
            Flower[n] = Instantiate(prefabFlower);
            Flower[n].SetActive(false);
        }
        // 3. Baby 동물생성
        for (int n = 0; n < B0.Length; n++)
        {
            B0[n] = Instantiate(prefabB0);
            B0[n].SetActive(false);
        }
        for (int n = 0; n < B1.Length; n++)
        {
            B1[n] = Instantiate(prefabB1);
            B1[n].SetActive(false);
        }
        for (int n = 0; n < B2.Length; n++)
        {
            B2[n] = Instantiate(prefabB2);
            B2[n].SetActive(false);
        }
        for (int n = 0; n < B3.Length; n++)
        {
            B3[n] = Instantiate(prefabB3);
            B3[n].SetActive(false);
        }
        for (int n = 0; n < B4.Length; n++)
        {
            B4[n] = Instantiate(prefabB4);
            B4[n].SetActive(false);
        }
        for (int n = 0; n < B5.Length; n++)
        {
            B5[n] = Instantiate(prefabB5);
            B5[n].SetActive(false);
        }
        for (int n = 0; n < B6.Length; n++)
        {
            B6[n] = Instantiate(prefabB6);
            B6[n].SetActive(false);
        }
        for (int n = 0; n < B7.Length; n++)
        {
            B7[n] = Instantiate(prefabB7);
            B7[n].SetActive(false);
        }
        for (int n = 0; n < B8.Length; n++)
        {
            B8[n] = Instantiate(prefabB8);
            B8[n].SetActive(false);
        }
        for (int n = 0; n < B9.Length; n++)
        {
            B9[n] = Instantiate(prefabB9);
            B9[n].SetActive(false);
        }
        for (int n = 0; n < B10.Length; n++)
        {
            B10[n] = Instantiate(prefabB10);
            B10[n].SetActive(false);
        }
    }
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "A0":
                targetPool = A0;
                break;
            case "A1":
                targetPool = A1;
                break;
            case "A2":
                targetPool = A2;
                break;
            case "A3":
                targetPool = A3;
                break;
            case "A4":
                targetPool = A4;
                break;
            case "A5":
                targetPool = A5;
                break;
            case "A6":
                targetPool = A6;
                break;
            case "A7":
                targetPool = A7;
                break;
            case "A8":
                targetPool = A8;
                break;
            case "A9":
                targetPool = A9;
                break;
            case "A10":
                targetPool = A10;
                break;
            case "B0":
                targetPool = B0;
                break;
            case "B1":
                targetPool = B1;
                break;
            case "B2":
                targetPool = B2;
                break;
            case "B3":
                targetPool = B3;
                break;
            case "B4":
                targetPool = B4;
                break;
            case "B5":
                targetPool = B5;
                break;
            case "B6":
                targetPool = B6;
                break;
            case "B7":
                targetPool = B7;
                break;
            case "B8":
                targetPool = B8;
                break;
            case "B9":
                targetPool = B9;
                break;
            case "B10":
                targetPool = B10;
                break;
            case "Nest":
                targetPool = Nest;
                break;
            case "Poop":
                targetPool = Poop;
                break;
            case "Flower":
                targetPool = Flower;
                break;
        }
        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}