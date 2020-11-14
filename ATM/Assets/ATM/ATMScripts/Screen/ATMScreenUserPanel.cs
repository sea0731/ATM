using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenUserPanel : MonoBehaviour,IATMTipScreen
{
    public Button _BtnTransfer;
    public Button _BtnTransferIn;
    public Button _BtnTransferOut;
    public Button _BtnBalance;
    public Button _BtnExit;
    public GameObject _taskErrorMessage;
    void Awake()
    {
        BtnRegister();
    }
    void OnEnable()
    {
        ATMAudioManager.PlayEF("UserPanel");
        SetTip();
    }

    public void SetTip()
    {
        switch (ATMTaskManager.CurrentTaskType)
        {
            case ATMTASKTYPE.CHECK:
                ATMFingerTip._instance.SetTip(_BtnBalance.transform);
                break;
            case ATMTASKTYPE.TRANSFER:
                ATMFingerTip._instance.SetTip(_BtnTransfer.transform);
                break;
            case ATMTASKTYPE.DRAW:
                ATMFingerTip._instance.SetTip(_BtnTransferOut.transform);
                break;
            case ATMTASKTYPE.CARDOUT:
                ATMFingerTip._instance.SetTip(_BtnExit.transform);
                break;
            default:
                ATMFingerTip._instance.SetTip(_BtnExit.transform);
                break;
        }
    }

    void BtnRegister()
    {
        _BtnTransfer.onClick.AddListener(OnBtnTransferClick);
        _BtnTransferIn.onClick.AddListener(OnBtnTransferInClick);
        _BtnTransferOut.onClick.AddListener(OnBtnTransferOutClick);
        _BtnBalance.onClick.AddListener(OnBtnBalanceClick);
        _BtnExit.onClick.AddListener(OnBtnExitClick);
    }

    void OnBtnTransferClick()
    {
        if (ATMTaskManager.CurrentTaskType != ATMTASKTYPE.TRANSFER&& ATMTaskManager.CurrentTaskType !=ATMTASKTYPE.EMPTY)
            ShowTaskError();
        else
            ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.TRANSFER;
    }
    void OnBtnTransferInClick()
    {

        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.TRANSFERIN;
    }
    void OnBtnTransferOutClick()
    {
        if (ATMTaskManager.CurrentTaskType != ATMTASKTYPE.DRAW && ATMTaskManager.CurrentTaskType != ATMTASKTYPE.EMPTY)
            ShowTaskError();
        else
            ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.TRANSFEROUT;
    }
    void OnBtnBalanceClick()
    {
        if (ATMTaskManager.CurrentTaskType != ATMTASKTYPE.CHECK && ATMTaskManager.CurrentTaskType != ATMTASKTYPE.EMPTY)
            ShowTaskError();
        else
            ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.BALANCE;
    }
    void OnBtnExitClick()
    {
        if (ATMTaskManager.CurrentTaskType != ATMTASKTYPE.CARDOUT && ATMTaskManager.CurrentTaskType != ATMTASKTYPE.EMPTY)
            ShowTaskError();
        else
            ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.WELCOME;
    }
    void ShowTaskError()
    {
        _taskErrorMessage.SetActive(true);
    }
}
