using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMScreenWelcom : MonoBehaviour, IATMTipScreen
{
    public Button _BtnHelp;
    public Button _Task;
    public GameObject TipGameObject;
    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.CARDIN;

    private bool _isFirstRun = true;
    void Start()
    {
        SetTip();
    }
    void OnEnable()
    {
        if (!_isFirstRun)
            SetTip();
    }

    public void OnBtnHelpClick()
    {
        print("  show help");
    }

    public void OnBtnTaskClick()
    {
        print("  show task");
    }
    public void SetTip()
    {
        if ( ATMTaskManager.CurrentTaskType!=ATMTASKTYPE.EMPTY && ATMTaskManager.CurrentTaskType!=ATMTASKTYPE.FINISHED)
        {
            ATMFingerTip._instance.SetTip(TipGameObject.transform);
        }
        _isFirstRun = false;
    }
}
