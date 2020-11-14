using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenTransferThird : ATMWithKeyBoard
{
    public Button _BtnConfirm;
    public Button _BtnClear;
    public Button _BtnReturn;
    public Button _BtnExit;
    public Text _ShowMessage;
    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.TRANSFER;
    private bool _canConfirm = true;
    public override void Awake()
    {
        base.Awake();
        BtnRegister();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _CurrentInputField.text = "";
        ATMAudioManager.PlayEF("TransferInputCount");
        SetTip();
    }

    private void SetTip()
    {
        if (ATMTaskManager.CurrentTaskType == ATMTASKTYPE.CARDOUT)
            SetTip(_BtnExit.transform, false);
        else if (ATMTaskManager.CurrentTaskType != TASKTYPE)
        {
            SetTip(_BtnReturn.transform, false);
        }
        else
        {
            SetTip(null, true);
        }
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
        _BtnClear.onClick.AddListener(InputClear);
        _BtnExit.onClick.AddListener(OnBtnExitClick);
    }

    public override void AddInput(string value)
    {
        if (_CurrentInputField.text.Length < ATMConfig._MoneyLength)
        {
            _CurrentInputField.text += value;
        }
        string[] values = _CurrentInputField.text.Split('.');
        if (values.Length >= 3)
        {
            _canConfirm = false;
            SetTip(_BtnClear.transform, false);
        }
        else
        {
            _canConfirm = true;
            SetTip(_BtnConfirm.transform, false);
        }
    }

    public override void InputConfirm()
    {
        OnBtnConfirmClick();
    }
    public override void InputCancel()
    {
        OnBtnReturnClick();
    }

    void OnBtnConfirmClick()
    {
        if (_canConfirm == false)
        {
            _CurrentInputField.text = "";
            return;
        }
        int inputmoney;
        try
        {
            inputmoney = int.Parse(_CurrentInputField.text);
        }
        catch (Exception e)
        {
            _CurrentInputField.text = "";
            return;
        }
       
        if (inputmoney <= 0)
        {
            _CurrentInputField.text = "";
            return;
        }
        if (!ATMTaskManager._Instance.CheckMoney(inputmoney))
        {
            _CurrentInputField.text = "";
            MoneyInCorrect();
            return;
        }
            
       
        int outmoney = CardUserManager.TransferOutByID(ATMScreenManager._Instance.CurrentCardUser.UserID, inputmoney);
        if (outmoney > 0)
        {
            CardUserManager.TransferInCard(ATMScreenTransfer._Instance._targetId, outmoney);
        }
        ATMTaskManager._Instance.TaskDone(TASKTYPE);
        ATMScreenTransfer._Instance._transfermoney = outmoney;
        ATMScreenTransfer._Instance.SetGOActive(3);
    }

    private void MoneyInCorrect()
    {
        StartCoroutine(MoneyInCorrectIE());
    }
    private IEnumerator MoneyInCorrectIE()
    {
        _ShowMessage.text = "金额不对";
        yield return new WaitForSeconds(3f);
        _ShowMessage.text = "";
    }
    void OnBtnReturnClick()
    {
        ATMScreenTransfer._Instance.SetGOActive(1);
    }

    void OnBtnExitClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.WELCOME;
    }

}
