using UnityEngine;
using System.Collections;
using System;

public class ATMMaoyeye : MonoBehaviour
{
    private float _ZMin = -2;
    private float _Speed = 45f;
    public static bool _IsShow;

    public static DelMoneyOu0t _EventMoneyBeenTaken;
    public static DelMoneyOu0t _EventMoneyOut;
    void OnEnable()
    {
        ATMAudioManager.PlayEF("CountMoney");
        StartCoroutine(MoneyOutIE());
        _IsShow = true;
    }

    IEnumerator MoneyOutIE()
    {
        while (transform.localPosition.z > _ZMin)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, -2f, _ZMin), Time.deltaTime * _Speed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (_EventMoneyOut != null)
            _EventMoneyOut();
        ATMAudioManager.PlayEF("MoneyOut");
    }

    void OnMouseDown()
    {
       
        gameObject.SetActive(false);

        if (_EventMoneyBeenTaken != null)
        {
            _EventMoneyBeenTaken();
        }
    }

    void OnDisable()
    {
        transform.localPosition = new Vector3(0, 20f, 37f);
        _IsShow = false;
    }
}
public delegate void DelMoneyOu0t();
