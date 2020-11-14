using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenTransferFirst : ATMWithKeyBoard,IATMTipScreen
{
    public Button _BtnConfirm;
    public Button _BtnClear;
    public Button _BtnReturn;
    public Button _BtnExit;
    public GameObject _ShowInputErrorGo;

    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.TRANSFER;


    public override void Awake()
    {
        base.Awake();
        BtnRegister();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        ATMScreenTransfer._Instance.SetUserInfoNull();
        _ShowInputErrorGo.SetActive(false);
        _CurrentInputField.text = "";
        ATMAudioManager.PlayEF("TransferInoutID");
        SetTip();
    }

    public void SetTip()
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
        _BtnClear.onClick.AddListener(InputClear);
        _BtnReturn.onClick.AddListener(OnBtnReturnClick);
        _BtnExit.onClick.AddListener(OnBtnExitClick);
    }

    public override void AddInput(string value)
    {
        if (_CurrentInputField == null)
        {
            Debug.LogError("InputField had not been set");
        }
        if (_CurrentInputField.text.Length < ATMConfig._AccountLength)
        {
            _CurrentInputField.text += value;
        }
        if (ATMTaskManager.CurrentTaskType == TASKTYPE)
            if (_CurrentInputField.text.Length == ATMConfig._AccountLength)
                SetTip(transform.Find("BtnConfirm"), false);
            else
            {
                SetTip(ATMTaskPannelManager._Instance.GetTaskBtn().transform, true);
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
        if (_CurrentInputField.text == "")
        {
            return;
        }
        string id = _CurrentInputField.text;
        string name = CardUserManager.CheckUserNameByID(id);
        if (name != "")
        {
            ATMScreenTransfer._Instance.SetUserInfo(id, name);
        }
        else
        {
            ShowInputError();
        }
    }

    void OnBtnReturnClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.USERPANEL;
    }
    void OnBtnExitClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.WELCOME;
    }

    void ShowInputError()
    {
        StartCoroutine(ShowInputErrorIE());
    }

    IEnumerator ShowInputErrorIE()
    {
        _ShowInputErrorGo.SetActive(true);
        yield return new WaitForSeconds(2f);
        _ShowInputErrorGo.SetActive(false);
        InputClear();
    }
}
