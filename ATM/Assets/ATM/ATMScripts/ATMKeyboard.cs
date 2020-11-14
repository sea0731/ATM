using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMKeyboard : MonoBehaviour
{
    public Button _Btn1;
    public Button _Btn2;
    public Button _Btn3;
    public Button _Btn4;
    public Button _Btn5;
    public Button _Btn6;
    public Button _Btn7;
    public Button _Btn8;
    public Button _Btn9;
    public Button _Btn00;
    public Button _Btn0;
    public Button _BtnDot;
    public Button _BtnCancel;
    public Button _BtnClear;
    public Button _BtnConfirm;
    void Awake()
    {
        BtnRigiter();

    }


    void BtnRigiter()
    {
        _Btn0.onClick.AddListener(OnBtn0Click);
        _Btn1.onClick.AddListener(OnBtn1Click);
        _Btn2.onClick.AddListener(OnBtn2Click);
        _Btn3.onClick.AddListener(OnBtn3Click);
        _Btn4.onClick.AddListener(OnBtn4Click);
        _Btn5.onClick.AddListener(OnBtn5Click);
        _Btn6.onClick.AddListener(OnBtn6Click);
        _Btn7.onClick.AddListener(OnBtn7Click);
        _Btn8.onClick.AddListener(OnBtn8Click);
        _Btn9.onClick.AddListener(OnBtn9Click);
        _Btn00.onClick.AddListener(OnBtn00Click);
        _BtnDot.onClick.AddListener(OnBtnDotClick);
        _BtnCancel.onClick.AddListener(OnBtnCancelClick);
        _BtnClear.onClick.AddListener(OnBtnClearClick);
        _BtnConfirm.onClick.AddListener(OnBtnConfirmClick);
    }

    public static event DelInput _EventInput0;
    public static event DelInput _EventInput1;
    public static event DelInput _EventInput2;
    public static event DelInput _EventInput3;
    public static event DelInput _EventInput4;
    public static event DelInput _EventInput5;
    public static event DelInput _EventInput6;
    public static event DelInput _EventInput7;
    public static event DelInput _EventInput8;
    public static event DelInput _EventInput9;
    public static event DelInput _EventInput00;
    public static event DelInput _EventInputDot;
    public static event DelInput _EventInputCancel;
    public static event DelInput _EventInputClear;
    public static event DelInput _EventInputConfirm;

    public void OnBtn0Click()
    {
        if (_EventInput0 != null)
        {
            _EventInput0();
        }

    }
    public void OnBtn1Click()
    {
        if (_EventInput1 != null)
        {
            _EventInput1();
        }

    }
    public void OnBtn2Click()
    {
        if (_EventInput2 != null)
        {
            _EventInput2();
        }
    }
    public void OnBtn3Click()
    {
        if (_EventInput3 != null)
        {
            _EventInput3();
        }
    }
    public void OnBtn4Click()
    {
        if (_EventInput4 != null)
        {
            _EventInput4();
        }
    }
    public void OnBtn5Click()
    {
        if (_EventInput5 != null)
        {
            _EventInput5();
        }
    }
    public void OnBtn6Click()
    {
        if (_EventInput6 != null)
        {
            _EventInput6();
        }
    }
    public void OnBtn7Click()
    {
        if (_EventInput7 != null)
        {
            _EventInput7();
        }
    }
    public void OnBtn8Click()
    {
        if (_EventInput8 != null)
        {
            _EventInput8();
        }
    }
    public void OnBtn9Click()
    {
        if (_EventInput9 != null)
        {
            _EventInput9();
        }
    }
    public void OnBtn00Click()
    {
        if (_EventInput00 != null)
        {
            _EventInput00();
        }
    }
    public void OnBtnDotClick()
    {
        if (_EventInputDot != null)
        {
            _EventInputDot();
        }
    }
    public void OnBtnCancelClick()
    {
        if (_EventInputCancel != null)
        {
            _EventInputCancel();
        }
    }
    public void OnBtnClearClick()
    {
        if (_EventInputClear != null)
        {
            _EventInputClear();
        }
    }
    public void OnBtnConfirmClick()
    {
        if (_EventInputConfirm != null)
        {
            _EventInputConfirm();
        }
    }
}
public delegate void DelInput();
