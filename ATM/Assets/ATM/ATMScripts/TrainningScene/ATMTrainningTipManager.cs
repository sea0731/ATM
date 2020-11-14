using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class ATMTrainningTipManager : MonoBehaviour {

    public Queue<Action> _guideProgress;

    // Use this for initialization
    void Start () {
        _guideProgress = new Queue<Action>();
        _guideProgress.Enqueue(() => StartCoroutine(Step1()));
        _guideProgress.Enqueue(() => StartCoroutine(Step2()));
        _guideProgress.Enqueue(() => StartCoroutine(Step3()));
        ExecuteNextStep();

    }
	
    IEnumerator Step1()
    {
        yield return null;
        Debug.Log("Step1");
        ATMTrainingPanelTextManager._Instance.AddText("欢迎来到ATM练习模式");
        ATMAudioManager.PlayEF("ATM_Welcome");
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
        yield return new WaitForSeconds(2);      
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
        yield return StartCoroutine(SetCallBack(() => ExecuteNextStep()));
    }
    IEnumerator Step2()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Step2");
        ATMAudioManager.PlayEF("ATM_taskPanel");
        yield return StartCoroutine(ATMTrainingSecondPanelManager.Instance.NextTip());
        ATMAudioManager.PlayEF("ATM_cardIn");
       yield return StartCoroutine(ATMTrainingSecondPanelManager.Instance.NextTip());
        ATMAudioManager.PlayEF("ATM_takeMoney");
        yield return StartCoroutine(ATMTrainingSecondPanelManager.Instance.NextTip());
        yield return StartCoroutine(ATMTrainingSecondPanelManager.Instance.NextTip());
        yield return StartCoroutine(SetCallBack(() => ExecuteNextStep()));
    }
    IEnumerator Step3()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Step3");
        ReturnToSelectScene();
    }
    bool ExecuteNextStep()
    {
        if (_guideProgress.Count > 0)
        {
            _guideProgress.Dequeue()();
            return true;
        }
        return false;
    }
   
    IEnumerator SetCallBack(Action act)
    {
        yield return 1;
        act();
    }

    public void ReturnToSelectScene()
    {
        SceneManager.LoadScene("ATMSelection");
    }

}
