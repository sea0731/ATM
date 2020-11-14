using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenBalance : ATMWithKeyBoard,IATMTipScreen
{
    public Button _BtnConfirm;
    public Text text;
    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.CHECK;
    public override void AddInput(string value)
    {
    }
    public override void Awake()
    {
        BtnRegister();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        ShowBalance();
        SetTip();
    }

    public void SetTip()
    {
        SetTip(_BtnConfirm.transform,false);
    }

    private void SetTip(Transform go, bool keyboardEffect)
    {
        ATMFingerTip._instance.GoTip = go;
        ATMFingerTip._instance.KeyboardEffect = keyboardEffect;
    }
    void BtnRegister()
    {
        _BtnConfirm.onClick.AddListener(OnBtnConfirmClick);
    }

    public override void InputConfirm()
    {
        OnBtnConfirmClick();
    }

    void ShowBalance()
    {
        // text.text = CardUserManager.CheckUserBalanceByID(ScreenManager._Instance.CurrentCardUser.UserID);
        text.text = string.Format("{0:N}", CardUserManager.CheckUserBalanceByID(ATMScreenManager._Instance.CurrentCardUser.UserID));
        ATMTaskManager._Instance.TaskDone(TASKTYPE);

    }

    void OnBtnConfirmClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.USERPANEL;
    }
}
