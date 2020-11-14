using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;


public class ATMTaskPannelManager : MonoBehaviour
{
    public static ATMTaskPannelManager _Instance;


    public static event DelTip PannelHideEvent;
    public static event DelTip PannelShowEvent;
    public Button _BtnTaskTwo;
    public Button _BtnNothing;

    public Image _BGTaskAll;
    public Text _taskText;

    private float _PanelHideX = -198f;
    private float _PanelShowX = -5f;

    private float _BtnTaskTwoHideX = -121;
    private float _BtnTaskTwoShowX = -81;

    private bool _IsAutoHide = true;

   
    bool isTaskPanelShow;
    

   
    public GameObject GetTaskBtn()
    {
        return _BtnTaskTwo.gameObject;
    }
    void Awake()
    {
        _Instance = this;
      
        BtnRegister();

        TaskPanelInit();

    }

  

    void BtnRegister()
    {

        _BtnTaskTwo.onClick.AddListener(OnBtnTaskClick);
        _BtnNothing.onClick.AddListener(OnBtnNothingClick);
    }

    void TaskPanelInit()
    {
        isTaskPanelShow = false;
        _BGTaskAll.transform.localPosition = new Vector3(_PanelHideX, _BGTaskAll.transform.localPosition.y, _BGTaskAll.transform.localPosition.z);
    }

    void OnBtnTaskClick()
    {
        if (isTaskPanelShow)
        {
            if (PannelHideEvent != null)
                PannelHideEvent();        
            _BGTaskAll.transform.DOLocalMoveX(_PanelHideX, 0.3f);
            isTaskPanelShow = false;
            //_BtnTaskTwo.transform.DOLocalMoveX(_BtnTaskTwoShowX, 0.3f);
        }
        else
        {
            if (PannelShowEvent != null)
                PannelShowEvent();
            _BGTaskAll.transform.DOLocalMoveX(_PanelShowX, 0.3f);
            //_BtnTaskTwo.transform.DOLocalMoveX(_BtnTaskTwoHideX, 0.3f);
            isTaskPanelShow = true;
            AutoHideBGTaskAll();
        }
    }
    public void HideTaskPanel()
    {
        if (isTaskPanelShow)
            OnBtnTaskClick();
    }

    public void SetTaskText(string s)
    {
        if (_taskText != null)
            _taskText.text = s;
    }

    void AutoHideBGTaskAll()
    {
        if (!_IsAutoHide)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(AutoHideBGTaskAllIE());
    }

    IEnumerator AutoHideBGTaskAllIE()
    {

        yield return new WaitForSeconds(30);
        if (isTaskPanelShow)
        {
            OnBtnTaskClick();
        }
    }



    void OnBtnNothingClick()
    {
        if (isTaskPanelShow)
        {
            OnBtnTaskClick();
        }
    }

}
public delegate void DelTip();

