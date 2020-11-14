using UnityEngine;
using System.Collections;
using System;

public class ATMRealCard : MonoBehaviour
{
    public static ATMRealCard Instance;

    public bool _switch = false;

    private float _ZMax = 57f;  //卡完全进去的Z轴本地位置
    private float _ZMin = -28.81f;   //卡完全出来的Z轴本地位置

    private float _Speed = 100f;
    public static DelCard _EventCardIn;
    void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        ATMScreenManager._EventCardOut += CardOut;
    }

    void OnDisable()
    {
        ATMScreenManager._EventCardOut -= CardOut;
    }
    void OnDestroy()
    {
        Instance = null;
    }
    public GameObject GetCardGo()
    {
        return gameObject;
    }
    private void CardOut()
    {
        StopAllCoroutines();
        StartCoroutine(CardInOutIE(false));
    }

    private void CardIn()
    {
        StopAllCoroutines();
        StartCoroutine(CardInOutIE(true));
      
        if (_EventCardIn != null)
        {
            _EventCardIn();
        }
    }


    void OnMouseDown()
    {
        if(_switch)
            CardIn();
    }

    IEnumerator CardInOutIE(bool _iscardIn)
    {
        if (_iscardIn)
        {
            while (transform.localPosition.z < _ZMax)
            {
                transform.localPosition += Vector3.forward * _Speed * Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }       

        }
        else
        {
            while (transform.localPosition.z > _ZMin)
            {
                transform.localPosition -= Vector3.forward * _Speed * Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            ATMAudioManager.PlayEF("Exit");
        }
    }
}

public delegate void DelCard();
