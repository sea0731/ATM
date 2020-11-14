using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ATMTaskTipFingerManager : MonoBehaviour
{
    public static ATMTaskTipFingerManager _Instance;

    public static event DelTipFiger _TipHideEvent;
    public GameObject _keyboard;
    GameObject _Finger;
    float _FadeSpeed = 15f;
    Color _keyboardColor;
    void Awake()
    {
        _Instance = this;
        _Finger = transform.Find("finger").gameObject;
        _keyboardColor = _keyboard.GetComponent<Image>().color;
    }

    public void ShowKeyboardEffect()
    {
        StartCoroutine(ATMKeyboardEffectOn());
    }

    private IEnumerator ATMKeyboardEffectOn()
    {
        while (true)
        {
            _keyboard.GetComponent<Image>().color = new Color((Mathf.Sin(10*Time.time)*50+205)/255, (Mathf.Sin(10 * Time.time) * 50 + 205) / 255, (Mathf.Sin(10 * Time.time) * 50 + 205) / 255); 
            yield return new WaitForFixedUpdate();
        }
    }
    private void ResetKeyboardEffect()
    {
        _keyboard.GetComponent<Image>().color = new Color(1,1,1);
    }

    public void ShowTipFinger(GameObject _GO)
    {
        ShowTipFinger(_GO.transform);
    }

    public void ShowTipFinger(Button _Btn)
    {
        ShowTipFinger(_Btn.transform);
      
    }

    public void ShowTipFinger(Transform _trans)
    {

        _Finger.gameObject.SetActive(true);
        //_Finger.transform.localPosition = new Vector3(_trans.localPosition.x, _trans.localPosition.y, 0);
        //_Finger.transform.position = new Vector3(_trans.position.x, _trans.position.y, _trans.position.z);
        Vector2 pos = Camera.main.WorldToScreenPoint(_trans.position);
        _Finger.transform.position = new Vector3(pos.x, pos.y, 0);
        StartCoroutine(ShowTransformIE());
    }

    IEnumerator ShowTransformIE()
    {
        bool isBig = true;
        while (true)
        {
            if (isBig)
            {
                _Finger.transform.localScale = Vector3.Lerp(_Finger.transform.localScale, Vector3.one * 1.4f, _FadeSpeed * Time.fixedDeltaTime);
                if (_Finger.transform.localScale.x >= 1.399f)
                {
                    isBig = false;
                }
            }
            else
            {
                _Finger.transform.localScale = Vector3.Lerp(_Finger.transform.localScale, Vector3.one, _FadeSpeed * Time.fixedDeltaTime);
                if (_Finger.transform.localScale.x <= 1.01f)
                {
                    isBig = true;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void HideTipFinger()
    {
        if (_TipHideEvent != null)
            _TipHideEvent();
        StopAllCoroutines();
        ResetKeyboardEffect();
        _Finger.transform.SetParent(transform);
        _Finger.transform.localScale = Vector3.one;
        _Finger.gameObject.SetActive(false);
    }
    public delegate void DelTipFiger();
}

