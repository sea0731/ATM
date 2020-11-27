using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using GameData;

public class ATMScreenTransferOutFirst : ATMWithKeyBoard,IATMTipScreen
{
    public Button _Btn100;
    public Button _Btn300;
    public Button _Btn500;
    public Button _Btn1000;
    public Button _Btn2000;
    public Button _Btn3000;
    public Button _BtnConfirm;
    public Button _BtnReturn;

    public Text _ShowMessage;
    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.DRAW;
    public override void Awake()
    {
        base.Awake();
        BtnRegister();
    }

    public override void OnEnable()
    {
        ATMAudioManager.PlayEF("TransferOut");
        base.OnEnable();
        _CurrentInputField.text = "";
        SetTip();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SetTip(null, false);
    }
    public void SetTip()
    {
        if (ATMTaskManager.CurrentTaskType != TASKTYPE)
            SetTip(_BtnReturn.transform, false);
        else
            SetTip(null, true);
    }
    public void SetTipNext()
    {
        if (ATMTaskManager.CurrentTaskType == TASKTYPE)
            SetTip(_BtnConfirm.transform, true);
    }
    private void SetTip(Transform go, bool keyboardEffect)
    {
        ATMFingerTip._instance.GoTip = go;
        ATMFingerTip._instance.KeyboardEffect = keyboardEffect;
    }
    void BtnRegister()
    {
        _BtnConfirm.onClick.AddListener(OnBtnConfirmClick);
        _BtnReturn.onClick.AddListener(OnBtnReturnClick);
        _Btn100.onClick.AddListener(OnBtn100Click);
        _Btn300.onClick.AddListener(OnBtn300Click);
        _Btn500.onClick.AddListener(OnBtn500Click);
        _Btn1000.onClick.AddListener(OnBtn1000Click);
        _Btn2000.onClick.AddListener(OnBtn2000Click);
        _Btn3000.onClick.AddListener(OnBtn3000Click);
    }

    public override void AddInput(string value)
    {
        if (_CurrentInputField == null)
        {
            Debug.LogError("InputField had not been set");
        }
        if (_CurrentInputField.text.Length < GobalData._MoneyLength)
        {
            _CurrentInputField.text += value;
        }
        SetTipNext();
    }

    public override void InputCancel()
    {
        OnBtnReturnClick();
    }
    public override void InputConfirm()
    {
        OnBtnConfirmClick();
    }


    void OnBtnConfirmClick()
    {
        if (_CurrentInputField.text == "")
        {
            return;
        }
        if (int.Parse(_CurrentInputField.text) % 100 != 0)
        {
            _CurrentInputField.text = "";
            ShowOnly100();
            return;
        }

        int money = int.Parse(_CurrentInputField.text);
        if (GameDataManager.FlowData.task.money != money)
        {
            _CurrentInputField.text = "";
            MoneyInCorrect();
            return;
        }
        if (money <= 0)
        {
            _CurrentInputField.text = "";
            return;
        }

        int outmoney = GameDataManager.FlowData.userCard.transferOut(money);
        ATMScreenTransferOut._Instance._OutMoney = outmoney;
        GameUI._instance.TaskDone(TASKTYPE);
        ATMScreenTransferOut._Instance.SetGOActive(1);
    }

    void ShowOnly100()
    {
        StartCoroutine(ShowOnly100IE());
    }
    void MoneyInCorrect()
    {
        StartCoroutine(MoneyInCorrectIE());
    }

    private IEnumerator MoneyInCorrectIE()
    {
        _ShowMessage.text = "金额不对";
        yield return new WaitForSeconds(3f);
        _ShowMessage.text = "";
    }

    IEnumerator ShowOnly100IE()
    {
        _ShowMessage.text = "仅支持整佰取款";
        yield return new WaitForSeconds(3f);
        _ShowMessage.text = "";
    }

    void OnBtnReturnClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.USERPANEL;
    }

    void OnBtn100Click()
    {
        _CurrentInputField.text = "100";
        SetTipNext();
    }
    void OnBtn300Click()
    {
        _CurrentInputField.text = "300";
        SetTipNext();
    }
    void OnBtn500Click()
    {
        _CurrentInputField.text = "500";
        SetTipNext();
    }
    void OnBtn1000Click()
    {
        _CurrentInputField.text = "1000";
        SetTipNext();
    }
    void OnBtn2000Click()
    {
        _CurrentInputField.text = "2000";
        SetTipNext();
    }
    void OnBtn3000Click()
    {
        _CurrentInputField.text = "3000";
        SetTipNext();
    }
}
