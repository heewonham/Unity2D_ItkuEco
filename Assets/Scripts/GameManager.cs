using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animal animal;
    public Player player;
    public UIManager uimanager;
    public FoodManager food;
    public ObjectManager objectmanager;
    public TalkManager talkManager;
    public QuestManager questmanager;
    public TalkEffect talk;
    public NPCMenu npcmanager;
    public saveManager savemanager;
    public TalkEffect talkeffect;

    public IDLevelEP[] idlevel_inform;
    public bool[] idlevel_informbool = { false };
    public bool[] itemchk = new bool [12];
    public Item[] item_inform = new Item[12]; // 0 ~8 똥, 9~11  꽃잎 

    public GameObject StartMenu;
    public GameObject FoodMenu;
    public GameObject HomeMenu;
    public GameObject SickMenu;
    public GameObject Bag1;
    public GameObject Bag2;

    public GameObject ErrorMessage;
    public GameObject UpMessage;
    public GameObject noanimal; // 더 이상 키울수 없다는 안내문
    public GameObject nofood; // 더 이상 음식이 없다는 안내문
    public GameObject nohungry; // 더 이상 배고프지 않다는 안내문
    public GameObject SeasonMatch; // 계절에 맞지않은 동물을 키울 경우 뜨는 안내문
    public GameObject SavePop; // 저장되었다는 안내문
    public GameObject shock; // 기절했다는 표시
    public GameObject ExitRechk; // 나가는 거 다시 확인.
    public GameObject TopPenel;// sound 와 게임끝내기

    public GameObject showMove; // 포탈 이동 후 이동완료 안내문
    public GameObject showend; //동물 성장 후 안내문
    public GameObject GetPoop; // 거름을 얻었다는 안내문
    public GameObject GetFlower; // 거름을 얻었다는 안내문
    public GameObject SendSign; // 산싱령에게 보냈다는 표시
    public GameObject RevivalSign; // 부활했다는 표시
    public GameObject popMornig;// 아침표시

    // 지도
    public GameObject Mapping; // map Open
    public GameObject[] Maps = new GameObject[4]; // Map표시
    public Text MapText; // Map 이름표시
    public Image pos; // 현재 위치표시

    public Transform playertran;
    public GameObject Morning;
    public GameObject playerscan;
    public GameObject[] Ground;
    IDLevelEP iDLevelEP;
    AnimalMove move;

    // 퀘스트 관련
    public Text questTalk;
    public int talkIndex;
    public Animator talkPanel; // GameObject 
    GameObject scanObj;
    public bool IsTalking = false; // 토크 진행중
    public GameObject[] QuestPop;
    int index;

    // 효과음
    public Slider BGMslider;
    public Slider Effectslider;
    public AudioSource[] audios = new AudioSource[7];

    public bool isAction;
    int sex;
    float weigh;
    public int poopcount;
    public int flowercount;
    public bool sick = false;

    // 날짜
    public int year;
    public string[] Season;
    public int date;
    public int month;
    public float time;
    public float leveltime = 150f; // 레벨에 따른 시간

    // Player 레벨, 피, 경험치, 돈
    public int[] levelblood = new int[16]; // 레벨에 따른 체력 표시
    public int[] levelEp= new int[16]; // 레벨에 따른 경험치 표시
    public float playerbloodminus;
    public int playerEp = 0;
    public int playerlevel; // 플레이어의 레벨
    public int peacecoin = 500;

    //전면 광고 ID가 들어가는 곳입니다.
    //ca-app-pub-3940256099942544/1033173712
    // 실제 광고 : ca-app-pub-2288075540498274/1054119308";
    private readonly string unitID = "ca-app-pub-2288075540498274/1054119308";
    private InterstitialAd screenAd;
    void Awake()
    {
        audios[0] = audios[0].GetComponent<AudioSource>();
        audios[1] = audios[1].GetComponent<AudioSource>();
        audios[2] = audios[2].GetComponent<AudioSource>();
        audios[3] = audios[3].GetComponent<AudioSource>();
        audios[4] = audios[4].GetComponent<AudioSource>();
        audios[5] = audios[5].GetComponent<AudioSource>();
        audios[6] = audios[6].GetComponent<AudioSource>();
    }
    void Start()
    {
        MobileAds.Initialize((initStatus) => InitAd());
    }
    // 광고 관련
    private void InitAd()
    {
        string id = unitID;
        screenAd = new InterstitialAd(id);

        screenAd.OnAdClosed += HandleOnAdLoaded;
        screenAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        screenAd.OnAdOpening += HandleOnAdOpening;
        screenAd.OnAdClosed += HandleOnAdClosed;
        screenAd.OnAdLeavingApplication += HandleonAdLeavingApplication;

        AdLoad();
    }
    private void AdLoad()
    {
        AdRequest request = new AdRequest.Builder().Build();
        screenAd.LoadAd(request);
    }
    public void HandleOnAdLoaded(object sender, EventArgs args) { }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) { }
    public void HandleOnAdOpening(object sender, EventArgs args) { }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        screenAd.Destroy();
        AdLoad();
    }
    public void HandleonAdLeavingApplication(object sender, EventArgs args) { }

    public void adShow()
    {
        StartCoroutine("ShowScreenAd");
    }
    private IEnumerator ShowScreenAd()
    {
        while (!screenAd.IsLoaded())
        {
            yield return null;
        }
        screenAd.Show();
    }
    void OnEnable()
    {
        playertran.position = new Vector3(savemanager.playerx, savemanager.playery, -0.1f);
        poopcount = savemanager.sPoopcount;
        flowercount = savemanager.sFlowercount;
        sick = savemanager.sPlayersick;
        year = savemanager.sYear;
        date = savemanager.sDate;
        month = savemanager.sMonth;
        time = savemanager.sTime;
        playerbloodminus = savemanager.sPlayerbloodminus;
        playerEp = savemanager.sPlayerEp;
         playerlevel = savemanager.sPlayerlevel;
        peacecoin = savemanager.sPeacecoin;
        GlobalVariable.Gemstone = PlayerPrefs.GetInt("gemstone");
        BGMslider.value = PlayerPrefs.GetFloat("sfxVol");
        Effectslider.value = PlayerPrefs.GetFloat("bgmVol");

        for (int i = 0; i < 12; i++)
        {
            idlevel_informbool[i] = savemanager.sidlevel_informbool[i];
        }
    }
    void Update()
    {
        volumBGM();
        volumEffect();
        Timeflow(); //시간 흐름
        Die();  // 플레이어 죽음
        player.levelup(); // 레벨업
        GroundChange(); // 땅 바꾸기
        if(player.AnimalObj != null)
        {
            playerscan = player.AnimalObj;
            iDLevelEP = playerscan.GetComponent<IDLevelEP>();
        }
        questTalk.text = questmanager.CheckQuest();
        questpopup(questmanager.Npcid);
    }
    public void volumBGM()
    {
        GlobalVariable.bgmVol = BGMslider.value;
        audios[0].volume = GlobalVariable.bgmVol;
    }
    public void volumEffect()
    {
        GlobalVariable.sfxVol = Effectslider.value;

        audios[1].volume = GlobalVariable.sfxVol; // 클릭
        audios[2].volume = GlobalVariable.sfxVol; // 성공
        audios[3].volume = GlobalVariable.sfxVol; // Die
        audios[4].volume = GlobalVariable.sfxVol; // 아침
        audios[5].volume = GlobalVariable.sfxVol; // 밥먹을때
        audios[6].volume = GlobalVariable.sfxVol; // Random
        talkeffect.audioSource.volume = GlobalVariable.sfxVol; // 키보드 소리
    }
    void questpopup(int id)
    {
        switch (id)
        {
            case 500: index = 0; break;
            case 1000: index = 1; break;
            case 1500: index = 2; break;
            case 2000: index = 3; break;
            case 2500: index = 4; break;
            case 3000: index = 5; break;
            case 3500: index = 6; break;
            case 4000: index = 7; break;
            case 4500: index = 8; break;
            case 5000: index = 9; break;
            case 5500: index = 10; break;
            case 6000: index = 11; break;
            case 6500: index = 12; break;
            case 7000: index = 13; break;
            case 7500: index = 14; break;
            case 8000: index = 15; break;
            default:
                index = 20; break;
        }

        for(int i = 0; i < 16; i++)
        {
            if (i == index)
                QuestPop[i].SetActive(true);
            else
                QuestPop[i].SetActive(false);
        }
    }
    // 계절별 변화
    void GroundChange()
    {
        if(month == 0 || month == 1)
        {
            Ground[2].SetActive(false); // 겨울
            Ground[1].SetActive(false); // 가울
            Ground[0].SetActive(true); // 봄
            Ground[3].SetActive(true); // 꽃
        }
        else if(month == 2)
        {
            Ground[0].SetActive(false);
            Ground[3].SetActive(false);
            Ground[1].SetActive(true);
            Ground[2].SetActive(false); // 겨울
        }
        else if(month == 3)
        {
            Ground[1].SetActive(false);
            Ground[2].SetActive(true);
            Ground[0].SetActive(false); // 봄
            Ground[3].SetActive(false); // 꽃
        }
    }
    void Die()
    {
        if (!sick&& playerbloodminus >= levelblood[playerlevel] *0.75f)
        {
            sick = true;
            npcmanager.AllPop.text = "체력이 부족해요!!!";
            player.playersick1();
        }
        else if(sick && playerbloodminus <= levelblood[playerlevel] * 0.75f)
        {
            player.playercolorreset();
            npcmanager.AllPop.text = "";
        }

        if(playerbloodminus >= levelblood[playerlevel])
        {
            audios[3].Play();
            VanishItem();
            timechange();
            shock.SetActive(true);
            playertran.position = new Vector2(23,17.5f);
            playerbloodminus = levelblood[playerlevel] / 2;
        }
    }
    public void MenuOpen() // 동물과 관련된 메뉴
    {
        if (iDLevelEP.Animal_Damage >= 2)
            SickOpen();
        else if (!iDLevelEP.Is)
        {
            if (idlevel_informbool[iDLevelEP.Num])
                return;

            if (!StartMenu.activeSelf)
            {
                StartMenu.SetActive(true);
                isAction = true;
            }
            else
            {
                StartMenu.SetActive(false);
                isAction = false;
            }
        }
        else
        {
            if (!FoodMenu.activeSelf)
            {
                Time.timeScale = 0;
                if (iDLevelEP.Animal_EP == 2 || (iDLevelEP.Animal_EP == 10 || iDLevelEP.Animal_EP == 11))
                    UpMessage.SetActive(true);
                FoodMenu.SetActive(true);
                isAction = true;
            }
            else
            {
                FoodMenu.SetActive(false);
                isAction = false;
                Time.timeScale = 1;
                if (!iDLevelEP.Isbaby)
                {
                    move = playerscan.GetComponent<AnimalMove>();
                    move.isAct = move.ismoving;
                }
            }
        }
    }
    public void HomeMenuOpen() // 저장과 종료, 다음날로 이동
    {
        if (!HomeMenu.activeSelf)
        {
            Time.timeScale = 0;
            HomeMenu.SetActive(true);
            isAction = true;
        }
        else
        {
            Time.timeScale = 1;
            HomeMenu.SetActive(false);
            isAction = false;
        }
    }
    public void SickOpen()
    {
        if (!SickMenu.activeSelf)
        {
            Time.timeScale = 0;
            SickMenu.SetActive(true);
            isAction = true;
        }
        else
        {
            Time.timeScale = 1;
            SickMenu.SetActive(false);
            isAction = false;
        }
    }
    public void Close()
    {
        Time.timeScale = 1;
        uimanager.Foodcheck();
        if (StartMenu.activeSelf)
        {
            StartMenu.SetActive(false);
            isAction = false;
        }
        else if (FoodMenu.activeSelf)
        {
            FoodMenu.SetActive(false);
            isAction = false;
            if (!iDLevelEP.Isbaby)
            {
                move = playerscan.GetComponent<AnimalMove>();
                move.isAct = move.ismoving;
            }
        }
        else if (HomeMenu.activeSelf)
        {
            HomeMenu.SetActive(false);
            isAction = false;
        }
        else if(SickMenu.activeSelf)
        {
            SickMenu.SetActive(false);
            isAction = false;
        }
    }
    void Timeflow() // 시간흐름
    {
        time += Time.deltaTime;
        if (time >= leveltime)
        {
            timechange();
            playertran.position = new Vector2(23, 17.5f);
            playerbloodminus = levelblood[playerlevel] / 2;
        }
    }
    // 시간 흐름
    void timechange()
    {
        audios[4].Play();
        npcmanager.AllPop.text = "";
        player.playercolorreset();
        playerspeedreset();
        if (date == 30)
        {
            if (month == 3)
            {
                month = 0;
                year++;
                date = 1;
            }
            else
            {
                month++;
                date = 1;
            }
        }
        else
            date++;

        animalTimechange();
        time = 0;
        questmanager.QuestLoad();
        SetPoop();
        SetFlowerL();
        popMornig.SetActive(true);
        Invoke("UnpopMornig", 1f);
        Animator morn = Morning.GetComponent<Animator>();
        morn.SetTrigger("Morning");
        PopAd();
    }
    void PopAd()
    {
        if(date == 10 || date == 20 || date == 30)
        {
            adShow();
        }
    }
    void UnpopMornig()
    {
        popMornig.SetActive(false);
    }
    void animalTimechange()
    {
        for (int i = 0; i < 12; i++)
        {
            if (idlevel_informbool[i])
            {
                idlevel_inform[i].TimeFlow();
            }
        }

    }
    public void TalkAction(GameObject scan)
    {
        // Get Current Object
        scanObj = scan;
        ObjData objData = scanObj.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        // Visible Talk for Action
        talkPanel.SetBool("IsShow", isAction);
    }
    void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questmanager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        // End Talk
        if (talkData == null)
        {
            isAction = false;
            IsTalking = false;
            talkIndex = 0;
            questTalk.text = questmanager.CheckQuest(id);
            return;
        }
        // Continue Talk
        talk.SetMsg(talkData);
        IsTalking = true;

        // Next Talk
        isAction = true;
        talkIndex++;
    }
    public void PlayerAtt(int ep, int blood)
    {
        playerbloodminus += blood;
        playerEp += ep;
    }
    public void Grow(int id,int anisex)
    {
        audios[2].Play();
        animalminus(id);
        weigh = Seasonmatch(id,id);
        if (anisex == 2)
            sex = UnityEngine.Random.Range(0, 2);
        else
            sex = anisex;
        switch (id)
        {
            case 0:
            case 3:
            case 5:
            case 6:
                peacecoin += (int)(250*weigh);
                playerEp += (int)(100*weigh);
                animal.Completed[id, sex]++;
                animal.completedanimal++;
                uimanager.completed(sex, (int)(250*weigh), id,(int)(100*weigh));
                showend.SetActive(true);
                break;
            case 1: case 10: case 9: case 8: case 4:
                peacecoin += (int)(350 * weigh);
                playerEp += (int)(150*weigh);
                animal.Completed[id, sex]++;
                animal.completedanimal++;
                uimanager.completed(sex, (int)(350*weigh), id,(int)(150*weigh));
                showend.SetActive(true);
                break;
            case 2: case 7: 
                peacecoin += (int)(500*weigh);
                playerEp += (int)(200*weigh);
                animal.Completed[id, sex]++;
                animal.completedanimal++;
                uimanager.completed(sex, (int)(500*weigh), id,(int)(200*weigh));
                showend.SetActive(true);
                break;
        }
    }
    public void PlayerAbilityButton(string type)
    {
        switch (type)
        {
            case "bloodup":
                audios[1].Play();
                if (food.bloodPortion > 0)
                {
                    food.bloodPortion--;
                    npcmanager.AllPop.text = "플레이어의 체력이 20% 상승하였습니다.";
                    audios[5].Play();
                    Invoke("allpopreset", 1f);
                    if (playerbloodminus < levelblood[playerlevel] / 5)
                        playerbloodminus = 0;
                    else
                        playerbloodminus -= levelblood[playerlevel] / 5;
                }
                break;
            case "speedup":
                audios[1].Play();
                if (food.speedPortion > 0)
                {
                    food.speedPortion--;
                    npcmanager.AllPop.text = "플레이어의 속도가 하루동안 2배로 상승합니다.";
                    audios[5].Play();
                    Invoke("allpopreset", 1f);
                    player.Speed = 15;
                }
                break;
            case "taxi":
                audios[1].Play();
                if (peacecoin >= 50)
                {
                    playertran.position = new Vector2(23, 17.5f);
                    peacecoin -= 50;
                    npcmanager.AllPop.text = "집으로 이동완료";
                    Invoke("allpopreset", 1f);
                }
                break;
        }
    }
    void allpopreset()
    {
        npcmanager.AllPop.text = "";
    }
    void playerspeedreset()
    {
        player.Speed = 10;
    }
    public void DoorAct(string name)
    {
        switch (name)
        {
            case "ForestDoor" :
                player.transform.position = new Vector3(-87.72f, -101f, -0.2f);
                showMove.SetActive(true);
                break;
            case "SkyDoor":
                player.transform.position = new Vector3(-93.5f,27.4f,-0.2f);
                showMove.SetActive(true);
                break;
            case "IceDoor":
                player.transform.position = new Vector3(-128.2f,-33.65f,-0.2f);
                showMove.SetActive(true);
                break;
            case "ForestHomeDoor" :
                player.transform.position = new Vector3(-6.51f, 3.16f, -0.2f);
                showMove.SetActive(true);
                break;
            case "SkyHomeDoor":
                player.transform.position = new Vector3(42.18f, -15.72f, -0.2f);
                showMove.SetActive(true);
                break;
            case "IceHomeDoor":
                player.transform.position = new Vector3(42.5f, -1.55f, -0.2f);
                showMove.SetActive(true);
                break;
        }
    }
    void SetPoop() // 똥 생성
    {
        int maxnum = (int)(animal.AnimalNS * 0.7);
        int num = UnityEngine.Random.Range(0,maxnum);
        while (num > 0)
        {
            GameObject poopSet = objectmanager.MakeObj("Poop");
            poopSet.transform.position = new Vector2(UnityEngine.Random.Range(2.5f, 13.6f), UnityEngine.Random.Range(10, 17));
            item_inform[num] = poopSet.GetComponent<Item>();
            itemchk[num] = true; // 0 ~8 똥, 9~11  꽃잎 
            num--;
        }
    }
    void SetFlowerL()
    {
        if (month <= 1)
        {
            int num = UnityEngine.Random.Range(0, 4);
            while (num > 0)
            {
                GameObject FlowerSet = objectmanager.MakeObj("Flower");
                FlowerSet.transform.position  = new Vector2(UnityEngine.Random.Range(40.2f, 51.3f), UnityEngine.Random.Range(12.6f,22.6f));
                item_inform[11-num] = FlowerSet.GetComponent<Item>();
                itemchk[11-num] = true; // 0 ~8 똥, 9~11  꽃잎 
                num--;
            }
        }
    }
    void VanishItem()
    {
        for(int i = 0; i < 12; i++)
        {
            if (itemchk[i])
            {
                itemchk[i] = false;
                item_inform[i].Vanishing();
            }
        }
    }
    void animalminus(int id)
    {
        switch (id)
        {
            case 6: case 7: case 8: case 4:
                animal.BringUpB--;
                break;
            default:
                animal.BringUpM--;
                break;
        }
    }
    public float Seasonmatch(int id)
    {
        switch (id)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                if (month == 0 || month == 1)
                    weigh = 1;
                else
                    weigh = 0.8f;
                break;
            case 5:
            case 6:
            case 7:
                if (month == 2)
                    weigh = 1;
                else
                    weigh = 0.8f;
                break;
            case 8:
            case 9:
            case 10:
                if (month == 3)
                    weigh = 1;
                else
                    weigh = 0.8f;
                break;
        }
        if(weigh == 0.8f)
            SeasonMatch.SetActive(true);
        return weigh;
    }
    float Seasonmatch(int id, int end)
    {
        int a = end;
        switch (id)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                if (month == 1 || month == 0)
                    weigh = 1;
                else
                    weigh = 0.8f;
                break;
            case 5:
            case 6:
            case 7:
                if (month == 2)
                    weigh = 1;
                else
                    weigh = 0.8f;
                break;
            case 8:
            case 9:
            case 10:
                if (month == 3)
                    weigh = 1;
                else
                    weigh = 0.8f;
                break;
        }
        return weigh;
    }
    public void SickButton(string type)
    {
        switch (type)
        {
            case "Send":
                audios[1].Play();
                idlevel_informbool[iDLevelEP.Num] = false;
                idlevel_inform[iDLevelEP.Num] = null;
                if (iDLevelEP.Isbaby)
                {
                    if (iDLevelEP.Animal_Id == 1 || iDLevelEP.Animal_Id == 2)
                        animal.BringUpB--;
                    else if (iDLevelEP.Animal_Id == 3 || iDLevelEP.Animal_Id == 4)
                        animal.BringUpM--;
                    iDLevelEP.reset();
                }
                else
                {
                    animalminus(iDLevelEP.ArrayID);
                    iDLevelEP.Restart();
                    move = playerscan.GetComponent<AnimalMove>();
                    move.isAct = move.ismoving;
                }
                Close();
                audios[3].Play();
                SendSign.SetActive(true);
                break;
            case "Safe":
                audios[1].Play();
                if (food.revivalPortion > 0)
                {
                    iDLevelEP.revival();
                    if (!iDLevelEP.Isbaby)
                    {
                        move = playerscan.GetComponent<AnimalMove>();
                        move.isAct = move.ismoving;
                    }
                    Close();
                    audios[2].Play();
                    RevivalSign.SetActive(true);
                }
                else
                    nofood.SetActive(true);
                break;
        }
    }
    public void HomeButton(string type)
    {
        switch (type)
        {
            case "Sleep":
                audios[1].Play();
                VanishItem();
                timechange();
                playerbloodminus = 0;
                playertran.position = new Vector2(23, 17.5f);
                Close();
                break;
            case "Save":
                audios[1].Play();
                savemanager.Save();
                Close();
                SavePop.SetActive(true);
                break;
            case "Bye":
                audios[1].Play();
                ExitRechk.SetActive(true);
                Close();
                break;
            case "savebye":
                audios[1].Play();
                savemanager.Save();
                ExitRechk.SetActive(false);
                GameExit();
                break;
            case "nosavebye":
                audios[1].Play();
                ExitRechk.SetActive(false);
                GameExit();
                break;
            case "popupexit":
                ExitRechk.SetActive(false);
                break;
            case "Exit":
                audios[1].Play();
                Close(); // 홈메뉴를 닫겠다.
                break;
        }
    }
    public void BagButton(string type)
    {
        switch (type)
        {
            case "BagOpen":
                audios[1].Play();
                Time.timeScale = 0;
                Bag1.SetActive(true);
                isAction = true;
                break;
            case "Next":
                audios[1].Play(); ;
                Bag1.SetActive(false);
                Bag2.SetActive(true);
                break;
            case "Prev":
                audios[1].Play();
                Bag1.SetActive(true);
                Bag2.SetActive(false);
                break;
            case "Exit":
                audios[1].Play();
                Time.timeScale = 1;
                Bag1.SetActive(false);
                Bag2.SetActive(false);
                isAction = false;
                break;
        }
    }
    public void TopPanelButton(string type)
    {
        switch(type)
        {
            case "TopPanelSetting":
                audios[1].Play();
                TopPenel.SetActive(true);
                break;
            case "GameExit":
                audios[1].Play();
                ExitRechk.SetActive(true);
                TopPenel.SetActive(false);
                break;
            case "Home":
                audios[1].Play();
                TopPenel.SetActive(false);
                SceneManager.LoadScene(0);
                break;
            case "PopUpExit":
                audios[1].Play();
                TopPenel.SetActive(false);
                break;
        }
    }
    public void MapButton(string type)
    {
        switch (type)
        {
            case "MapOpen":
                audios[1].Play();
                MapText.text = "필리아";
                Mapping.SetActive(true);
                Maps[0].SetActive(true);
                break;
            case "MainOpen":
                audios[1].Play();
                MapText.text = "필리아";
                Maps[0].SetActive(true);
                Maps[1].SetActive(false);
                Maps[2].SetActive(false);
                Maps[3].SetActive(false);
                break;
            case "SkyOpen":
                audios[1].Play();
                MapText.text = "아높아";
                Maps[1].SetActive(true);
                Maps[0].SetActive(false);
                Maps[2].SetActive(false);
                Maps[3].SetActive(false);
                break;
            case "ForestOpen":
                audios[1].Play();
                MapText.text = "아파릇";
                Maps[2].SetActive(true);
                Maps[0].SetActive(false);
                Maps[1].SetActive(false);
                Maps[3].SetActive(false);
                break;
            case "IceOpen":
                audios[1].Play();
                MapText.text = "아차거";
                Maps[3].SetActive(true);
                Maps[0].SetActive(false);
                Maps[1].SetActive(false);
                Maps[2].SetActive(false);
                break;
            case "Position":
                Position(playertran.position.x, playertran.position.y);
                Invoke("PosReset", 3f);
                break;
            case "Exit":
                audios[1].Play();
                Mapping.SetActive(false);
                Maps[1].SetActive(false);
                Maps[2].SetActive(false);
                Maps[3].SetActive(false);
                break;
        }
    }
    void Position(float x, float y)
    {
        float posX = (x + 25) * (800f / 78.0f);
        float posY = (y + 25) * (600f / 51.0f);
        Debug.Log(posX);
        Debug.Log(posY);
        //pos.rectTransform.position = new Vector2(posX,posY);
        pos.rectTransform.anchoredPosition = new Vector2(posX, posY);
        pos.gameObject.SetActive(true);
    }
    void PosReset()
    {
        pos.gameObject.SetActive(false);
    }

    // 팝업 닫기
    public bool popClose()
    {
        Debug.Log("pop");
        if (SendSign.activeSelf)
        {
            SendSign.SetActive(false);
            return true;
        } 
        else if (RevivalSign.activeSelf)
        {
            RevivalSign.SetActive(false);
            return true;
        }
        else if (shock.activeSelf)
        {
            shock.SetActive(false);
            return true;
        }
        else if (SavePop.activeSelf)
        {
            SavePop.SetActive(false);
            return true;
        }
        else if (UpMessage.activeSelf)
        {        
            UpMessage.SetActive(false);
            return true;
        }
        else if (ErrorMessage.activeSelf)
        {
            ErrorMessage.SetActive(false);
            return true;
        }
        else if (nofood.activeSelf)
        {
            nofood.SetActive(false);
            return true;
        }
        else if (nohungry.activeSelf)
        {
            nohungry.SetActive(false);
            return true;
        }   
        else if (SeasonMatch.activeSelf)
        {
            SeasonMatch.SetActive(false);
            return true;
        }
        else if (GetPoop.activeSelf)
        {
            GetPoop.SetActive(false);
            return true;
        }
        else if (GetFlower.activeSelf)
        {
            GetFlower.SetActive(false);
            return true;
        }
        else if (showend.activeSelf)
        {
            showend.SetActive(false);
            return true;
        }
        else if (showMove.activeSelf)
        {
            showMove.SetActive(false);
            return true;
        }
        else if (noanimal.activeSelf)
        {
            noanimal.SetActive(false);
            return true;
        }
        else if (player.Gettingup.activeSelf)
        {
            player.Gettingup.SetActive(false);
            player.nofish.SetActive(false);
            player.Fish.SetActive(false);
            player.Shrimp.SetActive(false);
            player.Fruit.SetActive(false);
            player.EarthW.SetActive(false);
            player.Insect.SetActive(false);
            player.NT.SetActive(false);
            player.Gettingtext.text = "";
            return true;
        }
        else if(npcmanager.nobuying.activeSelf)
        {
            npcmanager.nobuying.SetActive(false);
            return true;
        }
        return false;
    }
    public void GameExit()
    {
        Application.Quit();
    }
}