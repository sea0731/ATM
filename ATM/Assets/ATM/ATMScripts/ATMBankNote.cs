using UnityEngine;
using System.Collections;

public class ATMBankNote : MonoBehaviour
{
    public static ATMBankNote Instance;
    // private bool HasMoney = false;
    public ATMCashGO _CashGO;
    public GameObject _MoneyGO;

    private float _thismoney;

    void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        ATMScreenTransferOutSecond._EventOutMoney += OutMoney;
        ATMMaoyeye._EventMoneyBeenTaken += OutMoneyTip;
    }
    void OnDisable()
    {
        ATMScreenTransferOutSecond._EventOutMoney -= OutMoney;
        ATMMaoyeye._EventMoneyBeenTaken -= OutMoneyTip;
    }
    void OnDestroy()
    {
        Instance = null;
    }
    public GameObject GetCashGo()
    {
        return _MoneyGO;
    }
    void OutMoney(int money)
    {
        _thismoney = money;
        _MoneyGO.gameObject.SetActive(true);

    }

    void OutMoneyTip()
    {
        StartCoroutine(OutMoneyTipIE());
    }

    IEnumerator OutMoneyTipIE()
    {
        yield return new WaitForSeconds(0.1f);
        _CashGO.gameObject.SetActive(true);
        _CashGO._ValueCash.text = "您已成功取出" + _thismoney + "元";
    }
}

