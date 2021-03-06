﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using I2.Loc;

public class ATMScreenTransferForth : MonoBehaviour,IATMTipScreen
{
    public Button _BtnGoOn;
    public Button _BtnReturn;
    public Text _ShowMessage;
    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.TRANSFER;
    void Awake()
    {
        BtnRegister();
    }


    void BtnRegister()
    {
        _BtnGoOn.onClick.AddListener(OnBtnGoOnClick);
        _BtnReturn.onClick.AddListener(OnBtnReturnClick);
    }

    void OnEnable()
    {
        HideButton();
        _ShowMessage.text = "";
        StartCoroutine(ShowTransferOutING());
        SetTip();
    }

    public void SetTip()
    {
        if(ATMTaskManager.CurrentTaskType!= TASKTYPE)
            SetTip(_BtnReturn.transform, false);
        else
            SetTip(_BtnGoOn.transform, false);
    }
    private void SetTip(Transform go, bool keyboardEffect)
    {
        ATMFingerTip._instance.GoTip = go;
        ATMFingerTip._instance.KeyboardEffect = keyboardEffect;
    }
    void OnDisable()
    {
        HideButton();
        _ShowMessage.text = "";
    }

    IEnumerator ShowTransferOutING()
    {
        _ShowMessage.text = ScriptLocalization.TipWaitingTransfer;
        yield return new WaitForSeconds(2);
        //        if (ATMScreenTransfer._Instance._transfermoney == 0)
        //        {
        //            ATMAudioManager.PlayEF("OperationError");
        //            _ShowMessage.text = "对不起,暂不能办理此项业务";
        //        }
        //        else
        //        {
        //        }

        _ShowMessage.text = ScriptLocalization.TipTransferFinish1 + ATMScreenTransfer._Instance._targetName + ScriptLocalization.TipTransferFinish2 + string.Format("{0:N}", GameDataManager.FlowData.task.money) + ScriptLocalization.TipTransferFinish3;

        yield return new WaitForSeconds(2);
        ShowButton();
    }

    void HideButton()
    {
        _BtnGoOn.gameObject.SetActive(false);
        _BtnReturn.gameObject.SetActive(false);
    }

    void ShowButton()
    {
        _BtnGoOn.gameObject.SetActive(true);
        _BtnReturn.gameObject.SetActive(true);
    }


    void OnBtnGoOnClick()
    {
        ATMScreenTransfer._Instance.SetGOActive(0);
    }

    void OnBtnReturnClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.USERPANEL;
    }
}

