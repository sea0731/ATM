using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMFingerTip : MonoBehaviour
{
    public static ATMFingerTip _instance;

    public static event DelTip _FigerTipShowEvent;
    public static event DelTip _KeyboardTipShowEvent;
    public bool _AutoHide = true;
    public bool _isOpen=false;
    private float timer;
    private Transform _goTip;
    private bool _keyboardEffect;
    private bool _isFigerTipShow;
    private bool _isKeyboardTipShow;
    public Transform GoTip
    {
        get
        {
            return _goTip;
        }

        set
        {
            _goTip = value;
            ResetTimer();
        }
    }

    public bool KeyboardEffect
    {
        get
        {
            return _keyboardEffect;
        }

        set
        {
            _keyboardEffect = value;
            ResetTimer();
        }
    }

    void Awake()
    {
        _instance = this;
        timer = 0;
        _keyboardEffect = false;
        _isFigerTipShow = false;
        _isKeyboardTipShow = false;
    }
 
    void Update()
    {
        //if (Input.anyKeyDown)
        //{
        //    ATMTaskTipFingerManager._Instance.HideTipFinger();
        //}
        if (Input.anyKeyDown&& _AutoHide)
        {
            if(_isFigerTipShow|| _isKeyboardTipShow)
            {
                _isFigerTipShow = false;
                _isKeyboardTipShow = false;
                ATMTaskTipFingerManager._Instance.HideTipFinger();
            }
            ResetTimer();
        }
           
    }

    public void ShowTip(Transform _go)
    {
        if (_isFigerTipShow)
            return;
        if (_FigerTipShowEvent != null)
            _FigerTipShowEvent();
       
        ATMTaskTipFingerManager._Instance.ShowTipFinger(_go);
        _isFigerTipShow = true;
    }
    public void ShowKeyboardTip()
    {
        if (_isKeyboardTipShow)
            return;
        if (_KeyboardTipShowEvent != null)
            _KeyboardTipShowEvent();

        ATMTaskTipFingerManager._Instance.ShowKeyboardEffect();
        _isKeyboardTipShow = true;
    }
    public IEnumerator TimerCount()
    {
        for (timer = 0; timer < ATMConfig._tipTime; timer += Time.deltaTime)
        {
            yield return 0;
        }
        if(_isOpen)
        {
            if (GoTip != null)
            {
                ShowTip(GoTip);
                //Debug.LogError("Counter!");
            }
                

            if (KeyboardEffect)
                ShowKeyboardTip();
        }
        
    }
    public void ResetTimer()
    {
        timer = 0;
        StopAllCoroutines();
        StartCoroutine(TimerCount());
    }
    public void SetTip(Transform go)
    {
        GoTip = go;
    }
    public void Close()
    {
        StopAllCoroutines();
    }
    void OnDestroy()
    {
        _instance = null;
    }
}
