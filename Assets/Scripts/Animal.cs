using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameManager manager;
    public saveManager save;

    public int BringUpM; // 기르는 포유류
    public int BringUpB;  // 기르는 조류
    public int AnimalNS; // 현재 기르는 동물 수

    public int currentM; // 소유한 포유류;
    public int currentB; // 소유한 조류;
    public int TotalAnimal; // 땅크기에 맞는 기를 수 있는 동물 수

    public int[,] Completed = new int[11, 2]; //완료된 동물
    public int completedanimal;

    void OnEnable()
    {
        BringUpB = save.sBringUpB;
        BringUpM = save.sBringUpM;
        currentB = save.sCurrentB;
        currentM = save.sCurrentM;
        for(int i = 0; i < 11; i++)
        {
            Completed[i, 0] = save.sCompleted[i, 0];
            Completed[i, 1] = save.sCompleted[i, 1];
        }
        completedanimal = save.sCompleted_Animal;
    }
    void LateUpdate()
    {
         if (manager.playerlevel < 5)
            TotalAnimal = 8;
        else if (manager.playerlevel < 8)
            TotalAnimal = 10;
        else
            TotalAnimal = 12;

        AnimalNS = BringUpM + BringUpB;

    }
}
