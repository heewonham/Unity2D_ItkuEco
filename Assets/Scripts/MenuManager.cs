using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject start_Pop; // 시작팝업
    public GameObject rechk; // 재확인팝업
    public GameObject Collection; // 컬렉션 팝업
    public GameObject tutorial; // 튜토리얼 팝업
    public GameObject[] tutorialpage = new GameObject[5];
    public AudioSource[] audios = new AudioSource[2];
    public GameObject[] EndingCollect = new GameObject[8];
    public GameObject[] FinishColl = new GameObject[8];

    int CollectionPage;
    bool IsExisSave;
    int Tpage;
    void Awake()
    {
        // Check Save Data Exist
        IsExisSave = PlayerPrefs.HasKey("PlayerX");
        audios[0] = audios[0].GetComponent<AudioSource>();
        audios[1] = audios[1].GetComponent<AudioSource>();
        audios[0].Play();
        CollectChk();
    }
    void OnEnable()
    {
        CollectChk();
    }
    void CollectChk()
    {
        for (int i = 0; i < 8; i++)
        {
            if (PlayerPrefs.GetInt("Collect" + i.ToString()) == 0)
            {
                GlobalVariable.Collect[i] = false;
                PlayerPrefs.SetInt("Collect" + i.ToString(), 0);
            }
            else if (PlayerPrefs.GetInt("Collect" + i.ToString()) == 1)
            {
                GlobalVariable.Collect[i] = true;
            }
            PlayerPrefs.Save();
            FinishColl[i].SetActive(GlobalVariable.Collect[i]);
        }
    }
    public void OpeningButton(string type)
    {
        switch (type)
        {
            case "ready": // 시작버튼
                audios[1].Play();
                if (IsExisSave)
                    start_Pop.SetActive(true);
                else
                {
                    Newstart();
                    SceneManager.LoadScene(1); //Opening
                }   
                break;
            case "tutorial": // 튜터리얼 버튼
                audios[1].Play();
                Tpage = 0;
                tutorialpage[Tpage].SetActive(true);
                tutorial.SetActive(true);
                break;
            case "Collection": // 컬렉션 버튼
                audios[1].Play();
                CollectionPage = 0;
                EndingCollect[CollectionPage].SetActive(true);
                Collection.SetActive(true);
                break;
            case "Exit": // 나가기 버튼
                audios[1].Play();
                Application.Quit();
                break;
                // Pop Up 버튼
            case "relay": // 이어하기
                audios[1].Play();
                start_Pop.SetActive(false);
                SceneManager.LoadScene(2);
                break;
            case "restart": // 재시작하기
                audios[1].Play();
                start_Pop.SetActive(false);
                rechk.SetActive(true);
                break;
            case "yes": // Yes버튼
                audios[1].Play();
                rechk.SetActive(false);
                Newstart();
                SceneManager.LoadScene(1);
                break;
            case "no": // No버튼
                audios[1].Play();
                rechk.SetActive(false);
                break;
            case "popexit": // 팝업끄기
                audios[1].Play();
                if (start_Pop.activeSelf)
                    start_Pop.SetActive(false);
                else if (rechk.activeSelf)
                    rechk.SetActive(false);
                break;
        }
    }
    public void tutorialpagebutton(string type)
    {
        switch (type)
        {
            case "Prev":
                if(Tpage  > 0)
                {
                    tutorialpage[Tpage].SetActive(false);
                    Tpage--;
                    tutorialpage[Tpage].SetActive(true);
                }
                break;
            case "Next":
                if(Tpage < 4)
                {
                    tutorialpage[Tpage].SetActive(false);
                    Tpage++;
                    tutorialpage[Tpage].SetActive(true);
                }
                break;
            case "Exit":
                tutorialpage[Tpage].SetActive(false);
                tutorial.SetActive(false);
                break;
        }
    }
    public void CollectionButton(string type)
    {
        switch (type)
        {
            case "Prev":
                audios[1].Play();
                if (CollectionPage > 0)
                {
                    EndingCollect[CollectionPage].SetActive(false);
                    CollectionPage--;
                    EndingCollect[CollectionPage].SetActive(true);
                }
                break;
            case "Next":
                audios[1].Play();
                if (CollectionPage < 7 )
                {
                    EndingCollect[CollectionPage].SetActive(false);
                    CollectionPage++;
                    EndingCollect[CollectionPage].SetActive(true);
                }
                break;
            case "Exit":
                audios[1].Play();
                EndingCollect[CollectionPage].SetActive(false);
                Collection.SetActive(false);
                break;
        }
    }
    void Newstart()
    {
        // #0. player
        PlayerPrefs.SetFloat("PlayerX", 22.77f);        // player.x
        PlayerPrefs.SetFloat("PlayerY", 17.65f);        // player.y
        PlayerPrefs.SetFloat("sfxVol", 0.5f);
        PlayerPrefs.SetFloat("bgmVol", 0.5f);

        // #1. Animal
        PlayerPrefs.SetInt("BringUpB", 0);
        PlayerPrefs.SetInt("BringUpM", 0);
        PlayerPrefs.SetInt("currentB", 0);
        PlayerPrefs.SetInt("currentM", 0);
        PlayerPrefs.SetInt("Completed0_0",0);
        PlayerPrefs.SetInt("Completed0_1",0);
        PlayerPrefs.SetInt("Completed1_0",0);
        PlayerPrefs.SetInt("Completed1_1", 0);
        PlayerPrefs.SetInt("Completed2_0", 0);
        PlayerPrefs.SetInt("Completed2_1", 0);
        PlayerPrefs.SetInt("Completed3_0", 0);
        PlayerPrefs.SetInt("Completed3_1", 0);
        PlayerPrefs.SetInt("Completed4_0", 0);
        PlayerPrefs.SetInt("Completed4_1", 0);
        PlayerPrefs.SetInt("Completed5_0", 0);
        PlayerPrefs.SetInt("Completed5_1", 0);
        PlayerPrefs.SetInt("Completed6_0", 0);
        PlayerPrefs.SetInt("Completed6_1", 0);
        PlayerPrefs.SetInt("Completed7_0", 0);
        PlayerPrefs.SetInt("Completed7_1", 0);
        PlayerPrefs.SetInt("Completed8_0", 0);
        PlayerPrefs.SetInt("Completed8_1", 0);
        PlayerPrefs.SetInt("Completed9_0", 0);
        PlayerPrefs.SetInt("Completed9_1",0);
        PlayerPrefs.SetInt("Completed10_0", 0);
        PlayerPrefs.SetInt("Completed10_1", 0);
        PlayerPrefs.SetInt("completedanimal", 0);

        // 2. Food
        PlayerPrefs.SetInt("ML_count",10);
        PlayerPrefs.SetInt("Hot_count", 10);
        PlayerPrefs.SetInt("MTl_count", 10);
        PlayerPrefs.SetInt("MTs_count", 10);
        PlayerPrefs.SetInt("FR_count", 10);
        PlayerPrefs.SetInt("FS_count", 10);
        PlayerPrefs.SetInt("SR_count",10);
        PlayerPrefs.SetInt("InS_count", 10);
        PlayerPrefs.SetInt("NT_count", 10);
        PlayerPrefs.SetInt("LZ_count", 10);
        PlayerPrefs.SetInt("ER_count", 10);
        PlayerPrefs.SetInt("revivalPortion",0);
        PlayerPrefs.SetInt("bloodPortion", 0);
        PlayerPrefs.SetInt("speedPortion", 0);
        PlayerPrefs.SetInt("FemalePortion", 0);
        PlayerPrefs.SetInt("MalePortion", 0);

        // 3. GameManager
        PlayerPrefs.SetInt("sick",0);
        PlayerPrefs.SetInt("poopcount", 0);
        PlayerPrefs.SetInt("flowercount", 0);
        PlayerPrefs.SetInt("year", 1);
        PlayerPrefs.SetInt("date",1);
        PlayerPrefs.SetInt("month", 0);
        PlayerPrefs.SetFloat("time", 0);
        PlayerPrefs.SetFloat("playerbloodminus",0);
        PlayerPrefs.SetInt("playerEp", 0);
        PlayerPrefs.SetInt("playerlevel", 0);
        PlayerPrefs.SetInt("peacecoin", 1000);

        // #4. QuestManger
        PlayerPrefs.SetInt("questID", 10);
        PlayerPrefs.SetInt("questActionIndex",0);
        PlayerPrefs.SetInt("QuestanimalA", 0);
        PlayerPrefs.SetInt("QuestanimalF", 0);
        PlayerPrefs.SetInt("QuestanimalM", 0);

        // # 키우는 동물 저장하기
        for (int i = 0; i < 12; i++)
        {
            PlayerPrefs.SetInt("idlevel_informbool" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_ArrayID" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_Animal_Id" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_Animal_EP" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_Animal_Damage" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_animalSex" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_Is" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_sickpanel" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_hungry" + i.ToString(), 0);
            PlayerPrefs.SetInt("idlevel_inform_IsBaby" + i.ToString(), 0);
        }
        PlayerPrefs.Save();
    }
}