using UnityEngine;
using System.Collections;
using System;

public class ATMScreenTransfer : MonoBehaviour
{
    public static ATMScreenTransfer _Instance;
    public GameObject[] All;

    public string _targetId;
    public string _targetName;
    public int _transfermoney;

    void Awake()
    {
        _Instance = this;
    }

    public void SetGOActive(int index)
    {
        if (All.Length < 4)
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
        SetUserInfoNull();
    }

    public void SetUserInfo(string id, string name)
    {
        if (_targetId != "")
        {
            Debug.LogError("_targetId has been set");
        }
        _targetId = id;
        _targetName = name;
        SetGOActive(1);
    }

    public void SetUserInfoNull()
    {
        _targetId = "";
        _targetName = "";
    }
}
