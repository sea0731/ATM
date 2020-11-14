using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class ATMWithKeyBoard : MonoBehaviour
{
    protected InputField _CurrentInputField;

    public virtual void OnEnable()
    {

        ATMKeyboard._EventInput1 += Input1;
        ATMKeyboard._EventInput2 += Input2;
        ATMKeyboard._EventInput3 += Input3;
        ATMKeyboard._EventInput4 += Input4;
        ATMKeyboard._EventInput5 += Input5;
        ATMKeyboard._EventInput6 += Input6;
        ATMKeyboard._EventInput7 += Input7;
        ATMKeyboard._EventInput8 += Input8;
        ATMKeyboard._EventInput9 += Input9;
        ATMKeyboard._EventInput0 += Input0;
        ATMKeyboard._EventInput00 += Input00;
        ATMKeyboard._EventInputDot += InputDot;
        ATMKeyboard._EventInputCancel += InputCancel;
        ATMKeyboard._EventInputClear += InputClear;
        ATMKeyboard._EventInputConfirm += InputConfirm;
    }

    public virtual void OnDisable()
    {
        ATMKeyboard._EventInput1 -= Input1;
        ATMKeyboard._EventInput2 -= Input2;
        ATMKeyboard._EventInput3 -= Input3;
        ATMKeyboard._EventInput4 -= Input4;
        ATMKeyboard._EventInput5 -= Input5;
        ATMKeyboard._EventInput6 -= Input6;
        ATMKeyboard._EventInput7 -= Input7;
        ATMKeyboard._EventInput8 -= Input8;
        ATMKeyboard._EventInput9 -= Input9;
        ATMKeyboard._EventInput0 -= Input0;
        ATMKeyboard._EventInput00 -= Input00;
        ATMKeyboard._EventInputDot -= InputDot;
        ATMKeyboard._EventInputCancel -= InputCancel;
        ATMKeyboard._EventInputClear -= InputClear;
        ATMKeyboard._EventInputConfirm -= InputConfirm;
    }
    public virtual void Awake()
    {
        GetChildInputField();
    }

    public virtual void Start()
    {

    }

    protected virtual void GetChildInputField()
    {
        _CurrentInputField = transform.Find("InputField").GetComponent<InputField>();
    }

    public abstract void AddInput(string value);


    public virtual void Input1()
    {
        AddInput("1");
    }
    public virtual void Input2()
    {
        AddInput("2");
    }
    public virtual void Input3()
    {
        AddInput("3");
    }
    public virtual void Input4()
    {
        AddInput("4");
    }
    public virtual void Input5()
    {
        AddInput("5");
    }
    public virtual void Input6()
    {
        AddInput("6");
    }
    public virtual void Input7()
    {
        AddInput("7");
    }
    public virtual void Input8()
    {
        AddInput("8");
    }
    public virtual void Input9()
    {
        AddInput("9");
    }
    public virtual void Input0()
    {
        AddInput("0");
    }

    public virtual void Input00()
    {
        for (int i = 0; i < 2; i++)
        {
            Input0();
        }
    }
    public virtual void InputDot()
    {
        AddInput(".");
    }
    public virtual void InputCancel()
    {

    }
    public virtual void InputClear()
    {
        _CurrentInputField.text = "";

        ATMFingerTip._instance.GoTip = ATMTaskPannelManager._Instance.GetTaskBtn().transform;
        ATMFingerTip._instance.KeyboardEffect = true;
    }

    public virtual void InputConfirm()
    {
    }
}
