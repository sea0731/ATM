using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenTransferSecond : ATMWithKeyBoard,IATMTipScreen
{
    public Button _BtnConfirm;
    public Button _BtnReturn;
    public Button _BtnClear;
    public Text _TxtValueUserName;
    public Text _TxtValueCard;

    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.TRANSFER;
    public override void OnEnable()
    {
        base.OnEnable();
        _TxtValueUserName.text = ATMScreenTransfer._Instance._targetName;
        _TxtValueCard.text = ATMScreenTransfer._Instance._targetId + "";
        SetTip();
    }
    protected override void GetChildInputField()
    {
        // _CurrentInputField = transform.FindChild("InputField").GetComponent<InputField>();
    }

    public void SetTip()
    {
       
       // if (ATMTaskManager.CurrentTaskType == ATMTASKTYPE.CARDOUT)
       //     SetTip(_BtnExit.transform, false);
        if (ATMTaskManager.CurrentTaskType != TASKTYPE)
        {
            SetTip(_BtnReturn.transform, false);
        }
        else
        {
            SetTip(_BtnConfirm.transform, false);
        }

    }
    private void SetTip(Transform go, bool keyboardEffect)
    {
        ATMFingerTip._instance.GoTip = go;
        ATMFingerTip._instance.KeyboardEffect = keyboardEffect;
    }

    public override void AddInput(string value)
    {
        // throw new NotImplementedException();
    }


    public override void Awake()
    {
        base.Awake();
        BtnRegister();
    }


    void BtnRegister()
    {
        _BtnConfirm.onClick.AddListener(OnBtnConfirmClick);
        _BtnReturn.onClick.AddListener(OnBtnReturnClick);
        //_BtnClear.onClick.AddListener(OnBtnClearClick);
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
        ATMScreenTransfer._Instance.SetGOActive(2);
    }

    void OnBtnReturnClick()
    {
        ATMScreenTransfer._Instance.SetGOActive(0);
    }
    void OnBtnExitClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.WELCOME;
    }
}
