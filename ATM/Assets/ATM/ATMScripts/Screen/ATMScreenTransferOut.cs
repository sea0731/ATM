using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenTransferOut : MonoBehaviour
{
    public static ATMScreenTransferOut _Instance;
    public GameObject[] All;

    public int _OutMoney;

    void Awake()
    {
        _Instance = this;
    }

    public void SetGOActive(int index)
    {
        if (All.Length < 2)
        {
            Debug.LogError("you need add GO into ALL[]");
        }
        for (int i = 0; i < All.Length; i++)
        {
            All[i].SetActive(false);
        }
        All[index].SetActive(true);
    }

    void OnEnable()
    {
        SetGOActive(0);
    }

    void OnDisable()
    {
    }


}
