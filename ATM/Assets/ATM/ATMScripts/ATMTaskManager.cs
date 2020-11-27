using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using System.Text;


public class ATMTaskManager : MonoBehaviour {

    public static ATMTaskManager _Instance;
    private static Queue<ATMTASKTYPE> _taskQueue;
    private static ATMTASKTYPE _currentTaskType;

    private int _taskMoney;
    private string _taskPanelText;
    public static ATMTASKTYPE CurrentTaskType
    {
        get
        {
            return _currentTaskType;
        }

    }
    // Use this for initialization
    void Awake () {
        _Instance = this;
        //初始化任务队列
        InitListQueue();
        //初始化任务中的信息
        InitTaskInfo();
      
    }
    IEnumerator Start()
    {
        SetCurrentTaskType();
        InitTaskText();
        yield return StartCoroutine(ShowTask());

    }


    private IEnumerator ShowTask()
    {
        ATMTrainingPanelTextManager._Instance.AddText(_taskPanelText);
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
        yield return new WaitForSeconds(5);
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
        //StartCoroutine(ATMFingerTip._instance.TimerCount());
        ATMFingerTip._instance._isOpen = ATMTaskFromUI.IsTipon;
        ATMRealCard.Instance._switch = true;
    }

    private void InitTaskText()
    {
        string s = "";
        s += "密码为\n<color=red>" + CardUserManager._CardUser0.PassWord + "</color>\n";
        switch (ATMTaskFromUI.TaskFromUI)
        {
            case ATMTASKTYPE.CHECK:
                s += "请<color=red>查询</color>余额";
                break;
            case ATMTASKTYPE.TRANSFER:
                s += "请向账户\n<color=red>" + CardUserManager._CardUser0.UserID + "</color>\n转账\n<color=red>" + _taskMoney + "</color>元";
                break;
            case ATMTASKTYPE.DRAW:
                s+= "请取款\n<color=red>" + _taskMoney + "</color>元";
                break;
            default:
                s+=  "金额\n<color=red>" + _taskMoney + "</color>元\n" + "账户<color=red>\n" + CardUserManager._CardUser1.UserID+ "</color>";
                break;
        }
        _taskPanelText = s;
        ATMTaskPannelManager._Instance.SetTaskText(s);
    }

    // Update is called once per frame
    void OnDestroy () {
        _Instance = null;
	}
    private void InitTaskInfo()
    {
        CardUserManager._CardUser0.PassWord = ATMTaskInfoPool.Password[UnityEngine.Random.Range(0, ATMTaskInfoPool.Password.Length)];
        _taskMoney = ATMTaskInfoPool.Money[UnityEngine.Random.Range(0, ATMTaskInfoPool.Money.Length)];
    }

    private void InitListQueue()
    {
        _taskQueue = new Queue<ATMTASKTYPE>();
        AddTaskToTaskQueue();
    }

    private void AddTaskToTaskQueue()
    {
        if (ATMTaskFromUI.TaskFromUI != ATMTASKTYPE.EMPTY)
        {
            _taskQueue.Enqueue(ATMTASKTYPE.CARDIN);
            _taskQueue.Enqueue(ATMTASKTYPE.PASSWORD);
            _taskQueue.Enqueue(ATMTaskFromUI.TaskFromUI);
            _taskQueue.Enqueue(ATMTASKTYPE.CARDOUT);
            _taskQueue.Enqueue(ATMTASKTYPE.FINISHED);

        }
        _taskQueue.Enqueue(ATMTASKTYPE.EMPTY);

    }

    void SetCurrentTaskType()
    {
        _currentTaskType = _taskQueue.Peek();
        //SetTaskText();
    }

    private void SetTaskText()
    {

        ATMTaskPannelManager._Instance.SetTaskText(TaskTextString(_currentTaskType));
    }

    private string TaskTextString(ATMTASKTYPE _type)
    {
        switch (_type)
        {
            case ATMTASKTYPE.CARDIN:
                return "请插卡";
            case ATMTASKTYPE.PASSWORD:
                return "请输入密码\n" + CardUserManager._CardUser0.PassWord;
            case ATMTASKTYPE.CHECK:
                return "请查询余额";
            case ATMTASKTYPE.DRAW:
                return "请取款\n" + _taskMoney + "元";
            case ATMTASKTYPE.TRANSFER:
                return "请向账户\n" + CardUserManager._CardUser0.UserID + "\n转账\n" + _taskMoney + "元";
            case ATMTASKTYPE.FINISHED:
                return "恭喜你已完成所有任务！";
            case ATMTASKTYPE.CARDOUT:
                return "请退出卡";
            default:
                return "密码\n" + CardUserManager._CardUser0.PassWord + "\n金额\n" + _taskMoney + "元\n"+"账户\n" + CardUserManager._CardUser1.UserID;
        }
    }

    internal bool CheckMoney(int inputmoney)
    {
        return _taskMoney == inputmoney;
    }

   
    public void TaskDone(ATMTASKTYPE _type)
    {
        if (_taskQueue.Peek() != ATMTASKTYPE.EMPTY && _taskQueue.Peek() != ATMTASKTYPE.FINISHED && _taskQueue.Peek() == _type)
        {
            Debug.Log("Done:" + _taskQueue.Peek());
            _taskQueue.Dequeue();
            Debug.Log("Next:" + _taskQueue.Peek());
            SetCurrentTaskType();
        }
        else
            SetCurrentTaskType();
        if(_taskQueue.Peek() == ATMTASKTYPE.FINISHED)
        {
            StartCoroutine(TaskOver());

        }

    }

    private IEnumerator TaskOver()
    {
        ATMFingerTip._instance.Close();
       // ATMTaskTipFingerManager._Instance.gameObject.SetActive(false);      
        ATMFileIO.WriteToFilePerformance(DateTime.Now - ATMDataManager.Instance.GetStartTime(), "Task Finished");
       
        ATMDataManager.Instance.FinishTime = DateTime.Now- ATMDataManager.Instance.GetStartTime();
       // InsertToDatabase();

        InitEndString();

        ATMTrainingPanelTextManager._Instance.AddText("恭喜您已完成所有任务\n\n"+"任务时间:<color=red>" + ATMDataManager.Instance.FinishTime.Minutes+ "分钟" + ATMDataManager.Instance.FinishTime.Seconds+ "秒</color>");
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
        yield return new WaitForSeconds(3);

       // DataTable dt = DatabaseManager.Instance.atmDatabaseOperation.GetAllGameData(Convert.ToInt32(ATMConfig._id), ATMConfig._level);
       // List<float> playTimeList = new List<float>();
        //List<float> performanceList = new List<float>();

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    playTimeList.Add(float.Parse(dt.Rows[i]["PlayTime"].ToString())/60f);
        //    //performanceList.Add(float.Parse(dt.Rows[i]["Performance"].ToString()));
        //}
       // ATMEndUI.instance.SetTimePoint(playTimeList, "PlayTime", 4f);
        ATMEndUI.instance.ShowEnd();

        //StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());

        //GraphManager.Instance.Show(playTimeList, performanceList);
        //yield return new WaitForSeconds(6);
        //ReturnToSelectScene();

    }

    public void ReturnToSelectScene()
    {
        SceneManager.LoadScene("ATMSelection");
    }
    private void InsertToDatabase()
    {
        //DatabaseManager.Instance.atmDatabaseOperation.InsertGameData(new CognitiveLeapGameDataBase(Convert.ToInt32(ATMConfig._id), DateTime.Now, ATMConfig._level, Convert.ToSingle( ATMDataManager.Instance.FinishTime.TotalSeconds), 1));
    }

    private void InitEndString()
    {
        StringBuilder str = new StringBuilder();
        str.Append("完成任务总时间为：\n\n<color=red>").Append(ATMDataManager.Instance.FinishTime.Minutes.ToString()).Append("分钟").Append(ATMDataManager.Instance.FinishTime.Seconds.ToString()).Append("秒</color>");

        ATMEndUI.instance.SetMainInfo(str.ToString());
        ATMEndUI.instance.SetTitleInfo("恭喜您完成全部任务");
    }

}

public static class ATMTaskInfoPool
{
    public static int[] Password = new int[] { 123456, 985608, 153256, 236754, 978435, 345433 };
    public static int[] Money = new int[] { 1000, 3000, 100, 500, 5000, 10000 };
}

