using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMScreenTransferOutSecond : MonoBehaviour
{
    public static BankNoteOutMoney _EventOutMoney;
    public Text _ShowMessage;
    void OnEnable()
    {
        StartCoroutine(ShowTransferOutING());
    }

    IEnumerator ShowTransferOutING()
    {
        _ShowMessage.text = "取钱中，请稍等...";
        yield return new WaitForSeconds(2);
        if (ATMScreenTransferOut._Instance._OutMoney == 0)
        {
            _ShowMessage.text = "账户余额不足";
        }
        else
        {
            _EventOutMoney(ATMScreenTransferOut._Instance._OutMoney);
            ATMScreenTransferOut._Instance._OutMoney = 0;
        }
        yield return new WaitForSeconds(2);
        ATMScreenTransferOut._Instance.SetGOActive(2);
        _ShowMessage.text = "";
    }
}

public delegate void BankNoteOutMoney(int money);
