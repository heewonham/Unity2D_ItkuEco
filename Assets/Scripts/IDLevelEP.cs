using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class IDLevelEP : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public ParticleSystem effect;

    public bool Isbaby; // 아기인지 여부
    public int ArrayID; // 동물생성
    public int default_Ep; // 기본 

    public int Num; // 고유 넘버, nest는 변동무, 나머지는 변동함.
    public bool Is=false; // 동물이 있는지 여부
    public GameObject hungrypanel;
    public GameObject sickpanel;
    public bool makeani = false;
    public bool hungry = false;
    public int Animal_Id; // 동물아이디
    public int Animal_EP; // 경험치
    public int Animal_Damage = 0; // 데미지
    public int animalSex = 2;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Animal_Id];
        effect.Stop();
    }
    void OnEnable()
    {
        spriteRenderer.sprite = sprites[Animal_Id];
    }
    void Update()
    {
        if (makeani == true)
        {
            makeani = false;
            spriteRenderer.sprite = sprites[Animal_Id];
            effect.Play();
        }
        hungrypanel.SetActive(hungry);
        // 배고픈 후 시간이 지난뒤 밥 안주면 데미지
        chkDamage();
        spriteRenderer.sprite = sprites[Animal_Id];
    }

    public void TimeFlow()
    {
        if (hungry)
            Animal_Damage++;
        else if (Animal_Damage >= 2)
            return;
        else
            hungry = true;
    }
    void chkDamage()
    {
        if (Animal_Damage == 1)
            sickpanel.SetActive(true);
        else if (Animal_Damage >= 2)
        {
            if (Isbaby)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                hungrypanel.SetActive(false);
            }
            else
            {
                AnimalMove move = gameObject.GetComponent<AnimalMove>();
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                hungrypanel.SetActive(false);
                move.isAct = false;
             }
        }
    }
    // 맨 처음으로 돌아가기
    public void Restart()
    {
        reset();
        gameObject.SetActive(false);
    }
    public void reset()
    {
        Animal_EP = default_Ep;
        Animal_Damage = 0;
        animalSex = 2;
        hungry = false;
        Animal_Id = 0;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        hungrypanel.SetActive(false);
        sickpanel.SetActive(false);
        Is = false;
        if (Isbaby)
            spriteRenderer.sprite = sprites[0];
        else
            Num = 0;
    }
    public void revival()
    {
        Animal_Damage = 0;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        sickpanel.SetActive(false);
    }
}