using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMScreenInputPassWord : ATMWithKeyBoard,IATMTipScreen
{
    public Button _BtnConfirm;
    public Button _BtnClear;
    public Button _BtnExit;
    public GameObject _InputErrorGo;
    
    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.PASSWORD;

    public Text _ShowMessage;
    private int _ErrorCount;
    //    public InputField _InputPassword;
    private GameObject _btnTasking;
    public override void Awake()
    {
        base.Awake();
        BtnRegister();
        _ErrorCount = 0;
        _btnTasking = ATMTaskPannelManager._Instance.GetTaskBtn();
    }

    void BtnRegister()
    {
        _BtnConfirm.onClick.AddListener(OnBtnComfirmClick);
        _BtnClear.onClick.AddListener(InputClear);
        _BtnExit.onClick.AddListener(OnBtnExitClick);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _InputErrorGo.SetActive(false);
        _CurrentInputField.text = "";
        SetTip();
       
    }

    public void SetTip()
    {
        SetTip(null, true);
    }
    public override void Start()
    {
        base.Start();
    }

    public override void AddInput(string value)
    {
        if (_CurrentInputField == null)
        {
            Debug.LogError("InputField had not been set");
        }
        if (_CurrentInputField.text.Length < ATMConfig._PassWordLength)
        {
            _CurrentInputField.text += value;
        }
        if(_CurrentInputField.text.Length == ATMConfig._PassWordLength)
            SetTip(transform.Find("BtnConfirm"), false);
        else if(_ErrorCount<3)
            SetTip(_btnTasking.transform, true);

    }

    public override void InputDot()
    {
        
    }

    public override void InputCancel()
    {
        OnBtnCancelClick();
    }

    public override void InputConfirm()
    {
        OnBtnComfirmClick();
    }

    void OnBtnComfirmClick()
    {
        //TODO: 确认按钮 事情
        if (ATMScreenManager._Instance.CheckPassWord(_CurrentInputField.text))
        {
            _ErrorCount = 0;
            ATMTaskManager._Instance.TaskDone(TASKTYPE);
            ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.USERPANEL;
            
        }
        else
        {
            PassWordError();
        }
    }

    void OnBtnCancelClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.WELCOME;
    }

    void OnBtnExitClick()
    {
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.WELCOME;
    }

    void PassWordError()
    {
        _ErrorCount++;
        StartCoroutine(PassWordErrorIE());
        if (_ErrorCount >= 3)
        {
            SetTip(_btnTasking.transform, false);
        }else
        {
            SetTip(_btnTasking.transform, true);
        }
    }

    IEnumerator PassWordErrorIE()
    {
        ATMAudioManager.PlayEF("PassWordError");
        _InputErrorGo.SetActive(true);
        yield return new WaitForSeconds(3f);
        _InputErrorGo.SetActive(false);
        InputClear();
    }
    /// <summary>
    /// 设置提示，第一个为手指位置，第二个为键盘提示是否开启
    /// </summary>
    /// <param name="go"></param>
    /// <param name="keyboardEffect"></param>
    void SetTip(Transform go,bool keyboardEffect)
    {
        ATMFingerTip._instance.GoTip = go;
        ATMFingerTip._instance.KeyboardEffect = keyboardEffect;
    }
}
