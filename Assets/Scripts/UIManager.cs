using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager manager;
    Animator anim;

    public Image showep;
    public Text eptext;
    public Image showblood;
    public Text bloodtext;
    public Text playerleveltext;

    public GameObject night;
    public Image dateImage;
    public Image[] SeasonImage;
    public Text yearText;
    public Text seasonText;
    public Text dateText;

    public Text ML; // 우유
    public Text Hot; // 온도올리기
    public Text MTs; // 고기소
    public Text FR; // 과일
    public Text MTl; //고기대
    public Text FS; // 물고기
    public Text SR; // 새우
    public Text InS; // 벌레
    public Text NT; // 나무열매 
    public Text LZ; // 도마뱀
    public Text ER; // 지렁이
    public Text revival; // 부활물약
    public Text Female; // 암컷촉진
    public Text Male; // 수컷촉진
    public Text speedup; // 스피드촉진
    public Text bloodup; // 체력촉진

    // 판매소
    public Text sML; // 우유
    public Text sHot; // 온도올리기
    public Text sMTs; // 고기소
    public Text sFR; // 과일
    public Text sMTl; //고기대
    public Text sFS; // 물고기
    public Text sSR; // 새우
    public Text sInS; // 벌레
    public Text sNT; // 나무열매 
    public Text sLZ; // 도마뱀
    public Text sER; // 지렁이
    public Text srevival; // 부활물약
    public Text sFemale; // 암컷촉진
    public Text sMale; // 수컷촉진
    public Text sspeedup; // 스피드촉진
    public Text sbloodup; // 체력촉진
    public Text sECount; 
    public Text sMCount;

    public Text ECount;
    public Text MCount;

    public Text cointext;
    public Text possibleani;
    public Text Completedanimal;

    // 성장완료 후 팝업
    public Text Sex;
    public Text coinup;
    public Text epup;
    public Text animalkind;

    // 가방
    public Text Bag_ML; // 우유
    public Text Bag_Hot; // 온도올리기
    public Text Bag_MTs; // 고기소
    public Text Bag_FR; // 과일
    public Text Bag_MTl; //고기대
    public Text Bag_FS; // 물고기
    public Text Bag_SR; // 새우
    public Text Bag_InS; // 벌레
    public Text Bag_NT; // 나무열매 
    public Text Bag_LZ; // 도마뱀
    public Text Bag_ER; // 지렁이
    public Text Bag_revival; // 부활물약
    public Text Bag_speed;
    public Text Bag_blood;
    public Text Bag_female;
    public Text Bag_male;
    public Text Bag_Poop;
    public Text Bag_Flower;
    public Text Bag_ECount;
    public Text Bag_MCount;

    void LateUpdate()
    {
        Foodcheck(); // 현재 갖고 있는 음식확인
        CheckCount(); // 현재 갖고있는 동물확인
        playercheck(); // 플레이어 관련된 레벨, 경험치, 피, 총 가능 동물, 코인
        timeCheck(); // 시간과 관련된 확인
        BagCheck(); // 가방체크
    }
    void changeImage(int a) // 계절 바뀜
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == a)
                SeasonImage[i].fillAmount = 1;
            else
                SeasonImage[i].fillAmount = 0;
        }
    }
    public void completed(int s, int c, int k, int e)
    {
        switch(s)
        {
            case 0: Sex.text = "(수)";
                break;
            case 1: Sex.text = "(암)";
                break;
        }
        coinup.text = string.Format("{0:n0}", c) + "";
        epup.text = string.Format("{0:n0}", e) + "";
        switch (k)
        {
            case 0: animalkind.text = "랫서팬더"; break;
            case 1: animalkind.text = "반달가슴곰"; break;
            case 2: animalkind.text = "사자"; break;
            case 3: animalkind.text = "미어캣"; break;
            case 4: animalkind.text = "타조"; break;
            case 5: animalkind.text = "하늘다람쥐"; break;
            case 6: animalkind.text = "두루미"; break;
            case 7: animalkind.text = "독수리"; break;
            case 8: animalkind.text = "펭귄"; break;
            case 9: animalkind.text = "하프물범"; break;
            case 10: animalkind.text = "북극여우"; break;
        }
    }
    public void Foodcheck()
    {
        ML.text = string.Format("{0:00}", manager.food.ML_count);
        Hot.text = string.Format("{0:00}", manager.food.Hot_count);
        MTs.text = string.Format("{0:00}", manager.food.MTs_count);
        MTl.text = string.Format("{0:00}", manager.food.MTl_count);
        FR.text = string.Format("{0:00}", manager.food.FR_count);
        FS.text = string.Format("{0:00}", manager.food.FS_count);
        SR.text = string.Format("{0:00}", manager.food.SR_count);
        InS.text = string.Format("{0:00}", manager.food.InS_count);
        NT.text = string.Format("{0:00}", manager.food.NT_count);
        LZ.text = string.Format("{0:00}", manager.food.LZ_count);
        ER.text = string.Format("{0:00}", manager.food.ER_count);
        revival.text = string.Format("{0:00}", manager.food.revivalPortion);
        Female.text = string.Format("{0:00}", manager.food.FemalePortion);
        Male.text = string.Format("{0:00}", manager.food.MalePortion);
        bloodup.text = string.Format("{0:00}", manager.food.bloodPortion);
        speedup.text = string.Format("{0:00}", manager.food.speedPortion);

        //판매 체크
        sML.text = string.Format("{0:00}", manager.food.ML_count);
        sHot.text = string.Format("{0:00}", manager.food.Hot_count);
        sMTs.text = string.Format("{0:00}", manager.food.MTs_count);
        sMTl.text = string.Format("{0:00}", manager.food.MTl_count);
        sFR.text = string.Format("{0:00}", manager.food.FR_count);
        sFS.text = string.Format("{0:00}", manager.food.FS_count);
        sSR.text = string.Format("{0:00}", manager.food.SR_count);
        sInS.text = string.Format("{0:00}", manager.food.InS_count);
        sNT.text = string.Format("{0:00}", manager.food.NT_count);
        sLZ.text = string.Format("{0:00}", manager.food.LZ_count);
        sER.text = string.Format("{0:00}", manager.food.ER_count);
        srevival.text = string.Format("{0:00}", manager.food.revivalPortion);
        sFemale.text = string.Format("{0:00}", manager.food.FemalePortion);
        sMale.text = string.Format("{0:00}", manager.food.MalePortion);
        sbloodup.text = string.Format("{0:00}", manager.food.bloodPortion);
        sspeedup.text = string.Format("{0:00}", manager.food.speedPortion);
        sECount.text = "" + manager.animal.currentB;
        sMCount.text = "" + manager.animal.currentM;

    }
    public void CheckCount() // 소유한 알과 새끼 확인
    {
        ECount.text = "" + manager.animal.currentB;
        MCount.text = "" + manager.animal.currentM;
    }
    void timeCheck()
    {
        if (manager.time / manager.leveltime >= 0.8f)
            night.SetActive(true);
        else
            night.SetActive(false);

        dateImage.fillAmount = manager.time / manager.leveltime;
        // 계절에 따른 이미지 변경
        changeImage(manager.month);

        yearText.text = string.Format("{0:00}", manager.year) + " 년";
        seasonText.text = manager.Season[manager.month];
        dateText.text = string.Format("{0:00}", manager.date) + "";
    }
    void playercheck()
    {
        playerleveltext.text = string.Format("{0:00}", manager.playerlevel +1) + "";
        showep.fillAmount = (float)(manager.playerEp) / (float)(manager.levelEp[manager.playerlevel]);
        eptext.text = ((float)manager.playerEp /
            (float)manager.levelEp[manager.playerlevel]) * 100 + "%";
        showblood.fillAmount = (float)(manager.levelblood[manager.playerlevel]
            - manager.playerbloodminus) / (float)manager.levelblood[manager.playerlevel];
        bloodtext.text = (((float)(manager.levelblood[manager.playerlevel] - manager.playerbloodminus) / (float)manager.levelblood[manager.playerlevel]) * 100) + "%";

        possibleani.text = manager.animal.AnimalNS+"마리" +"/" + manager.animal.TotalAnimal +"마리";
        cointext.text = string.Format("{0:n0}", manager.peacecoin) + "";
        Completedanimal.text = string.Format("{0:00}", manager.animal.completedanimal) + "마리";
    }
    void BagCheck()
    {
        Bag_ML.text = string.Format("{0:00}", manager.food.ML_count);
        Bag_Hot.text = string.Format("{0:00}", manager.food.Hot_count);
        Bag_MTs.text = string.Format("{0:00}", manager.food.MTs_count);
        Bag_MTl.text = string.Format("{0:00}", manager.food.MTl_count);
        Bag_FR.text = string.Format("{0:00}", manager.food.FR_count);
        Bag_FS.text = string.Format("{0:00}", manager.food.FS_count);
        Bag_SR.text = string.Format("{0:00}", manager.food.SR_count);
        Bag_InS.text = string.Format("{0:00}", manager.food.InS_count);
        Bag_NT.text = string.Format("{0:00}", manager.food.NT_count);
        Bag_LZ.text = string.Format("{0:00}", manager.food.LZ_count);
        Bag_ER.text = string.Format("{0:00}", manager.food.ER_count);
        Bag_revival.text = string.Format("{0:00}", manager.food.revivalPortion);
        Bag_speed.text = string.Format("{0:00}", manager.food.speedPortion); ;
        Bag_blood.text = string.Format("{0:00}", manager.food.bloodPortion);
        Bag_female.text = string.Format("{0:00}", manager.food.FemalePortion);
        Bag_male.text = string.Format("{0:00}", manager.food.MalePortion);
        Bag_Poop.text = string.Format("{0:00}", manager.poopcount);
        Bag_Flower.text = string.Format("{0:00}", manager.flowercount);
        Bag_ECount.text = string.Format("{0:00}", manager.animal.currentB);
        Bag_MCount.text = string.Format("{0:00}", manager.animal.currentM);
    }
}