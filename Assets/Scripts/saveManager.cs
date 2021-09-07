using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveManager : MonoBehaviour
{
    public Animal animal;
    public FoodManager food;
    public GameManager manager;
    public QuestManager quest;
    public Player player;

    // #0. player
    public float playerx;
    public float playery;

    // #1. Animal
    public int sBringUpM; // 기르는 포유류
    public int sBringUpB;  // 기르는 조류
    public int sCurrentM; // 소유한 포유류;
    public int sCurrentB; // 소유한 조류;
    public int[,] sCompleted = new int[11, 2];
    public int sCompleted_Animal; // 총 완료동물 수

    // # FoodManager
    public int sML_count; // 우유
    public int sHot_count; // 온도올리기
    public int sMTs_count; // 고기소
    public int sFR_count; // 과일
    public int sMTl_count; //고기대
    public int sFS_count; // 물고기
    public int sSR_count; // 새우
    public int sInS_count; // 벌레
    public int sNT_count; // 나무열매 
    public int sLZ_count; // 도마뱀
    public int sER_count; // 지렁이
    public int sRevivalPortion; // 부활물약
    public int sBloodPortion;// 체력물약
    public int sSpeedPortion; // 속도물약
    public int sFemalePortion;// 암컷촉진물약
    public int sMalePortion; // 수컷촉진물약

    // #3. GameManager
    public bool[] sidlevel_informbool = new bool[12];
    public int sPoopcount;
    public int sFlowercount;
    public bool sPlayersick;
    public int sYear;
    public int sDate;
    public int sMonth;
    public float sTime;
    public float sPlayerbloodminus;
    public int sPlayerEp;
    public int sPlayerlevel; // 플레이어의 레벨
    public int sPeacecoin;

    // #4. QuestManger
    public int sQuestID;
    public int sQuestActionIndex; // 퀘스트 대화 순서 변수
    public int sQuestanimalA;
    public int sQuestanimalF;
    public int sQuestanimalM;

    // #5. 동물키우기
    public bool[] IsBaby = new bool[12];
    public int[] ArrayID = new int[12];
    public int[] Animal_Id = new int[12];
    public int[] Animal_EP = new int[12];
    public int[] Animal_Damage = new int[12];
    public int[] animal_sex = new int[12];
    public bool[] Is = new bool[12];
    public bool[] sickpanel = new bool[12];
    public bool[] hungry = new bool[12];

    void Awake()
    {
        Load();
    }
    public void Save()
    {
        // #0. player
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);        // player.x
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);        // player.y
        PlayerPrefs.SetFloat("sfxVol", manager.BGMslider.value);        
        PlayerPrefs.SetFloat("bgmVol", manager.Effectslider.value);

        // #1. Animal
        PlayerPrefs.SetInt("BringUpB", animal.BringUpB);
        PlayerPrefs.SetInt("BringUpM", animal.BringUpM);
        PlayerPrefs.SetInt("currentB", animal.currentB);
        PlayerPrefs.SetInt("currentM", animal.currentM);
        PlayerPrefs.SetInt("Completed0_0", animal.Completed[0, 0]);
        PlayerPrefs.SetInt("Completed0_1", animal.Completed[0, 1]);
        PlayerPrefs.SetInt("Completed1_0", animal.Completed[1, 0]);
        PlayerPrefs.SetInt("Completed1_1", animal.Completed[1, 1]);
        PlayerPrefs.SetInt("Completed2_0", animal.Completed[2, 0]);
        PlayerPrefs.SetInt("Completed2_1", animal.Completed[2, 1]);
        PlayerPrefs.SetInt("Completed3_0", animal.Completed[3, 0]);
        PlayerPrefs.SetInt("Completed3_1", animal.Completed[3, 1]);
        PlayerPrefs.SetInt("Completed4_0", animal.Completed[4, 0]);
        PlayerPrefs.SetInt("Completed4_1", animal.Completed[4, 1]);
        PlayerPrefs.SetInt("Completed5_0", animal.Completed[5, 0]);
        PlayerPrefs.SetInt("Completed5_1", animal.Completed[5, 1]);
        PlayerPrefs.SetInt("Completed6_0", animal.Completed[6, 0]);
        PlayerPrefs.SetInt("Completed6_1", animal.Completed[6, 1]);
        PlayerPrefs.SetInt("Completed7_0", animal.Completed[7, 0]);
        PlayerPrefs.SetInt("Completed7_1", animal.Completed[7, 1]);
        PlayerPrefs.SetInt("Completed8_0", animal.Completed[8, 0]);
        PlayerPrefs.SetInt("Completed8_1", animal.Completed[8, 1]);
        PlayerPrefs.SetInt("Completed9_0", animal.Completed[9, 0]);
        PlayerPrefs.SetInt("Completed9_1", animal.Completed[9, 1]);
        PlayerPrefs.SetInt("Completed10_0", animal.Completed[10, 0]);
        PlayerPrefs.SetInt("Completed10_1", animal.Completed[10, 1]);
        PlayerPrefs.SetInt("completedanimal", animal.completedanimal);

        // 2. Food
        PlayerPrefs.SetInt("ML_count", food.ML_count);
        PlayerPrefs.SetInt("Hot_count", food.Hot_count);
        PlayerPrefs.SetInt("MTl_count", food.MTl_count);
        PlayerPrefs.SetInt("MTs_count", food.MTs_count);
        PlayerPrefs.SetInt("FR_count", food.FR_count);
        PlayerPrefs.SetInt("FS_count", food.FS_count);
        PlayerPrefs.SetInt("SR_count", food.SR_count);
        PlayerPrefs.SetInt("InS_count", food.InS_count);
        PlayerPrefs.SetInt("NT_count", food.NT_count);
        PlayerPrefs.SetInt("LZ_count", food.LZ_count);
        PlayerPrefs.SetInt("ER_count", food.ER_count);
        PlayerPrefs.SetInt("revivalPortion", food.revivalPortion);
        PlayerPrefs.SetInt("bloodPortion", food.bloodPortion);
        PlayerPrefs.SetInt("speedPortion", food.speedPortion);
        PlayerPrefs.SetInt("FemalePortion", food.FemalePortion);
        PlayerPrefs.SetInt("MalePortion", food.MalePortion);

        // 3. GameManager
        PlayerPrefs.SetInt("sick", manager.sick ? 1 : 0);
        PlayerPrefs.SetInt("poopcount", manager.poopcount);
        PlayerPrefs.SetInt("flowercount", manager.flowercount);
        PlayerPrefs.SetInt("gemstone", GlobalVariable.Gemstone);
        PlayerPrefs.SetInt("year", manager.year);
        PlayerPrefs.SetInt("date", manager.date);
        PlayerPrefs.SetInt("month", manager.month);
        PlayerPrefs.SetFloat("time", manager.time);
        PlayerPrefs.SetFloat("playerbloodminus", manager.playerbloodminus);
        PlayerPrefs.SetInt("playerEp", manager.playerEp);
        PlayerPrefs.SetInt("playerlevel", manager.playerlevel);
        PlayerPrefs.SetInt("peacecoin", manager.peacecoin);

        // #4. QuestManger
        PlayerPrefs.SetInt("questID", quest.questID);
        PlayerPrefs.SetInt("questActionIndex", quest.questActionIndex);
        PlayerPrefs.SetInt("QuestanimalA", quest.QuestanimalA);
        PlayerPrefs.SetInt("QuestanimalF", quest.QuestanimalF);
        PlayerPrefs.SetInt("QuestanimalM", quest.QuestanimalM);

        // # 키우는 동물 저장하기
        for (int i = 0; i < 12; i++)
        {
            if (manager.idlevel_informbool[i])
            {
                PlayerPrefs.SetInt("idlevel_informbool" + i.ToString(), 1);
                PlayerPrefs.SetInt("idlevel_inform_ArrayID" + i.ToString(), manager.idlevel_inform[i].ArrayID);
                PlayerPrefs.SetInt("idlevel_inform_Animal_Id" + i.ToString(), manager.idlevel_inform[i].Animal_Id);
                PlayerPrefs.SetInt("idlevel_inform_Animal_EP" + i.ToString(), manager.idlevel_inform[i].Animal_EP);
                PlayerPrefs.SetInt("idlevel_inform_Animal_Damage" + i.ToString(), manager.idlevel_inform[i].Animal_Damage);
                PlayerPrefs.SetInt("idlevel_inform_animalSex" + i.ToString(), manager.idlevel_inform[i].animalSex);
                PlayerPrefs.SetInt("idlevel_inform_Is" + i.ToString(), manager.idlevel_inform[i].Is ? 1 : 0);
                PlayerPrefs.SetInt("idlevel_inform_sickpanel" + i.ToString(), manager.idlevel_inform[i].sickpanel.activeSelf ? 1 : 0);
                PlayerPrefs.SetInt("idlevel_inform_hungry" + i.ToString(), manager.idlevel_inform[i].hungry ? 1 : 0);
                PlayerPrefs.SetInt("idlevel_inform_IsBaby" + i.ToString(), manager.idlevel_inform[i].Isbaby ? 1 : 0);
            }
            else
            {
                PlayerPrefs.SetInt("idlevel_informbool" + i.ToString(), 0);
            }
            PlayerPrefs.Save();
        }

        PlayerPrefs.Save();
    }
    public void Load()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        // #0. player
        playerx = PlayerPrefs.GetFloat("PlayerX");        // player.x
        playery = PlayerPrefs.GetFloat("PlayerY");        // player.y

        // #1. Animal
        sBringUpM = PlayerPrefs.GetInt("BringUpM");
        sBringUpB = PlayerPrefs.GetInt("BringUpB");
        sCurrentM = PlayerPrefs.GetInt("currentM");
        sCurrentB = PlayerPrefs.GetInt("currentB");
        for (int i = 0; i < 11; i++)
        {
            sCompleted[i, 0] = PlayerPrefs.GetInt("Completed" + i.ToString()+"_0");
            sCompleted[i, 1] = PlayerPrefs.GetInt("Completed" + i.ToString() +"_1");
        }
        sCompleted_Animal = PlayerPrefs.GetInt("completedanimal");

        // 2. Food
        sML_count = PlayerPrefs.GetInt("ML_count");
        sHot_count = PlayerPrefs.GetInt("Hot_count");
        sFR_count = PlayerPrefs.GetInt("FR_count");
        sFS_count = PlayerPrefs.GetInt("FS_count");
        sMTl_count = PlayerPrefs.GetInt("MTl_count");
        sMTs_count = PlayerPrefs.GetInt("MTs_count");
        sSR_count = PlayerPrefs.GetInt("SR_count");
        sInS_count = PlayerPrefs.GetInt("InS_count");
        sNT_count = PlayerPrefs.GetInt("NT_count");
        sLZ_count = PlayerPrefs.GetInt("LZ_count");
        sER_count = PlayerPrefs.GetInt("ER_count");
        sRevivalPortion = PlayerPrefs.GetInt("revivalPortion");
        sBloodPortion = PlayerPrefs.GetInt("bloodPortion");
        sSpeedPortion = PlayerPrefs.GetInt("speedPortion");
        sFemalePortion = PlayerPrefs.GetInt("FemalePortion");
        sMalePortion = PlayerPrefs.GetInt("MalePortion");

        // 3. GameManager
        sPlayersick = PlayerPrefs.GetInt("sick") == 1 ? true : false; ;
        sPoopcount = PlayerPrefs.GetInt("poopcount");
        sFlowercount = PlayerPrefs.GetInt("flowercount");
        sYear = PlayerPrefs.GetInt("year");
        sDate = PlayerPrefs.GetInt("date");
        sMonth = PlayerPrefs.GetInt("month");
        sTime = PlayerPrefs.GetFloat("time");
        sPlayerbloodminus = PlayerPrefs.GetFloat("playerbloodminus");
        sPlayerEp = PlayerPrefs.GetInt("playerEp");
        sPlayerlevel = PlayerPrefs.GetInt("playerlevel");
        sPeacecoin = PlayerPrefs.GetInt("peacecoin");

        // #4. QuestManger
        sQuestID = PlayerPrefs.GetInt("questID");
        sQuestActionIndex = PlayerPrefs.GetInt("questActionIndex");
        sQuestanimalA = PlayerPrefs.GetInt("QuestanimalA");
        sQuestanimalF = PlayerPrefs.GetInt("QuestanimalF");
        sQuestanimalM = PlayerPrefs.GetInt("QuestanimalM");

        // # 키우는 동물 저장하기
        for (int i = 0; i < 12; i++)
        {
            sidlevel_informbool[i] = PlayerPrefs.GetInt("idlevel_informbool" + i.ToString()) == 1 ? true : false;
            if (sidlevel_informbool[i])
            {
                ArrayID[i] = PlayerPrefs.GetInt("idlevel_inform_ArrayID" + i.ToString());
                Animal_Id[i] = PlayerPrefs.GetInt("idlevel_inform_Animal_Id" + i.ToString());
                Animal_EP[i] = PlayerPrefs.GetInt("idlevel_inform_Animal_EP" + i.ToString());
                Animal_Damage[i] = PlayerPrefs.GetInt("idlevel_inform_Animal_Damage" + i.ToString());
                animal_sex[i] = PlayerPrefs.GetInt("idlevel_inform_animalSex" + i.ToString());
                Is[i] = PlayerPrefs.GetInt("idlevel_inform_Is" + i.ToString()) == 1 ? true : false;
                sickpanel[i] = PlayerPrefs.GetInt("idlevel_inform_sickpanel" + i.ToString()) == 1? true : false;
                hungry[i] = PlayerPrefs.GetInt("idlevel_inform_hungry" + i.ToString()) == 1 ? true : false;
                IsBaby[i] = PlayerPrefs.GetInt("idlevel_inform_IsBaby" + i.ToString()) == 1? true : false;
            }
        }
    }
}