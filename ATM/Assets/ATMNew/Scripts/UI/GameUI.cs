using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LabData;
using GameData;
using System;
using System.Text;
using I2.Loc;

public class GameUI : MonoBehaviour
{
    public static GameUI _instance;

    private static Queue<ATMTask> taskQueue;

    private static ATMTask mainTask;

    private static string mainTaskText;

    public static ATMTask currentTask;


    public Text text;
    private void Awake()
    {
        _instance = this;
        mainTask = GameDataManager.FlowData.task;
        taskQueue = CreateTaskQueue();
        currentTask = taskQueue.Peek();
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        mainTaskText = TaskText();
        yield return StartCoroutine(ShowTask());

        string pannelText = TaskPannelTextString();
        ATMTaskPannelManager._Instance.SetTaskText(pannelText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Queue<ATMTask> CreateTaskQueue()
    {
        Queue<ATMTask> taskQueue = new Queue<ATMTask>();

        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.CARDIN, false, 0));
        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.PASSWORD, false, 0));
        taskQueue.Enqueue(mainTask);
        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.CARDOUT, false, 0));
        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.FINISHED, false, 0));

        return taskQueue;
    }

    private string TaskText()
    {
        string taskText = "";
        taskText +=  ScriptLocalization.TaskTextPassword + GameDataManager.FlowData.userCard.Password + "</color>\n";
        switch (mainTask.taskFromUI)
        {
            case ATMTASKTYPE.CHECK:
                taskText += ScriptLocalization.TaskTextCheck;
                break;
            case ATMTASKTYPE.TRANSFER:
                taskText += ScriptLocalization.TaskTextTransfer1 + CardUserManager._CardUser0.UserID + ScriptLocalization.TaskTextTransfer2 + mainTask.money + ScriptLocalization.TaskTextDollars;
                break;
            case ATMTASKTYPE.DRAW:
                taskText += ScriptLocalization.TaskTextDraw + mainTask.money + ScriptLocalization.TaskTextDollars;
                break;
            default:
                taskText += ScriptLocalization.TaskTextDraw + mainTask.money + ScriptLocalization.TaskTextDollars;
                break;
        }

        return taskText;
    }

    private string TaskPannelTextString()
    {
        switch (currentTask.taskFromUI)
        {
            case ATMTASKTYPE.CARDIN:
                return ScriptLocalization.TaskTipCardIn;
            case ATMTASKTYPE.PASSWORD:
                return ScriptLocalization.TaskTipPassword + GameDataManager.FlowData.userCard.Password;
            case ATMTASKTYPE.CHECK:
                return ScriptLocalization.TaskTipCheck;
            case ATMTASKTYPE.DRAW:
                return ScriptLocalization.TaskTipDraw + GameDataManager.FlowData.task.money + ScriptLocalization.TaskTextDollars;
            case ATMTASKTYPE.TRANSFER:
                return ScriptLocalization.TaskTipTransfer1 + GameDataManager.FlowData.task.targetID + ScriptLocalization.TaskTipTransfer2 + mainTask.money + ScriptLocalization.TaskTextDollars;
            case ATMTASKTYPE.FINISHED:
                return ScriptLocalization.TaskTipFinished;
            case ATMTASKTYPE.CARDOUT:
                return ScriptLocalization.TaskTipCardOut;
            default:
                return ScriptLocalization.TaskTipDraw + GameDataManager.FlowData.task.money + ScriptLocalization.TaskTextDollars;
        }
    }

    private IEnumerator ShowTask()
    {
        //ATMTrainingPanelTextManager._Instance.AddText(mainTaskText);
        Debug.Log("mainTaskText=" + mainTaskText);
        yield return StartCoroutine(ShowText(false));
        yield return new WaitForSeconds(5);
        yield return StartCoroutine(ShowText(true));
        //StartCoroutine(ATMFingerTip._instance.TimerCount());
        ATMFingerTip._instance._isOpen = ATMTaskFromUI.IsTipon;
        ATMRealCard.Instance._switch = true;
    }

    public IEnumerator ShowText(bool isShowed)
    {
        if (!isShowed)
        {
            GetComponent<Image>().raycastTarget = true;
            GetComponent<Image>().CrossFadeAlpha(1, 1f, true);
            Debug.Log("mainTaskText=" + mainTaskText);
            (text as Graphic).CrossFadeAlpha(0, 1, true);
            yield return new WaitForSeconds(1);
            text.text = mainTaskText;
            (text as Graphic).CrossFadeAlpha(1, 1, true);
            yield return new WaitForSeconds(1);
        }
        else
        {
            text.text = "";
            (GetComponent<Image>() as Graphic).CrossFadeAlpha(0, 2f, true);
            yield return new WaitForSeconds(2);
            GetComponent<Image>().raycastTarget = false;
        }

        yield return null;
    }

    public void TaskDone(ATMTASKTYPE _type)
    {
        if (taskQueue.Peek().taskFromUI != ATMTASKTYPE.EMPTY && taskQueue.Peek().taskFromUI != ATMTASKTYPE.FINISHED && taskQueue.Peek().taskFromUI == _type)
        {
            Debug.Log("Done:" + taskQueue.Peek().taskFromUI);
            taskQueue.Dequeue();
            Debug.Log("Next:" + taskQueue.Peek().taskFromUI);
            currentTask = taskQueue.Peek();
        }
        else
            currentTask = taskQueue.Peek();
        if (taskQueue.Peek().taskFromUI == ATMTASKTYPE.FINISHED)
        {
            StartCoroutine(TaskOver());
        }

        string pannelText = TaskPannelTextString();
        ATMTaskPannelManager._Instance.SetTaskText(pannelText);

    }

    private IEnumerator TaskOver()
    {
        ATMFingerTip._instance.Close();
        // ATMTaskTipFingerManager._Instance.gameObject.SetActive(false);      
        ATMFileIO.WriteToFilePerformance(DateTime.Now - ATMDataManager.Instance.GetStartTime(), "Task Finished");

        ATMDataManager.Instance.FinishTime = DateTime.Now - ATMDataManager.Instance.GetStartTime();
        StringBuilder str = new StringBuilder();
        str.Append(ScriptLocalization.TaskFinishTime).Append(ATMDataManager.Instance.FinishTime.Minutes.ToString()).Append(ScriptLocalization.Minutes).Append(ATMDataManager.Instance.FinishTime.Seconds.ToString()).Append(ScriptLocalization.Seconds);

        ATMEndUI.instance.SetMainInfo(str.ToString());
        ATMEndUI.instance.SetTitleInfo(ScriptLocalization.TaskFinish);

        ATMTrainingPanelTextManager._Instance.AddText(ScriptLocalization.TaskFinish + ScriptLocalization.TaskFinishTime + ATMDataManager.Instance.FinishTime.Minutes + ScriptLocalization.Minutes + ATMDataManager.Instance.FinishTime.Seconds + ScriptLocalization.Seconds);
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
        yield return new WaitForSeconds(3);

       
        ATMEndUI.instance.ShowEnd();

    }
}
