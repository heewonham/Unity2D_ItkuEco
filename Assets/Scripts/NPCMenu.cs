using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMenu : MonoBehaviour
{
    public GameManager manager;
    public Animal animal;
    public FoodManager food;

    public GameObject GodMenu;
    public GameObject PandaMenu;
    public GameObject DrMenu;
    public GameObject SellerMenu;
    public GameObject TreeSell;
    public GameObject Account;
    public Text Eating;
    public Text During;
    public Text AllPop; // 안내창
    public GameObject nobuying; // 살 수 없어요. 문구
    public GameObject Ranitem; // 랜덤 아이템
    public Text randomitem; // 랜덤아이템 문구

    public void GodButton(string type)
    {
        switch (type)
        {
            case "Egg":
                manager.audios[1].Play();
                if (manager.peacecoin >= 50)
                {
                    manager.peacecoin -= 50;
                    animal.currentB++;
                    AllPop.text = "알 1개를 얻었습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "Mamm":
                manager.audios[1].Play();
                if (manager.peacecoin >= 50)
                {
                    manager.peacecoin -= 50;
                    animal.currentM++;
                    AllPop.text = "포유류 1마리를 얻었습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "Exit":
                manager.audios[1].Play();
                AllPop.text = "";
                Time.timeScale = 1;
                GodMenu.SetActive(false);
                break;
        }
    }
    public void PandaButton(string type)
    {
        switch (type)
        {
            case "WW":
                manager.audios[1].Play();
                if (manager.peacecoin >= 150)
                {
                    manager.peacecoin -= 150;
                    food.FemalePortion++;
                    AllPop.text = "암컷촉진물약을 얻었습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "MM":
                manager.audios[1].Play();
                if (manager.peacecoin >= 150)
                {
                    manager.peacecoin -= 150;
                    food.MalePortion++;
                    AllPop.text = "수컷촉진물약을 얻었습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "BB":
                manager.audios[1].Play();
                if (manager.peacecoin >= 100)
                {
                    manager.peacecoin -= 100;
                    food.bloodPortion++;
                    AllPop.text = "체력회복물약을 얻었습니다..";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "SS":
                manager.audios[1].Play();
                if (manager.peacecoin >= 150)
                {
                    manager.peacecoin -= 150;
                    food.speedPortion++;
                    AllPop.text = "속도증진물약을 얻었습니다..";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "HH":
                manager.audios[1].Play();
                if (manager.peacecoin >= 150)
                {
                    manager.peacecoin -= 150;
                    food.revivalPortion++;
                    AllPop.text = "동물치료물약을 얻었습니다..";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "Exit":
                manager.audios[1].Play();
                AllPop.text = "";
                Time.timeScale = 1;
                PandaMenu.SetActive(false);
                break;
        }
    }
    public void SellerButton(string type)
    {
        switch (type)
        {
            case "hot":
                manager.audios[1].Play();
                if (manager.peacecoin >= 8)
                {
                    manager.peacecoin -= 8;
                    food.Hot_count++;
                    AllPop.text = "온도데우기 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "milk":
                manager.audios[1].Play();
                if (manager.peacecoin >= 8)
                {
                    manager.peacecoin -= 8;
                    food.ML_count++;
                    AllPop.text = "우유 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "fr":
                manager.audios[1].Play();
                if (manager.peacecoin >= 15)
                {
                    manager.peacecoin -= 15;
                    food.FR_count++;
                    AllPop.text = "과일 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "meatL":
                manager.audios[1].Play();
                if (manager.peacecoin >= 20)
                {
                    manager.peacecoin -= 20;
                    food.MTl_count++;
                    AllPop.text = "고기(대) 1개를 구매하였습니다..";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "meatS":
                manager.audios[1].Play();
                if (manager.peacecoin >= 20)
                {
                    manager.peacecoin -= 20;
                    food.MTs_count++;
                    AllPop.text = "고기(소) 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "fish":
                manager.audios[1].Play();
                if (manager.peacecoin >= 15)
                {
                    manager.peacecoin -= 15;
                    food.FS_count++;
                    AllPop.text = "물고기 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "ins":
                manager.audios[1].Play();
                if (manager.peacecoin >= 10)
                {
                    manager.peacecoin -= 10;
                    food.InS_count++;
                    AllPop.text = "벌레 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "nt":
                manager.audios[1].Play();
                if (manager.peacecoin >= 10)
                {
                    manager.peacecoin -= 10;
                    food.NT_count++;
                    AllPop.text = "나무열매 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "sr":
                manager.audios[1].Play();
                if (manager.peacecoin >= 15)
                {
                    manager.peacecoin -= 15;
                    food.SR_count++;
                    AllPop.text = "새우 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "lz":
                manager.audios[1].Play();
                if (manager.peacecoin >= 15)
                {
                    manager.peacecoin -= 15;
                    food.LZ_count++;
                    AllPop.text = "도마뱀 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "ew":
                manager.audios[1].Play();
                if (manager.peacecoin >= 10)
                {
                    manager.peacecoin -= 10;
                    food.ER_count++;
                    AllPop.text = "지렁이 1개를 구매하였습니다.";
                }
                else
                    nobuying.SetActive(true);
                break;
            case "Exit":
                manager.audios[1].Play();
                AllPop.text = "";
                Time.timeScale = 1;
                SellerMenu.SetActive(false);
                break;
        }
    }
    public void DoctorButton(string type)
    {
        switch (type)
        {
            case "bear":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 우유 > 과일 > 고기(소)";
                During.text = "봄 계절 동물, 봄 성장시기 9일 소요\n(여름에 키워도 성장속도 동일!)" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 350 피스코인 획득";
                break;
            case "panda":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 우유 > 나무열매 > 과일";
                During.text = "봄 계절 동물, 봄 성장시기 7일 소요\n(여름에 키워도 성장속도 동일!)" +
                    "(그러나 다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 250 피스코인 획득";
                break;
            case "lion":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 우유 > 고기(소) > 고기(대)";
                During.text = "봄 계절 동물, 봄 성장시기 10일 소요\n(여름에 키워도 성장속도 동일!)" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 500 피스코인 획득";
                break;
            case "meerkat":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 우유 > 지렁이 > 도마뱀";
                During.text = "여름 계절 동물, 여름 성장시기 7일 소요\n(봄에 키워도 성장속도 동일!)" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 250 피스코인 획득";
                break;
            case "ostrich":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 알데우기 > 지렁이 > 도마뱀";
                During.text = "여름 계절 동물, 여름 성장시기 7일 소요\n(봄에 키워도 성장속도 동일!)" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 350 피스코인 획득";
                break;
            case "squrr":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 우유 > 벌레 > 나무열매";
                During.text = "가을 계절 동물, 가을 성장시기 6일 소요\n" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 250 피스코인 획득";
                break;
            case "crane":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 알데우기 > 벌레 > 물고기";
                During.text = "가을 계절 동물, 가을 성장시기 7일 소요\n" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 250 피스코인 획득";
                break;
            case "Eagle":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 알데우기 > 고기(소) > 고기(대)";
                During.text = "가을 계절 동물, 가을 성장시기 10일 소요\n" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 500 피스코인 획득";
                break;
            case "penguin":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 알데우기 > 새우 > 물고기";
                During.text = "겨울 계절 동물, 겨울 성장시기 8일 소요\n" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 350 피스코인 획득";
                break;
            case "seal":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 우유 > 새우 > 물고기";
                During.text = "겨울 계절 동물, 겨울 성장시기 8일 소요\n" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 350 피스코인 획득";
                break;
            case "fox":
                manager.audios[1].Play();
                Account.SetActive(true);
                Eating.text = "먹이 : 우유 > 물고기 > 고기(소)";
                During.text = "겨울 계절 동물, 겨울 성장시기 9일 소요\n" +
                    "(다른 계절에 키울 경우 성장속도가 느리다.)\n\n성장완료시 350 피스코인 획득";
                break;
            case "Exit1":
                manager.audios[1].Play();
                Time.timeScale = 1;
                DrMenu.SetActive(false);
                Account.SetActive(false);
                break;
            case "Exit2":
                manager.audios[1].Play();
                Account.SetActive(false);
                break;
        }
    }
    public void TreeButton(string type)
    {
        switch (type)
        {
            case "Ins":
                manager.audios[1].Play();
                if (manager.poopcount >= 3)
                {
                    int a = Random.Range(0, 4);
                    manager.poopcount -= 3;
                    switch (a)
                    {
                        case 0:
                            food.InS_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "벌레\n획득 >3<!!";
                            break;
                        case 1:
                            food.NT_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "나무열매\n획득 >3<!!";
                            break;
                        case 2:
                            food.ER_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "지렁이\n획득 >3<!!";
                            break;
                        case 3:
                            food.FR_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "과일\n획득 >3<!!";
                            break;
                    }
                    manager.audios[6].Play();
                }
                else
                    nobuying.SetActive(true);
                break;
            case "Nt":
                manager.audios[1].Play();
                if (manager.poopcount >= 7)
                {
                    int a = Random.Range(0, 4);
                    manager.poopcount -= 7;
                    switch (a)
                    {
                        case 0:
                            food.FS_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "물고기\n획득 >3<!!";
                            break;
                        case 1:
                            food.SR_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "새우\n획득 >3<!!";
                            break;
                        case 2:
                            food.LZ_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "도마뱀\n획득 >3<!!";
                            break;
                        case 3:
                            food.MTs_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "고기(소)\n획득 >3<!!";
                            break;
                    }
                    manager.audios[6].Play();
                }
                else
                    nobuying.SetActive(true);
                break;
            case "Fr":
                manager.audios[1].Play();
                if (manager.poopcount >= 15)
                {
                    int a = Random.Range(0, 10);
                    manager.poopcount -= 15;
                    switch (a)
                    {
                        case 0:
                            food.bloodPortion++;
                            Ranitem.SetActive(true);
                            randomitem.text = "체력포션\n획득 >3<!!";
                            break;
                        case 1:
                            food.speedPortion++;
                            Ranitem.SetActive(true);
                            randomitem.text = "속도포션\n획득 >3<!!";
                            break;
                        case 2:
                            food.revivalPortion++;
                            Ranitem.SetActive(true);
                            randomitem.text = "치료포션\n획득 >3<!!";
                            break;
                        case 3:
                            food.MalePortion++;
                            Ranitem.SetActive(true);
                            randomitem.text = "수컷포션\n획득 >3<!!";
                            break;
                        case 4:
                            food.FemalePortion++;
                            Ranitem.SetActive(true);
                            randomitem.text = "암컷포션\n획득 >3<!!";
                            break;
                        default:
                            food.MTl_count++;
                            Ranitem.SetActive(true);
                            randomitem.text = "고기(대)\n획득 >3<!!";
                            break;
                    }
                    manager.audios[6].Play();
                }
                else
                    nobuying.SetActive(true);
                break;
            case "exit":
                manager.audios[1].Play();
                Time.timeScale = 1;
                Ranitem.SetActive(false);
                TreeSell.SetActive(false);
                break;
        }
    }
}
