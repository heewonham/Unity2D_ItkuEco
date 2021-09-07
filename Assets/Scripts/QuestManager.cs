using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questID = 10;
    public int questActionIndex = 0; // 퀘스트 대화 순서 변수
    Dictionary<int, QuestData> questList;

    public FoodManager food;
    public Animal animal;
    public GameManager manager;
    public saveManager save;

    public GameObject popUp;
    public GameObject brother;
    public Text popUptext;
    public Text detailtext;

    public int QuestanimalA;
    public int QuestanimalF;
    public int QuestanimalM;
    public int Npcid = 0;

    int temp1 = 0;
    int temp2 = 0;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
        Detail(questID + questActionIndex);
    }
    void OnEnable()
    {
        questActionIndex = save.sQuestActionIndex;
        QuestanimalA = save.sQuestanimalA;
        QuestanimalF = save.sQuestanimalF;
        QuestanimalM = save.sQuestanimalM;
        questID = save.sQuestID;
    }
    void Update()
    {
        Detail(questID + questActionIndex);
        if (questList.ContainsKey(questID))
            Npcid = questList[questID].npcId[questActionIndex];
    }
    void GenerateData() // 퀘스트 번호 , 퀘스트 대화자들
    {
        questList.Add(0, new QuestData("새로운시작", new int[] { 0 }));
        questList.Add(10, new QuestData("<마을사람들과 인사하기>", new int[] {2000,0,2000,1500,2500,1000,0,1000,0,0}));
        questList.Add(20, new QuestData("<아~파릇 마을 도와주기>", new int[] { 5000,4000,3000,0,0,3000,0,0 }));
        questList.Add(30, new QuestData("<향긋한 봄냄새를 맡고 싶어!!>", new int[] { 1500,0,1500,0,1500,0 }));
        questList.Add(40, new QuestData("<음~ 배고파! 나 좀 도와줘!>", new int[] { 4500,0,4500,0,4500,0,0,4500,0,0 }));
        questList.Add(50, new QuestData("<산신령의 친구 살려주기>", new int[] { 2000,0,500,0,500,0 }));
        questList.Add(60, new QuestData("<으악!!! 막내동생을 찾아줘>", new int[] { 1000,2500,0,8500,0,2500,0,2500,0 }));
        questList.Add(70, new QuestData("<천고마비.. 아~높아 마을 도와주기>", new int[] { 5500,0,0,5500,0,0 }));
        questList.Add(80, new QuestData("<아~차거 마을 인사가기>", new int[] { 7000,8000,0,0,8000,0,8000,7500,0,0 }));
        questList.Add(90, new QuestData("<계절 원석에 대한 비밀1>", new int[] { 1500,0,0,1500,0,0 }));
        questList.Add(100, new QuestData("<비상비상!! 성비불균형발생?>", new int[] { 5000,0,0,5000,0,0 }));
        questList.Add(110, new QuestData("<친구에게 전하는 소중한 마음>", new int[] { 3500,0,8000,0,8000,0,3500,0,0 }));
        questList.Add(120, new QuestData("<계절 원석에 대한 비밀2>", new int[] { 6500,0,0,6500,0,6500,6000,0 }));
        questList.Add(130, new QuestData("<혹시 나를 도와줄 수 있니?>", new int[] { 8000,0,0,8000,0,0 }));
        questList.Add(140, new QuestData("<봄 계절 원석 만들기1>", new int[] { 2000,4000,0,0,4000,0,2000,0,2000,0,0 }));
        questList.Add(160, new QuestData("<여름 계절 원석 만들기2>", new int[] { 2000,4000,0,0,4000,0,2000,0,2000,0,0 })); ;
        questList.Add(180, new QuestData("<가을 계절 원석 만들기3>", new int[] { 2000, 6500,0,0,6500,0, 2000, 0, 2000, 0, 0 })); ;
        questList.Add(200, new QuestData("<겨울 계절 원석 만들기4>", new int[] { 2000,8000,0,0, 8000,0, 2000,0, 2000, 0, 0 })); ;
    }
    public int GetQuestTalkIndex(int id)
    {
        return questID + questActionIndex;
    }
    public void QuestLoad()
    {
        if (manager.year >= 3 && manager.month >= 3 && manager.date >= 30)
            SceneManager.LoadScene(3);
        else if (questList[questID].npcId.Length <= questActionIndex + 1)
        {
            if (manager.year >= 3) // 3년차
            {
                if (questID == 180 && manager.month >= 3 && manager.date >= 1) // 180
                    EndingQuest();
                else if (questID == 160 && manager.month >= 2 && manager.date >= 1) // 180
                    EndingQuest();
                else if (questID == 140 && manager.month >= 1 && manager.date >= 1) // 160
                    EndingQuest();
                else if (questID == 130 && manager.month >= 0 && manager.date >= 1) // 140
                    NextQuest();
            }
            
            if(manager.year >= 2) //2년차
            {
                if (questID == 120 && manager.month >= 3 && manager.date >= 1) //130
                    NextQuest();
                else if (questID == 110 && manager.month >= 2 && manager.date >= 1) //120
                    NextQuest();
                else if (questID == 100 && manager.month >= 1 && manager.date >= 20) // 110
                    NextQuest();
                else if (questID == 90 && manager.month >= 1 && manager.date >= 1) // 100
                    NextQuest();
                else if (questID == 80 && manager.month >= 0 && manager.date >= 1) // 90
                    NextQuest();
            }

            if(manager.year >= 1) //1년차
            {
                if (questID == 70 && manager.month >= 3 && manager.date >= 1) // 80
                    NextQuest();
               else if (questID == 60 && manager.month >= 2 && manager.date >= 5) // 70
                    NextQuest();
                else if (questID == 50 && manager.month >= 2 && manager.date >= 1) // 60
                    NextQuest();
                else if (questID == 40 && manager.month >= 1 && manager.date >= 20) // 50
                    NextQuest();
                else if (questID == 30 && manager.month >= 1 && manager.date >= 1) //40
                    NextQuest();
                else if (questID == 20 && manager.month >= 0 && manager.date >= 20) //30
                    NextQuest();
                else if (questID == 10 && manager.month >= 0 && manager.date >= 3) // 20
                    NextQuest();
            }
        }
    }
    public string CheckQuest() // 오버로딩 함수
    {
        // Quest Name
        if(questList.ContainsKey(questID))
            return questList[questID].questName;
        return null;
    }
    public string CheckQuest(int id)
    {
        // Next Talk Target
        if (id == questList[questID].npcId[questActionIndex])
            questActionIndex++;

        // Talk Complete & Next Quest
        mission(questID + questActionIndex);

        // Quest Name
        return questList[questID].questName;

    }
    void NextQuest()
    {
        questID += 10;
        questActionIndex = 0;
    }
    void EndingQuest()
    {
        questID += 20;
        questActionIndex = 0;
    }
    void Detail(int id)
    {
        switch (id)
        {
            case 10: case 11: case 12:  detailtext.text = "산신령 찾아가기"; break;
            case 13: detailtext.text = "겉핥기 찾아가기"; break;
            case 14: detailtext.text = "샐러다 찾아가기"; break;
            case 15: case 17: case 16: detailtext.text = "다판다 찾아가기"; break;
            case 18: case 19:  detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 20: detailtext.text = "아~ 파릇마을 타조 찾아가기"; break;
            case 21: detailtext.text = "아~ 파릇마을 수장 찾아가기"; break;
            case 22: detailtext.text = "래서팬더 찾아가기"; break;
            case 23: case 24: temp1 = animal.Completed[0, 0] + animal.Completed[0, 1] - QuestanimalA;
                detailtext.text = "랫서팬더 키우기 :\n" + temp1 +"/ 8"; mission(id); break;
            case 25: detailtext.text = "완료!!! 랫서팬더 찾아가기"; break;
            case 26: case 27: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 30: detailtext.text = "겉핥기 찾아가기"; break;
            case 31: detailtext.text = "꽃잎줍기 :\n" + manager.flowercount + "/ 15"; mission(id); break;
            case 32: detailtext.text = "꽃잎줍기 :\n" + manager.flowercount + "/ 15 \n겉핥기 찾아가기"; break;
            case 33: case 34: case 35:  detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 40: detailtext.text = "아~파릇 미어캣 찾아가기"; break;
            case 41: detailtext.text = "지렁이 구하기 :\n" + food.ER_count + "/ 10"; mission(id); break;
            case 42: detailtext.text = "지렁이 구하기 :\n" + food.ER_count + "/ 10  \n미어캣 찾아가기";break;
            case 43: case 44: detailtext.text = "완료! 또 다른 도움?"; break;
            case 45: case 46:
                temp1 = animal.Completed[3, 0] - QuestanimalM; temp2 = animal.Completed[3, 1] - QuestanimalF;
                detailtext.text = "미어캣 키우기 :\n수컷 = " + temp1 + "/ 6\n" + "암컷 =" + temp2 + "/6"; mission(id); break;
            case 47: detailtext.text = "완료!!! 미어캣 찾아가기"; break;
            case 48: case 49: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 50: detailtext.text = "산신령 찾아가기"; break;
            case 51: detailtext.text = "거름줍기 :\n" + manager.poopcount + "/ 10"; mission(id); break;
            case 52: detailtext.text = "거름줍기 :\n" + manager.poopcount + "/ 10 \n정령나무 찾아가기"; break;
            case 53: case 54: detailtext.text = "정령나무 찾아가기"; break;
            case 55: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 60: detailtext.text = "다판다 찾아가기"; break;
            case 61: detailtext.text = "샐러다 찾아가기"; break;
            case 62: case 63: detailtext.text = "동물마을에 숨어있는 동생찾기"; break;
            case 64: case 65: case 66: case 67: detailtext.text = "샐러다 찾아가기"; break;
            case 68: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 70: case 74: detailtext.text = "하늘다람쥐 찾아가기"; break;
            case 71: case 72:
                temp1 = animal.Completed[5, 0] - QuestanimalM;
                detailtext.text = "하늘다람쥐 키우기 :\n 수컷 =" + temp1 + "/ 9\n"; mission(id); break;
            case 73: detailtext.text = "완료!!! 하늘다람쥐 찾아가기"; break;
            case 75: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 80: detailtext.text = "펭귄 찾아가기"; break;
            case 81: detailtext.text = "북극여우 찾아가기"; break;
            case 82: case 83:
                temp1 = animal.Completed[8, 0] + animal.Completed[8, 1] - QuestanimalM;
                temp2 = animal.Completed[9, 0] + animal.Completed[9, 1] - QuestanimalF;
                detailtext.text = "펭귄 키우기 :" + temp1 + "/ 5\n 물범 키우기 : " + temp2 + "/ 5"; mission(id); break;
            case 84: case 85: case 86:  detailtext.text = "완료!!! 북극여우 찾아가기"; break;
            case 87: case 88: detailtext.text = "하프물범 찾아가기"; break;
            case 89: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 90: case 93: case 94: detailtext.text = "겉핥기 찾아가기"; break;
            case 91: case 92:
                temp1 = animal.Completed[1, 1] - QuestanimalA;
                detailtext.text = "반달가슴곰 암컷 :" + temp1 + "/ 6"; mission(id); break;
            case 95: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 100: detailtext.text = "타조 찾아가기"; break;
            case 101: case 102:
                temp1 = animal.Completed[4, 0] - QuestanimalM;
                detailtext.text = "타조 수컷 :" + temp1 + "/ 8"; mission(id); break;
            case 103: case 104: detailtext.text = "완료! 타조 찾아가기"; break;
            case 105: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 110: detailtext.text = "반달가슴곰 찾아가기"; break;
            case 111: detailtext.text = "꽃잎줍기 :\n" + manager.flowercount + "/ 15"; mission(id); break;
            case 112: detailtext.text = "꽃잎줍기 :\n" + manager.flowercount + "/ 15 \n북극여우 찾아가기"; break;
            case 113: case 114: detailtext.text = "친구에게 보답하기"; break;
            case 115: detailtext.text = "고기(소) 구하기 :\n" + food.MTs_count + "/ 15"; mission(id); break;
            case 116: detailtext.text = "고기(소) 구하기 :\n" + food.ER_count + "/ 15  \n반달가슴곰 찾아가기"; break;
            case 117: case 118: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 120: detailtext.text = "독수리 찾아가기"; break;
            case 121: case 122:
                temp1 = animal.Completed[6, 0] + animal.Completed[6, 1] - QuestanimalM;
                temp2 = animal.Completed[7, 0] + animal.Completed[7, 1] - QuestanimalF;
                detailtext.text = "두루미 키우기 :" + temp1 + "/ 5\n 독수리 키우기 : " + temp2 + "/ 5"; mission(id); break;
            case 123: case 124: detailtext.text = "완료! 독수리 찾아가기"; break;
            case 125: case 126: detailtext.text = "추가 정보?? 두루미 찾아가기"; break;
            case 127: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 130: detailtext.text = "북극여우 찾아가기"; break;
            case 131: case 132:
                temp1 = animal.Completed[10, 0] - QuestanimalM;
                temp2 = animal.Completed[10, 1] - QuestanimalF;
                detailtext.text = "북극여우 수컷 :" + temp1 + "/ 5\n북극여우 암컷 : " + temp2 + "/ 5"; mission(id); break;
            case 133: case 134: detailtext.text = "완료! 북극여우 찾아가기"; break;
            case 135: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 140: detailtext.text = "산신령 찾아가기"; break;
            case 141: detailtext.text = "사자 찾아가기"; break;
            case 142: case 143:
                temp1 = animal.Completed[0, 0] + animal.Completed[1, 0] + animal.Completed[2, 0] - QuestanimalA;
                temp2 = animal.Completed[0, 1] + animal.Completed[1, 1] + animal.Completed[2, 1] - QuestanimalF;
                detailtext.text = "봄 동물 수컷 : "+ temp1 + "/ 8\n봄 동물 암컷 : "+temp2 + "/ 8"; mission(id); break;
            case 144: case 145: detailtext.text = "완료! 사자 찾아가기"; break;
            case 146: detailtext.text = "산신령에게 찾아가기"; break;
            case 147: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000"; mission(id); break;
            case 148: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000\n산신령 찾아가기"; break;
            case 150: case 149: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 160: detailtext.text = "산신령 찾아가기"; break;
            case 161: detailtext.text = "사자 찾아가기"; break;
            case 162:
            case 163:
                temp1 = animal.Completed[3, 0] + animal.Completed[4, 0] - QuestanimalA;
                temp2 = animal.Completed[3, 1] + animal.Completed[4, 1]  - QuestanimalF;
                detailtext.text = "여름 동물 수컷 : " + temp1 + "/ 8\n여름 동물 암컷 : " + temp2 + "/ 8"; mission(id); break;
            case 164: case 165: detailtext.text = "완료! 사자 찾아가기"; break;
            case 166: detailtext.text = "산신령에게 찾아가기"; break;
            case 167: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000"; mission(id); break;
            case 168: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000\n산신령 찾아가기"; break;
            case 170: case 169: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 180: detailtext.text = "산신령 찾아가기"; break;
            case 181: detailtext.text = "독수리 찾아가기"; break;
            case 182:
            case 183:
                temp1 = animal.Completed[5, 0] + animal.Completed[6, 0] + animal.Completed[7, 0] - QuestanimalA;
                temp2 = animal.Completed[5, 1] + animal.Completed[6, 1] + animal.Completed[7, 1] - QuestanimalF;
                detailtext.text = "가을 동물 수컷 : " + temp1 + "/ 8\n가을 동물 암컷 : " + temp2 + "/ 8"; mission(id); break;
            case 184: case 185: detailtext.text = "완료! 독수리 찾아가기"; break;
            case 186: detailtext.text = "산신령에게 찾아가기"; break;
            case 187: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000"; mission(id); break;
            case 188: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000\n산신령 찾아가기"; break;
            case 190: case 189: detailtext.text = "완료!!! 다음 퀘스트를 기다리기\n퀘스트가 없어도 동물키우자!"; break;

            case 200: detailtext.text = "산신령 찾아가기"; break;
            case 201: detailtext.text = "북극여우 찾아가기"; break;
            case 202:
            case 203:
                temp1 = animal.Completed[8, 0] + animal.Completed[9, 0] + animal.Completed[10, 0] - QuestanimalA;
                temp2 = animal.Completed[8, 1] + animal.Completed[9, 1] + animal.Completed[10, 1] - QuestanimalF;
                detailtext.text = "겨울 동물 수컷 : " + temp1 + "/ 8\n겨울 동물 암컷 : " + temp2 + "/ 8"; mission(id); break;
            case 204: case 205: detailtext.text = "완료! 북극여우 찾아가기"; break;
            case 206: detailtext.text = "산신령에게 찾아가기"; break;
            case 207: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000"; mission(id); break;
            case 208: detailtext.text = "피스코인 18000 마련하기 :\n" + manager.peacecoin + "/ 22000\n산신령 찾아가기"; break;
            case 210: case 209: detailtext.text = "완료!!! 엔딩 기다리기"; break;
        }
    }
    void mission(int id)
    {
        switch(id)
        {
            case 0:
                NextQuest();
                break;
            case 11:
                animal.currentM += 2;
                animal.currentB += 2;
                popUp.SetActive(true);
                popUptext.text = "알 2개와 포유류2마리를 얻었습니다.";
                questActionIndex++;
                manager.TalkAction(manager.player.scanObj);
                Invoke("Closepop", 1.5f);
                break;
            case 16:
                food.revivalPortion += 2;
                popUp.SetActive(true);
                popUptext.text = "동물치유포션 2개를 얻었습니다.";
                questActionIndex++;
                manager.TalkAction(manager.player.scanObj);
                Invoke("Closepop", 1.5f);
                break;
            case 18 :
                PopQuest(100, 500);
                break;
            case 23:
                QuestanimalA = animal.Completed[0, 0] + animal.Completed[0, 1];
                questActionIndex++;
                break;
            case 24:
                if(animal.Completed[0, 0] + animal.Completed[0, 1] - QuestanimalA >= 8)
                    questActionIndex++;
                break;
            case 26:
                PopQuest(1600, 1200);
                break;
            case 31:
                if(manager.flowercount >= 15)
                    questActionIndex++;
                break;
            case 33 :
                manager.flowercount -= 15;
                PopQuest(250, 350);
                manager.TalkAction(manager.player.scanObj);
                break;
            case 41:
                if (food.ER_count >= 10)
                    questActionIndex++;
                break;
            case 43:
                food.ER_count -= 10;
                PopQuest(100, 200);
                manager.TalkAction(manager.player.scanObj);
                break;
            case 45:
                QuestanimalM = animal.Completed[3, 0];
                QuestanimalF = animal.Completed[3, 1];
                questActionIndex++;
                break;
            case 46:
                if (animal.Completed[3, 0] - QuestanimalM >= 6 && animal.Completed[3, 1] - QuestanimalF >= 6)
                    questActionIndex++;
                break;
            case 48:
                PopQuest(2400, 1800);
                break;
            case 51:
                if (manager.poopcount >= 10)
                    questActionIndex++;
                break;
            case 53:
                PopQuest(200, 300);
                manager.TalkAction(manager.player.scanObj);
                break;
            case 62:
                brother.SetActive(true);
                questActionIndex++;
                break;
            case 64:
                PopQuest(100, 200);
                break;
            case 66:
                PopQuest(200, 500);
                brother.SetActive(false);
                manager.TalkAction(manager.player.scanObj);
                break;
            case 71:
                QuestanimalM = animal.Completed[5, 0];
                questActionIndex++;
                break;
            case 72:
                if (animal.Completed[5, 0] - QuestanimalM >= 9)
                    questActionIndex++;
                break;
            case 74:
                PopQuest(2400, 1500);
                break;
            case 82:
                QuestanimalM = animal.Completed[8, 0]+animal.Completed[8,1]; // 펭귄
                QuestanimalF = animal.Completed[9, 0] + animal.Completed[9, 1]; // 물범
                questActionIndex++;
                break;
            case 83:
                if (animal.Completed[8, 0] + animal.Completed[8, 1] - QuestanimalM >= 5 &&
                    animal.Completed[9, 0] + animal.Completed[9, 1] - QuestanimalF >= 5)
                    questActionIndex++;
                break;
            case 85:
                PopQuest(2500, 2100);
                manager.TalkAction(manager.player.scanObj);
                break;
            case 88:
                food.speedPortion += 2;
                popUp.SetActive(true);
                popUptext.text = "속도증가물약 2개를 얻었습니다.";
                questActionIndex++;
                manager.TalkAction(manager.player.scanObj);
                Invoke("Closepop", 1.5f);
                break;
            case 91:
                QuestanimalA = animal.Completed[1, 1];
                questActionIndex++;
                break;
            case 92:
                if (animal.Completed[1, 1] - QuestanimalA >= 6)
                    questActionIndex++;
                break;
            case 94:
                PopQuest(2400, 1600);
                break;
            case 101:
                QuestanimalM = animal.Completed[4, 0];
                questActionIndex++;
                break;
            case 102:
                if (animal.Completed[4, 0] - QuestanimalM >= 8)
                    questActionIndex++;
                break;
            case 104:
                PopQuest(2900, 2400);
                break;
            case 111:
                if (manager.flowercount >= 15)
                    questActionIndex++;
                break;
            case 113:
                PopQuest(300, 450);
                manager.TalkAction(manager.player.scanObj);
                break;
            case 115:
                if (food.MTs_count >= 15)
                    questActionIndex++;
                break;
            case 117:
                PopQuest(450, 600);
                break;
            case 121:
                QuestanimalM = animal.Completed[6, 0] + animal.Completed[6, 1]; // 두루미
                QuestanimalF = animal.Completed[7, 0] + animal.Completed[7, 1]; // 독수리
                questActionIndex++;
                break;
            case 122:
                if (animal.Completed[6, 0] + animal.Completed[6, 1] - QuestanimalM >= 5 &&
                    animal.Completed[7, 0] + animal.Completed[7, 1] - QuestanimalF >= 5)
                    questActionIndex++;
                break;
            case 124:
                PopQuest(4000, 3800);
                manager.TalkAction(manager.player.scanObj);
                break;
            case 131:
                QuestanimalM = animal.Completed[10, 0];
                QuestanimalF = animal.Completed[10, 1];
                questActionIndex++;
                break;
            case 132:
                if (animal.Completed[10, 0] - QuestanimalM >= 5 &&
                    animal.Completed[10, 1]  - QuestanimalF >= 5)
                    questActionIndex++;
                break;
            case 134:
                PopQuest(3400, 3700);
                break;
            case 142:
                QuestanimalA = animal.Completed[0, 0] +  animal.Completed[1, 0] + animal.Completed[2, 0];
                QuestanimalF = animal.Completed[0, 1] + animal.Completed[1, 1] + animal.Completed[2, 1];
                questActionIndex++;
                break;
            case 143:
                if (animal.Completed[0, 0] + animal.Completed[1, 0] + animal.Completed[2, 0] - QuestanimalA >= 8 &&
                    animal.Completed[0, 1] + animal.Completed[1, 1] + animal.Completed[2, 1] - QuestanimalF >= 8)
                    questActionIndex++;
                break;
            case 145:
                PopQuest(6000, 7500);
                break;
            case 147:
                if (manager.peacecoin >= 22000)
                    questActionIndex++;
                break;
            case 149:
                GlobalVariable.Gemstone += 1;
                manager.peacecoin -= 22000;
                popUp.SetActive(true);
                popUptext.text = "봄의 계절원석을\n얻었습니다.";
                questActionIndex++;
                manager.TalkAction(manager.player.scanObj);
                Invoke("Closepop", 1.5f);
                break;
            case 162:
                QuestanimalA = animal.Completed[3, 0] + animal.Completed[4, 0];
                QuestanimalF = animal.Completed[3, 1] + animal.Completed[4, 1];
                questActionIndex++;
                break;
            case 163:
                if (animal.Completed[3, 0] + animal.Completed[4, 0] - QuestanimalA >= 8 &&
                    animal.Completed[3, 1] + animal.Completed[4, 1] - QuestanimalF >= 8)
                    questActionIndex++;
                break;
            case 165:
                PopQuest(6000, 7500);
                break;
            case 167:
                if (manager.peacecoin >= 22000)
                    questActionIndex++;
                break;
            case 169:
                GlobalVariable.Gemstone += 1;
                manager.peacecoin -= 22000;
                popUp.SetActive(true);
                popUptext.text = "여름의 계절원석을\n얻었습니다.";
                questActionIndex++;
                manager.TalkAction(manager.player.scanObj);
                Invoke("Closepop", 1.5f);
                break;
            case 182:
                QuestanimalA = animal.Completed[5, 0] + animal.Completed[6, 0] + animal.Completed[7, 0];
                QuestanimalF = animal.Completed[5, 1] + animal.Completed[6, 1] + animal.Completed[7, 1];
                questActionIndex++;
                break;
            case 183:
                if (animal.Completed[5, 0] + animal.Completed[6, 0] + animal.Completed[7, 0] - QuestanimalA >= 8 &&
                    animal.Completed[5, 1] + animal.Completed[6, 1] + animal.Completed[7, 1] - QuestanimalF >= 8)
                    questActionIndex++;
                break;
            case 185:
                PopQuest(6000, 7500);
                break;
            case 187:
                if (manager.peacecoin >= 22000)
                    questActionIndex++;
                break;
            case 189:
                GlobalVariable.Gemstone += 1;
                manager.peacecoin -= 22000;
                popUp.SetActive(true);
                popUptext.text = "가을의 계절원석을\n얻었습니다.";
                questActionIndex++;
                manager.TalkAction(manager.player.scanObj);
                Invoke("Closepop", 1.5f);
                break;
            case 202:
                QuestanimalA = animal.Completed[8, 0] + animal.Completed[9, 0] + animal.Completed[10, 0];
                QuestanimalF = animal.Completed[8, 1] + animal.Completed[9, 1] + animal.Completed[10, 1];
                questActionIndex++;
                break;
            case 203:
                if (animal.Completed[8, 0] + animal.Completed[9, 0] + animal.Completed[10, 0] - QuestanimalA >= 8 &&
                    animal.Completed[8, 1] + animal.Completed[9, 1] + animal.Completed[10, 1] - QuestanimalF >= 8)
                    questActionIndex++;
                break;
            case 205:
                PopQuest(6000, 7500);
                break;
            case 207:
                if (manager.peacecoin >= 22000)
                    questActionIndex++;
                break;
            case 209:
                GlobalVariable.Gemstone += 1;
                manager.peacecoin -= 22000;
                popUp.SetActive(true);
                popUptext.text = "겨울의 계절원석을\n얻었습니다.";
                questActionIndex++;
                manager.TalkAction(manager.player.scanObj);
                Invoke("Closepop", 1.5f);
                break;
        }
    }
    void PopQuest(int ep, int coin)
    {
        manager.playerEp += ep;
        manager.peacecoin += coin;
        popUp.SetActive(true);
        popUptext.text = "경험치 "+ ep +",\n 피스 코인 "+ coin +"\n 을 얻었습니다.";
        questActionIndex++;
        Invoke("Closepop", 1.5f);
    }
    void Closepop()
    {
        popUp.SetActive(false);
    }
}