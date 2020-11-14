
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMTrainningTransferOut : MonoBehaviour
{
    public static ATMTrainningTransferOut _Instance;
    public GameObject Second;
    public GameObject _money;

    void Awake()
    {
        _Instance = this;
        //SendMoneyOut();
    }

   
    void OnEnable()
    {
        Second.SetActive(true);
    }
    public void SendMoneyOut()
    {
        _money.SetActive(true);
    }
    void OnDisable()
    {
    }


}
