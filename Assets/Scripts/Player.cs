using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public Animal animal;
    public GameManager manager;
    public FoodManager Food;
    public UIManager uimanager;
    public ObjectManager objectmanager;
    public TalkManager talkManager;
    IDLevelEP id_level;
    AnimalMove animalMove;

    //Moving
    public JoystickValue value;
    public float Speed;
    float h;
    float v;
    float hValue;
    float vValue;
    bool isHorizonMove;
    bool vDown;
    bool hDown;
    Vector3 dirVec;
    Vector2 here;

    string str;
    float weigh; // 계절별 가중치
    int npcNum; // npc id 번호
    public int tmp; // 버튼 선택 후 아이디 임시저장
    public ParticleSystem BloodPlus; // 꽃밭에 있으면 체력상승
    public ParticleSystem UpEffect; // 레업

    public GameObject AnimalObj; // 동물
    public GameObject houseObj; // 홈메뉴
    public GameObject DoorObj; // 문
    public GameObject scanObj; // Npc 혹은 기타 물건

    GameObject GetItem;
    public GameObject border;
    GameObject animalSet;
    public GameObject TalkingChooBox;
    public Text Npcsell;

    // 물고기 잡이
    public GameObject Gettingup;
    public GameObject Fish; //물고기 잡음
    public GameObject Shrimp; // 새우 잡음
    public GameObject Fruit; // 과일 얻음
    public GameObject NT;// 나무열매얻음
    public GameObject EarthW; // 지렁이 얻음
    public GameObject Insect; // 벌레 얻음
    public GameObject nofish; // 아무것도 못잡음
    public Text Gettingtext;

    // space 두번 눌림 방지
    bool flag = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayMove();
        playScanOpen();
        Healing();
    }
    void FixedUpdate()
    {
        // Move
        Vector2 moveVec = isHorizonMove ? new Vector2(hValue, 0) : new Vector2(0, vValue);
        rigid.velocity = moveVec * Speed;
        RayCaseResult();
    }

    // 캐릭터 움직임
    void PlayMove() 
    {
        bool vUp = false, hUp = false;

        // Move Value
        // 모바일 환경에서
        /*
        if (!manager.isAction)
        {
            keyReset();
            key(value.joyTouch);
        } else keyReset();
        
        hValue = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + value.joyTouch.x;
        vValue = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + value.joyTouch.y;
        */

        // PC환경에서
        h = hValue = Input.GetAxisRaw("Horizontal");
        v = vValue =Input.GetAxisRaw("Vertical");

        // Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        hUp = Input.GetButtonUp("Horizontal");
        vUp = Input.GetButtonUp("Vertical");
        
        // Check Horizontal Move
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp) isHorizonMove = h != 0;

        //Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("Ischange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("Ischange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("Ischange", false);

        // Direction
        if (vDown && v > 0)
            dirVec = Vector3.up;
        else if (vDown && v < 0)
            dirVec = Vector3.down;
        else if (hDown && h < 0)
            dirVec = Vector3.left;
        else if (hDown && h > 0)
            dirVec = Vector3.right;

    }

    // 화면의 아이콘을 누를 경우
    public void ButtonDown(string type)
    {
        switch (type)
        {
            // Enter 이미지를 누를 경우
            case "E":
                manager.audios[1].Play();
                bool chk = manager.popClose();
                if (chk) return;
                if (AnimalObj != null)
                {
                    if (!id_level.Isbaby)
                    {
                        animalMove = AnimalObj.GetComponent<AnimalMove>();
                        animalMove.stopping();
                        animalMove.isAct = false;
                    }
                    manager.MenuOpen();
                }
                else if (houseObj != null)
                    manager.HomeMenuOpen();
                else if (DoorObj != null)
                    manager.DoorAct(DoorObj.name);
                else if (GetItem != null)
                    deleteItem(GetItem);
                else if (scanObj != null)
                {
                    ObjData data = scanObj.GetComponent<ObjData>();
                     if (data.isNpc && !manager.IsTalking)
                    {
                        npcNum = data.id;
                        sellchk(npcNum);
                        TalkingChooBox.SetActive(true);
                    }
                    else
                        manager.TalkAction(scanObj);
                }

                RaycastHit2D water = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("fishing"));
                RaycastHit2D earth = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("Digging"));
                RaycastHit2D tree = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("treeTouch"));
                if (water.collider != null)
                {
                    GetManager GeT = water.collider.gameObject.GetComponent<GetManager>();
                    if (!GeT.chk)
                    {
                        Fishing();
                        GeT.chk = true;
                    }
                }
                else if (tree.collider != null)
                {
                    GetManager GeT = tree.collider.gameObject.GetComponent<GetManager>();
                    if (!GeT.chk)
                    {
                        TreeTouch();
                        GeT.chk = true;
                    }
                }
                else if (earth.collider != null)
                {
                    GetManager Get = earth.collider.gameObject.GetComponent<GetManager>();
                    if (!Get.chk)
                    {
                        Digging();
                        Get.chk = true;
                    }
                }
                break;
        }
    }
    public void TalkingChooseButton(string type)
    {
        switch (type)
        {
            case "Sell":
                manager.audios[1].Play();
                TalkingChooBox.SetActive(false);
                talkManager.NpcChk(npcNum);
                break;
            case "Talk":
                manager.audios[1].Play();
                TalkingChooBox.SetActive(false);
                manager.TalkAction(scanObj);
                break;
            case "exit":
                manager.audios[1].Play();
                TalkingChooBox.SetActive(false);
                break;
        }
    }
    void playScanOpen()
    {
        if (Input.GetButtonDown("Jump"))
        {
            bool chk = manager.popClose();
            if (chk) return;
            if (AnimalObj != null)
            {
                manager.audios[1].Play();
                if (!id_level.Isbaby)
                {
                    animalMove = AnimalObj.GetComponent<AnimalMove>();
                    animalMove.stopping();
                    animalMove.isAct = false;
                }
                manager.MenuOpen();
            }
            else if (houseObj != null)
            {
                manager.audios[1].Play();
                manager.HomeMenuOpen();
            }
            else if (DoorObj != null)
            {
                manager.audios[1].Play();
                manager.DoorAct(DoorObj.name);
            }
            else if (GetItem != null)
            {
                manager.audios[1].Play();
                deleteItem(GetItem);
            }
            else if (scanObj != null)
            {
                manager.audios[1].Play();
                ObjData data = scanObj.GetComponent<ObjData>();
                if (data.isNpc && !manager.IsTalking)
                {
                    npcNum = data.id;
                    sellchk(npcNum);
                    TalkingChooBox.SetActive(true);
                }
                else
                    manager.TalkAction(scanObj);
            }
        }

        RaycastHit2D water = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("fishing"));
        RaycastHit2D earth = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("Digging"));
        RaycastHit2D tree = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("treeTouch"));
        if (water.collider != null && Input.GetButtonDown("Jump"))
        {
            GetManager GeT = water.collider.gameObject.GetComponent<GetManager>();
            if (!GeT.chk)
            {
                Fishing();
                GeT.chk = true;
            }
        }
        else if (tree.collider != null && Input.GetButtonDown("Jump"))
        {
            GetManager GeT = tree.collider.gameObject.GetComponent<GetManager>();
            if (!GeT.chk)
            {
                TreeTouch();
                GeT.chk = true;
            }
        }
        else if (earth.collider != null && Input.GetButtonDown("Jump"))
        {
            GetManager Get = earth.collider.gameObject.GetComponent<GetManager>();
            if (!Get.chk)
            {
                Digging();
                Get.chk = true;
            }
        }
    }
    void key(Vector2 value)
    {
        if ((value.x * value.x) >= (value.y * value.y))
        {
            if (value.x < 0)
            {
                h = -1;
                hDown = true;
            }
            else if (value.x > 0)
            {
                h = 1;
                hDown = true;
            }
        }
        else
        {
            if (value.y < 0)
            {
                v = -1;
                vDown = true;
            }
            else if (value.y > 0)
            {
                v = 1;
                vDown = true;
            }
        }
    }
    public void keyReset()
    {
        vDown = false;
        hDown = false;
        v = 0;
        h = 0;
    }
    void RayCaseResult()
    {
        Debug.DrawRay(rigid.position, dirVec * 1f, new Color(0, 1, 0));
        // Ray - 둥지
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("Nest"));
        if (rayHit.collider != null)
        {
            AnimalObj = rayHit.collider.gameObject;
            id_level = AnimalObj.GetComponent<IDLevelEP>();
        }
        else
            AnimalObj = null;
        // Ray - Door 
        RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("Door"));
        if (rayHit1.collider != null)
            DoorObj = rayHit1.collider.gameObject;
        else
            DoorObj = null;
        // Ray - house
        RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("House"));
        if (rayHit2.collider != null)
            houseObj = rayHit2.collider.gameObject;
        else
            houseObj = null;
        // Ray = GetItem
        RaycastHit2D rayHit3 = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("GetItem"));
        if (rayHit3.collider != null)
            GetItem = rayHit3.collider.gameObject;
        else
            GetItem = null;
        // Ray = Object(npc, 물건 등)
        RaycastHit2D rayHit4 = Physics2D.Raycast(rigid.position, dirVec, 1, LayerMask.GetMask("Object"));
        if (rayHit4.collider != null)
            scanObj = rayHit4.collider.gameObject;
        else
            scanObj = null;
    }
    void Fishing()
    {
        int lucky = Random.Range(0, 3);
        Gettingup.SetActive(true);
        if (lucky == 0)
        {
            manager.audios[3].Play();
            nofish.SetActive(true);
            manager.PlayerAtt(5, 5);
            Gettingtext.text = "낚시 실패...ㅠㅠ\n경험치5++";
        }
        else if (lucky == 1)
        {
            manager.audios[2].Play();
            Fish.SetActive(true);
            Gettingtext.text = "(물고기) 낚시에\n성공하였습니다.\n경험치 10++";
            manager.PlayerAtt(10, 5);
            Food.FS_count++;
        }
        else
        {
            manager.audios[2].Play();
            Shrimp.SetActive(true);
            Gettingtext.text = "(새우) 낚시에\n성공하였습니다.\n경험치 10++";
            manager.PlayerAtt(10, 5);
            Food.SR_count++;
        }
    }
    void TreeTouch()
    {
        int lucky = Random.Range(0, 4);
        Gettingup.SetActive(true);
        if (lucky == 0)
        {
            manager.audios[3].Play();
            nofish.SetActive(true);
            manager.PlayerAtt(5, 5);
            Gettingtext.text = "나무흔들기\n실패...ㅠㅠ\n경험치5++";
        }
        else if (lucky == 1)
        {
            manager.audios[2].Play();
            Insect.SetActive(true);
            Gettingtext.text = "벌레가\n떨어졌습니다.\n경험치 10++";
            manager.PlayerAtt(10, 5);
            Food.InS_count++;
        }
        else if(lucky == 2)
        {
            manager.audios[2].Play();
            Fruit.SetActive(true);
            Gettingtext.text = "과일이\n떨어졌습니다.\n경험치 10++";
            manager.PlayerAtt(10, 5);
            Food.FR_count++;
        }
        else
        {
            manager.audios[2].Play();
            NT.SetActive(true);
            Gettingtext.text = "나무열매가\n떨어졌습니다.\n경험치 10++";
            manager.PlayerAtt(10, 5);
            Food.NT_count++;
        }
    }
    void Digging()
    {
        int lucky = Random.Range(0, 3);
        Gettingup.SetActive(true);
        if (lucky == 0)
        {
            manager.audios[3].Play();
            nofish.SetActive(true);
            manager.PlayerAtt(5, 5);
            Gettingtext.text = "땅파기\n실패...ㅠㅠ\n경험치5++";
        }
        else
        {
            manager.audios[2].Play();
            EarthW.SetActive(true);
            Gettingtext.text = "지렁이를 주웠다.\n경험치 10++";
            manager.PlayerAtt(10, 5);
            Food.ER_count++;
        }
    }
    void sellchk(int num)
    {
        switch (num)
        {
            case 500: // 나무
                Npcsell.text = "똥 교환하기";
                break;
            case 1000: //다판다
                Npcsell.text = "포션 얻기";
                break;
            case 1500: //겉핥기
                Npcsell.text = "동물 정보얻기";
                break;
            case 2000: // 산신령
                Npcsell.text = "생명 얻기";
                break;
            case 2500: //샐러다
                Npcsell.text = "먹이 얻기";
                break;
        }
    }
    void Healing()
    {
        if (manager.month == 2 || manager.month == 3)
            return;
        float xx = rigid.transform.position.x;
        float yy = rigid.transform.position.y;
        if(( xx >= 40 && xx <= 51) && (yy >= 13 && yy <= 23))
        {
            if(!BloodPlus.isPlaying)
                BloodPlus.Play();
            if (manager.playerbloodminus <= 0)
            {
                manager.playerbloodminus = 0;
                BloodPlus.Stop();
            }
            else
                manager.playerbloodminus -= 0.1f;
        }
        else
            BloodPlus.Stop();
    }
    // start메뉴 버튼
    public void Button(string type)
    {
        switch (type)
        {
            case "E":
                manager.audios[1].Play();
                if (animal.AnimalNS < animal.TotalAnimal)
                {
                    tmp = 1;
                    SubMenuActive();
                    manager.Close();
                }
                else
                    manager.noanimal.SetActive(true);
                break;
            case "M":
                manager.audios[1].Play();
                if (animal.AnimalNS < animal.TotalAnimal)
                {
                    tmp = 3;
                    SubMenuActive();
                    manager.Close();
                }
                else
                    manager.noanimal.SetActive(true);
                break;
        }
    }
    // 초기 동물설정
    public void SubMenuActive()
    {
        if (tmp == 1 && !id_level.Is)
        {
            if (animal.currentB <= 0)
                manager.nofood.SetActive(true);
            else
            {
                animal.currentB--;
                animal.BringUpB++;
                manager.PlayerAtt(5, 5);
                manager.idlevel_informbool[id_level.Num] = true;
                manager.idlevel_inform[id_level.Num] = id_level.GetComponent<IDLevelEP>();
                id_level.Animal_Id = tmp;
                id_level.Is = true;
                id_level.makeani = true;
            }
        }
        else if (tmp == 3 && !id_level.Is)
        {
            if (animal.currentM <= 0)
                manager.nofood.SetActive(true);
            else
            {
                animal.currentM--;
                animal.BringUpM++;
                manager.PlayerAtt(5, 5);
                manager.idlevel_informbool[id_level.Num] = true;
                manager.idlevel_inform[id_level.Num] = id_level.GetComponent<IDLevelEP>();
                id_level.Animal_Id = tmp;
                id_level.Is = true;
                id_level.makeani = true;
            }
        }
    }
    // 먹이메뉴 버튼
    public void Button2(string type)
    {
        switch (type)
        {
            case "ML":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.ML_count > 0)
                        Submid(type);
                    else
                        manager.nofood.SetActive(true);;
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "Hot":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.Hot_count > 0)
                        Submid(type);
                    else
                        manager.nofood.SetActive(true);;
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "MTs":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.MTs_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);;
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "MTl":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.MTl_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "FR":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                     if (Food.FR_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "FS":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.FS_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "SR":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.SR_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "InS":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.InS_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "NT":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.NT_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "LZ":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.LZ_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "ER":
                manager.audios[1].Play();
                if (id_level.hungry)
                {
                    if (Food.ER_count > 0)
                        Upgrade(type);
                    else
                        manager.nofood.SetActive(true);
                }
                else
                    manager.nohungry.SetActive(true);
                break;
            case "FemaleP":
                manager.audios[1].Play();
                if (Food.FemalePortion > 0)
                {
                    id_level.animalSex = 1;
                    Food.FemalePortion--;
                    manager.audios[5].Play();
                }
                else
                    manager.nofood.SetActive(true);;
                break;
            case "MaleP":
                manager.audios[1].Play();
                if (Food.MalePortion > 0)
                {
                    id_level.animalSex = 0;
                    Food.MalePortion--;
                    manager.audios[5].Play();
                }
                else
                    manager.nofood.SetActive(true);;
                break;
        }
    }
    // 초창기 동물 레벨업. 경험치
    public void Submid(string food)
    {
        if ((id_level.Animal_Id == 1 && food == "Hot") && id_level.Animal_EP <= 2)
        {
            Setting();
            Food.Hot_count--;
        }     
        else if ((id_level.Animal_Id == 3 && food == "ML") && id_level.Animal_EP <= 2)
        {
            Setting();
            Food.ML_count--;
        }
        else
            manager.ErrorMessage.SetActive(true);;
    }
    void Setting()
    {
        anim.SetTrigger("Jump");
        id_level.Animal_EP++;
        id_level.effect.Play();
        manager.audios[5].Play();
        id_level.hungry = false;
        manager.PlayerAtt(5, 5);
        if (id_level.Animal_EP == 2)
        {
            id_level.Animal_Id++;
            id_level.makeani = true;
        }
        manager.Close();
    }
    void Upgrade(string type)
    {
        if (id_level.Animal_EP >= 2 && id_level.Animal_EP < 10)
            Mid(type);
        else if (id_level.Animal_EP >= 10)
            Finish(type);
        else
            manager.ErrorMessage.SetActive(true);;
    }
    public void Mid(string type)
    {
        switch (type)
        {
            case "MTs":
                if (id_level.Animal_EP == 2) // 1차 변화
                {
                    if (id_level.Animal_Id == 2) // 독수리
                        tmp = 7;
                    else if (id_level.Animal_Id == 4) // 사자
                        tmp = 2;
                    
                    weigh = manager.Seasonmatch(tmp);
                    born(0, tmp, (int)(Food.MTs_EP*weigh));
                    manager.PlayerAtt((int)(11*weigh), (int)(8*weigh));
                    Food.MTs_count--;
                    manager.Close();
                }
                else if (id_level.ArrayID == 2 || id_level.ArrayID == 7)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    id_level.Animal_EP += (int)(Food.MTs_EP*weigh);
                    id_level.effect.Play();
                    manager.audios[5].Play();
                    id_level.hungry = false;
                    manager.PlayerAtt((int)(11 * weigh), (int)(8 * weigh));
                    Food.MTs_count--;
                    manager.Close();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "FR":
                if (id_level.Animal_EP == 2) // 변화
                {
                    if (id_level.Animal_Id == 2) // 없음
                    { 
                        manager.ErrorMessage.SetActive(true);;
                        return;
                    }
                    else if (id_level.Animal_Id == 4) // 반달가슴곰
                        tmp = 1;

                    weigh = manager.Seasonmatch(tmp);
                    born(0, tmp, (int)(Food.FR_EP * weigh));
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    Food.FR_count--;
                    manager.Close();
                }
                else if (id_level.ArrayID == 1)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    id_level.Animal_EP += (int)(Food.FR_EP*weigh);
                    id_level.effect.Play();
                    manager.audios[5].Play();
                    id_level.hungry = false;               
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    Food.FR_count--;
                    manager.Close();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "FS":
                if (id_level.Animal_EP == 2) // 1차 변화
                {
                    if (id_level.Animal_Id == 2) // 없음
                    {
                        manager.ErrorMessage.SetActive(true);;
                        return;
                    } 
                    else if (id_level.Animal_Id == 4) // 북극여우
                        tmp = 10;

                    weigh = manager.Seasonmatch(tmp);
                    born(0, tmp, (int)(Food.FS_EP * weigh));
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    Food.FS_count--;
                    manager.Close();
                }
                else if (id_level.ArrayID == 10)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    id_level.Animal_EP += (int)(Food.FS_EP*weigh);
                    id_level.effect.Play();
                    manager.audios[5].Play();
                    id_level.hungry = false;            
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    Food.FS_count--;
                    manager.Close();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "SR":
                if (id_level.Animal_EP == 2) // 변화
                {
                    if (id_level.Animal_Id == 2) // 펭귄
                        tmp = 8;
                    else if (id_level.Animal_Id == 4) // 하프물범
                        tmp = 9;

                    weigh = manager.Seasonmatch(tmp);
                    born(0, tmp, (int)(Food.SR_EP * weigh));
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    Food.SR_count--;
                    manager.Close();
                }
                else if (id_level.ArrayID == 8 || id_level.ArrayID == 9)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    id_level.Animal_EP += (int)(Food.SR_EP*weigh);
                    id_level.effect.Play();
                    manager.audios[5].Play();
                    id_level.hungry = false;
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    Food.SR_count--;
                    manager.Close();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "InS":
                if (id_level.Animal_EP == 2) // 변화
                {
                    if (id_level.Animal_Id == 2) // 두루미
                        tmp = 6;
                    else if (id_level.Animal_Id == 4) // 하늘다람쥐
                        tmp = 5;

                    weigh = manager.Seasonmatch(tmp);
                    born(0, tmp, (int)(Food.InS_EP * weigh));
                    manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                    Food.InS_count--;
                    manager.Close();
                }
                else if (id_level.ArrayID == 6 || id_level.ArrayID == 5)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    id_level.Animal_EP += (int)(Food.InS_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                    Food.InS_count--;
                    manager.Close();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "NT":
                if (id_level.Animal_EP == 2) // 변화
                {
                    if (id_level.Animal_Id == 2) // 없음
                    {
                        manager.ErrorMessage.SetActive(true);;
                        return;
                    }
                    else if (id_level.Animal_Id == 4) // 랫서팬더
                        tmp = 0;

                    weigh = manager.Seasonmatch(tmp);
                    born(0, tmp, (int)(Food.NT_EP * weigh));
                    manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                    Food.NT_count--;
                    manager.Close();
                }
                else if (id_level.ArrayID == 0)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    id_level.Animal_EP += (int)(Food.NT_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                    Food.NT_count--;
                    manager.Close();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "ER":
                if (id_level.Animal_EP == 2) // 변화
                {
                    if (id_level.Animal_Id == 2) // 타조
                        tmp = 4;
                    else if (id_level.Animal_Id == 4) // 미어캣
                        tmp = 3;

                    weigh = manager.Seasonmatch(tmp);
                    born(0, tmp, (int)(Food.ER_EP * weigh));
                    manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                    Food.ER_count--;
                    manager.Close();
                }
                else if (id_level.ArrayID == 4 || id_level.ArrayID == 3)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    id_level.Animal_EP += (int)(Food.ER_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                    Food.ER_count--;
                    manager.Close();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            default:
                manager.ErrorMessage.SetActive(true);;
                break;
        }
    }
    public void Finish(string type)
    {
        switch (type)
        {
            case "MTl":
                if (id_level.Animal_EP == 10 || id_level.Animal_EP == 11) // 2차 변화
                { // ArrayID에 따른 변화 
                    if (id_level.ArrayID == 2 || id_level.ArrayID == 7)
                    {
                        weigh = manager.Seasonmatch(id_level.ArrayID);
                        born(1, id_level.ArrayID, (int)(Food.MTl_EP*weigh));
                        manager.PlayerAtt((int)(11 * weigh), (int)(8 * weigh));
                        Food.MTl_count--;
                        manager.Close();
                    }
                    else
                        manager.ErrorMessage.SetActive(true);;
                }
                else if (id_level.ArrayID == 2 || id_level.ArrayID == 7)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    manager.PlayerAtt((int)(11 * weigh), (int)(8 * weigh));
                    id_level.Animal_EP += (int)(Food.MTl_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    Food.MTl_count--;
                    manager.Close();
                    Growing();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "FR" :
                if (id_level.Animal_EP == 10 || id_level.Animal_EP == 11) // 2차 변화
                { // ArrayID에 따른 변화 
                    if (id_level.ArrayID == 0)
                    {
                        weigh = manager.Seasonmatch(id_level.ArrayID);
                        born(1, id_level.ArrayID, (int)(Food.FR_EP * weigh));
                        manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                        Food.FR_count--;
                        manager.Close();
                    }
                    else
                        manager.ErrorMessage.SetActive(true);;
                }
                else if(id_level.ArrayID == 0)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    id_level.Animal_EP += (int)(Food.FR_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    Food.FR_count--;
                    manager.Close();
                    Growing();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "FS":
                if (id_level.Animal_EP == 11 || id_level.Animal_EP == 10) // 2차 변화
                { // ArrayID에 따른 변화 
                    if (id_level.ArrayID == 9 || id_level.ArrayID == 6 || id_level.ArrayID == 8)
                    {
                        weigh = manager.Seasonmatch(id_level.ArrayID);
                        born(1, id_level.ArrayID, (int)(Food.FS_EP * weigh));
                        manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                        Food.FS_count--;
                        manager.Close();
                    }
                    else
                        manager.ErrorMessage.SetActive(true);;
                }
                else if (id_level.ArrayID == 9 || id_level.ArrayID == 6 || id_level.ArrayID == 8)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    id_level.Animal_EP += (int)(Food.FS_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    Food.FS_count--;
                    manager.Close();
                    Growing();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "NT":
                if (id_level.Animal_EP == 10 || id_level.Animal_EP == 11)  // 2차 변화
                { // ArrayID에 따른 변화 
                    if (id_level.ArrayID == 5)
                    {
                        weigh = manager.Seasonmatch(id_level.ArrayID);
                        born(1, id_level.ArrayID, (int)(Food.NT_EP * weigh));
                        manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                        Food.NT_count--;
                        manager.Close();
                    }
                    else
                        manager.ErrorMessage.SetActive(true);;
                }
                else if (id_level.ArrayID ==5)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    manager.PlayerAtt((int)(7 * weigh), (int)(8 * weigh));
                    id_level.Animal_EP += (int)(Food.NT_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    Food.NT_count--;
                    manager.Close();
                    Growing();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "MTs":
                if (id_level.Animal_EP == 10 || id_level.Animal_EP == 11)  // 2차 변화
                { // ArrayID에 따른 변화 
                    if (id_level.ArrayID == 1 || id_level.ArrayID == 10)
                    {
                        weigh = manager.Seasonmatch(id_level.ArrayID);
                        born(1, id_level.ArrayID, (int)(Food.MTs_EP * weigh));
                        manager.PlayerAtt((int)(11 * weigh), (int)(8 * weigh));
                        Food.MTs_count--;
                        manager.Close();
                    }
                    else
                        manager.ErrorMessage.SetActive(true);;
                }
                else if (id_level.ArrayID == 1 || id_level.ArrayID == 10)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    manager.PlayerAtt((int)(11 * weigh), (int)(8 * weigh));
                    id_level.Animal_EP += (int)(Food.MTs_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    Food.MTs_count--;
                    manager.Close();
                    Growing();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            case "LZ":
                if (id_level.Animal_EP == 10 || id_level.Animal_EP == 11)  // 2차 변화
                { // ArrayID에 따른 변화 
                    if (id_level.ArrayID == 3 || id_level.ArrayID == 4)
                    {
                        weigh = manager.Seasonmatch(id_level.ArrayID);
                        born(1, id_level.ArrayID, (int)(Food.LZ_EP * weigh));
                        manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                        Food.LZ_count--;
                        manager.Close();
                    }
                    else
                        manager.ErrorMessage.SetActive(true);;
                }
                else if (id_level.ArrayID == 3 || id_level.ArrayID == 4)
                {
                    anim.SetTrigger("Jump");
                    weigh = manager.Seasonmatch(id_level.ArrayID);
                    manager.PlayerAtt((int)(9 * weigh), (int)(8 * weigh));
                    id_level.Animal_EP += (int)(Food.LZ_EP*weigh);
                    id_level.effect.Play(); manager.audios[5].Play();
                    id_level.hungry = false;
                    Food.LZ_count--;
                    manager.Close();
                    Growing();
                }
                else
                    manager.ErrorMessage.SetActive(true);;
                break;
            default:
                manager.ErrorMessage.SetActive(true);;
                break;
        }
    }
    void Growing()
    {
        if (id_level.Animal_EP >= 18)
        {
            manager.Grow(id_level.ArrayID,id_level.animalSex);
            manager.idlevel_informbool[id_level.Num] = false;
            id_level.Restart();
        }
    }
    void born(int ck ,int num, int foodep)
    {
        manager.audios[2].Play();
        here = new Vector2(AnimalObj.transform.position.x, AnimalObj.transform.position.y-1f);
        int dama = id_level.Animal_Damage;
        int tempsex = id_level.animalSex;
        int tempnum = id_level.Num;
        if (ck == 0)
        {
            id_level.reset();
            str = "B" + num.ToString();
        }
        else
        {
            id_level.Restart();
            str = "A" + num.ToString();
        }
        animalSet = objectmanager.MakeObj(str);
        animalSet.transform.position = here;
        anim.SetTrigger("Jump");
        manager.idlevel_inform[tempnum] = animalSet.GetComponent<IDLevelEP>();
        manager.idlevel_inform[tempnum].Num = tempnum;
        manager.idlevel_inform[tempnum].Animal_Damage = dama;
        manager.idlevel_inform[tempnum].animalSex = tempsex;
        manager.idlevel_inform[tempnum].Animal_EP += foodep;
        manager.idlevel_inform[tempnum].Is = true;
        manager.idlevel_inform[tempnum].effect.Play();
        manager.audios[5].Play();
    }
    void deleteItem(GameObject name) // 똥 삭제
    {
        if (name.name == "Poop(Clone)")
        {
            manager.GetPoop.SetActive(true);
            manager.playerEp += 10;
            manager.playerbloodminus += 8; 
            GetItem.SetActive(false);
            manager.poopcount++;
        }
        else if(name.name == "FlowerL(Clone)")
        {
            manager.GetFlower.SetActive(true);
            manager.playerEp += 10;
            manager.playerbloodminus += 8;
            GetItem.SetActive(false);
            manager.flowercount++;
        }
    }
    public void levelup() // 캐릭터 렙업
    {
        if (manager.playerEp >= manager.levelEp[manager.playerlevel])
        { // 레벨업시 --> 애니메이션 추가
            manager.audios[2].Play();
            manager.playerEp = manager.playerEp - manager.levelEp[manager.playerlevel];
            manager.playerlevel++;
            manager.playerbloodminus = 0;
            UpEffect.Play();
            anim.SetTrigger("Jump");
        }
    }
    public void playersick1()
    {
        spriteRenderer.color = new Color(1, 0.5f, 0.5f, 0.7f);
        Invoke("playersick2", 1.5f);
    }
    public void playersick2()
    {
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
        Invoke("playersick1", 1.5f);
    }
    public void playercolorreset()
    {
        CancelInvoke();
        spriteRenderer.color = new Color(1, 1, 1,1);
    }
}