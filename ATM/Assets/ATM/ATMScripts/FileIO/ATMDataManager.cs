using UnityEngine;
using System.Collections;
using System;

public class ATMDataManager : MonoBehaviour {

    public static ATMDataManager Instance;
    private DateTime _startTime;
    private DateTime _finishTime;

    public DateTime GetStartTime()
    {
        return _startTime;
    }
    public TimeSpan FinishTime
    {
        get;set;
    }
    void Awake()
    {
        Instance = this;
        _startTime = DateTime.Now;
        ATMFileIO.CreateFile(_startTime,"123456");
        ATMFileIO.WriteHead("Time,Action");

    }
    void Start()
    {
        ATMRealCard._EventCardIn += CardInRecord;
        ATMMaoyeye._EventMoneyBeenTaken += MoneyTaken;

        ATMTaskPannelManager.PannelHideEvent += TaskPannelHide;
        ATMTaskPannelManager.PannelShowEvent += TaskPannelShow;

        ATMTaskTipFingerManager._TipHideEvent += TipHide;
        ATMFingerTip._FigerTipShowEvent += FigerTipShow;
        ATMFingerTip._KeyboardTipShowEvent += KeyboardShow;

    }
    void CardInRecord()
    {
        ATMFileIO.WriteToFilePerformance(DateTime.Now- _startTime, "CardIn");
    }
    void MoneyTaken()
    {
        ATMFileIO.WriteToFilePerformance(DateTime.Now- _startTime, "Take Money");
    }

    void TaskPannelShow()
    {
        ATMFileIO.WriteToFilePerformance(DateTime.Now - _startTime, "Task Panel Show");
    }
    void TaskPannelHide()
    {
        ATMFileIO.WriteToFilePerformance(DateTime.Now - _startTime, "Task Panel Hide");
    }
    void FigerTipShow()
    {
        ATMFileIO.WriteToFilePerformance(DateTime.Now - _startTime, "Finger Tip Show");
    }
    void KeyboardShow()
    {
        ATMFileIO.WriteToFilePerformance(DateTime.Now - _startTime, "KeyBoard Tip Show");
    }
    void TipHide()
    {
        ATMFileIO.WriteToFilePerformance(DateTime.Now - _startTime, "All Tip Hide");
    }
    void OnDestroy()
    {
        ATMRealCard._EventCardIn -= CardInRecord;
        ATMMaoyeye._EventMoneyBeenTaken -= MoneyTaken;

        ATMTaskPannelManager.PannelHideEvent -= TaskPannelHide;
        ATMTaskPannelManager.PannelShowEvent -= TaskPannelShow;

        ATMTaskTipFingerManager._TipHideEvent -= TipHide;
        ATMFingerTip._FigerTipShowEvent -= FigerTipShow;
        ATMFingerTip._KeyboardTipShowEvent -= KeyboardShow;

        ATMFileIO.WriteToFilePerformance( FinishTime,ATMTaskFromUI.TaskFromUI.ToString()+" Total Time");
        ATMFileIO.CloseFile();
        Instance = null;
    }
}
