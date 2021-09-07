using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject[] EndingStroy = new GameObject[3];
    public GameObject[] Endings = new GameObject[4];
    public GameObject[] Ending34 = new GameObject[8];// 0~3 정령 , 4~7 정령왕
    public AudioSource[] audios = new AudioSource[2];
    int page;
    void Awake()
    {
        audios[0] = audios[0].GetComponent<AudioSource>();
        audios[1] = audios[1].GetComponent<AudioSource>();

        page = 0;
        EndingStroy[page].SetActive(true);
        EndingDecision(GlobalVariable.Gemstone);
    }
    void EndingDecision(int gem)
    {
        if(gem >= 4)
        {
            Endings[3].SetActive(true);
            int num = Random.Range(4, 8);
            Ending34[num].SetActive(true);
            PlayerPrefs.SetInt("Collect" + num.ToString(), 1);
        }
        else if (gem >= 3)
        {
            Endings[2].SetActive(true);
            int num = Random.Range(0, 4);
            Ending34[num].SetActive(true);
            PlayerPrefs.SetInt("Collect" + num.ToString(), 1);
        }
        else if(gem >= 1)
            Endings[1].SetActive(true);
        else
            Endings[0].SetActive(true);
        PlayerPrefs.Save();
    }
    public void EndingButton(string type)
    {
        switch (type)
        {
            case "Next":
                if(page < 2)
                {
                    audios[1].Play();
                    EndingStroy[page].SetActive(false);
                    if (page == 1)
                        Newstart();
                    page++;
                    EndingStroy[page].SetActive(true);
                }
                break;
            case "Prev":
                if(page > 0)
                {
                    audios[1].Play();
                    EndingStroy[page].SetActive(false);
                    page--;
                    EndingStroy[page].SetActive(true);
                }
                break;
            case "End":
                audios[1].Play();
                EndingStroy[page].SetActive(false);
                Application.Quit();
                break;
        }
    }
    void Newstart()
    {
        // #0. player
        PlayerPrefs.SetFloat("PlayerX", 22.77f);        // player.x
        PlayerPrefs.SetFloat("PlayerY", 17.65f);        // player.y

        // #1. Animal
        PlayerPrefs.SetInt("BringUpB", 0);
        PlayerPrefs.SetInt("BringUpM", 0);
        PlayerPrefs.SetInt("currentB", 0);
        PlayerPrefs.SetInt("currentM", 0);
        PlayerPrefs.SetInt("Completed0_0", 0);
        PlayerPrefs.SetInt("Completed0_1", 0);
        PlayerPrefs.SetInt("Completed1_0", 0);
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
        PlayerPrefs.SetInt("Completed9_1", 0);
        PlayerPrefs.SetInt("Completed10_0", 0);
        PlayerPrefs.SetInt("Completed10_1", 0);
        PlayerPrefs.SetInt("completedanimal", 0);

        // 2. Food
        PlayerPrefs.SetInt("ML_count", 10);
        PlayerPrefs.SetInt("Hot_count", 10);
        PlayerPrefs.SetInt("MTl_count", 10);
        PlayerPrefs.SetInt("MTs_count", 10);
        PlayerPrefs.SetInt("FR_count", 10);
        PlayerPrefs.SetInt("FS_count", 10);
        PlayerPrefs.SetInt("SR_count", 10);
        PlayerPrefs.SetInt("InS_count", 10);
        PlayerPrefs.SetInt("NT_count", 10);
        PlayerPrefs.SetInt("LZ_count", 10);
        PlayerPrefs.SetInt("ER_count", 10);
        PlayerPrefs.SetInt("revivalPortion", 0);
        PlayerPrefs.SetInt("bloodPortion", 0);
        PlayerPrefs.SetInt("speedPortion", 0);
        PlayerPrefs.SetInt("FemalePortion", 0);
        PlayerPrefs.SetInt("MalePortion", 0);

        // 3. GameManager
        PlayerPrefs.SetInt("sick", 0);
        PlayerPrefs.SetInt("poopcount", 0);
        PlayerPrefs.SetInt("flowercount", 0);
        PlayerPrefs.SetInt("year", 1);
        PlayerPrefs.SetInt("date", 1);
        PlayerPrefs.SetInt("month", 0);
        PlayerPrefs.SetFloat("time", 0);
        PlayerPrefs.SetFloat("playerbloodminus", 0);
        PlayerPrefs.SetInt("playerEp", 0);
        PlayerPrefs.SetInt("playerlevel", 0);
        PlayerPrefs.SetInt("peacecoin", 1000);

        // #4. QuestManger
        PlayerPrefs.SetInt("questID", 10);
        PlayerPrefs.SetInt("questActionIndex", 0);
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
        PlayerPrefs.SetInt("gemstone", 0);
        PlayerPrefs.Save();
    }
}
