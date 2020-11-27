using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenTransferOut : MonoBehaviour
{
    public static ATMScreenTransferOut _Instance;
    public GameObject[] screens;

    public int _OutMoney;

    void Awake()
    {
        _Instance = this;
    }

    public void SetGOActive(int index)
    {
        if (screens.Length < 2)
        {
            Debug.LogError("you need add GO into ALL[]");
        }
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
        screens[index].SetActive(true);
    }

    void OnEnable()
    {
        SetGOActive(0);
    }

    void OnDisable()
    {
    }


}
