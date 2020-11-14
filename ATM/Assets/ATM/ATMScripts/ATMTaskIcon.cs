using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMTaskIcon : MonoBehaviour
{
    public int _IDTask;
    public string _NameTask;
    public Text _TipTaskValue;


    public Image _TipTask;
    private bool isTaskDone = false;

    public bool IsTaskDone
    {
        get
        {

            return isTaskDone;
        }

        set
        {
            isTaskDone = value;
            if (isTaskDone)
            {
                _TipTask.gameObject.SetActive(true);
            }
            else
            {
                _TipTask.gameObject.SetActive(false);
            }
        }
    }
    void OnEnable()
    {
        IsTaskDone = false;
    }
    //public TASKTYPE _TaskType;
    //public int _currentCount = 0;
    //public int _TargetCount;
    //public bool _IsTaskSet = false;

    //public void SetTask(TASKTYPE taskType, int targetCount)
    //{
    //    _TaskType = taskType;
    //    _TargetCount = targetCount;
    //    _IsTaskSet = true;
    //}

    //public void CheckIsDone(int doneCount)
    //{
    //    if (_IsTaskSet == false)
    //    {
    //        Debug.LogError("Task hadn't been set");
    //    }

    //    if (_currentCount >= _TargetCount)
    //    {
    //        IsTaskDone = true;
    //    }
    //}

}

