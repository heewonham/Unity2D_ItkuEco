using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetManager : MonoBehaviour
{
    public GameManager manager;
    public bool chk;
    public int Date;

    void Awake()
    {
        Date = manager.date;
    }
    void OnEnable()
    {
        Date = manager.date;
    }
    void Update()
    {
        if (Date != manager.date)
        {
            Date = manager.date;
            chk = false;
        }
    }
}
