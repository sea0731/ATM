using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ATMTrainingSecondPanelManager : MonoBehaviour {
    public static ATMTrainingSecondPanelManager Instance;

    public GameObject _TaskPanelGo;
    public GameObject _CardInGo;
    public GameObject _MoneyOutGo;
    private bool _isContinue = false;
    private Queue<IEnumerator> _SecondPanelAction;
    void Awake()
    {
        Instance = this;
        //_panels = new List<Image>();
        _SecondPanelAction = new Queue<IEnumerator>();
        _SecondPanelAction.Enqueue(TaskPanelTip());
        _SecondPanelAction.Enqueue(CardInTip());
        _SecondPanelAction.Enqueue(MoneyOutTip());

    }

  

    void Start () {
	
	}
	public void Continue()
    {
        _isContinue = true;
        ATMTaskTipFingerManager._Instance.HideTipFinger();
    }
    IEnumerator TaskPanelTip()
    {
        _TaskPanelGo.SetActive(true);
        foreach (Transform childImage in _TaskPanelGo.transform)
        {
            if (childImage.GetComponent<Image>() != null)
            {
                childImage.GetComponent<Image>().CrossFadeAlpha(0, 0f, true);
                childImage.GetComponent<Image>().CrossFadeAlpha(1f, 2, true);
            }
        }
       // yield return new WaitForSeconds(2);
        ATMTaskTipFingerManager._Instance.ShowTipFinger(ATMTaskPannelManager._Instance.GetTaskBtn().transform);

        while (!_isContinue)
        {
            yield return new WaitForEndOfFrame();
        }
        _TaskPanelGo.transform.Find("Text").GetComponent<Text>().text = "";
        _isContinue = false;
        yield return new WaitForSeconds(5);

        foreach (Transform childImage in _TaskPanelGo.transform)
        {
            if (childImage.GetComponent<Image>() != null)
            {
                childImage.GetComponent<Image>().CrossFadeAlpha(0, 0.5f, true);
            }
        }
        yield return new WaitForSeconds(2);
        ATMTaskPannelManager._Instance.HideTaskPanel();
        Destroy(_TaskPanelGo);
        ATMTrainingPanelTextManager._Instance.AddText("恭喜您已完成\n<color=red> 查看任务</color> 练习\n接下来是\n <color=red>插卡</color> 练习");
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
        
        yield return new WaitForSeconds(ATMAudioManager.PlayEF("ATM_finish1"));
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
    }


    private IEnumerator CardInTip()
    {
        _isContinue = false;
        _CardInGo.SetActive(true);
        foreach (Transform childImage in _CardInGo.transform)
        {
            if (childImage.GetComponent<Image>() != null)
            {
                childImage.GetComponent<Image>().CrossFadeAlpha(0, 0f, true);
                childImage.GetComponent<Image>().CrossFadeAlpha(1f, 2, true);
            }               
        }
        //yield return new WaitForSeconds(2);
        ATMTaskTipFingerManager._Instance.ShowTipFinger(ATMRealCard.Instance.GetCardGo().transform);
        ATMRealCard.Instance._switch = true;
        ATMRealCard._EventCardIn += Continue;
        while (!_isContinue)
        {
            yield return new WaitForEndOfFrame();
        }
        _CardInGo.transform.Find("Text").GetComponent<Text>().text = "";
        _isContinue = false;
        yield return new WaitForSeconds(5);
        foreach (Transform childImage in _CardInGo.transform)
        {
            if (childImage.GetComponent<Image>() != null)
                childImage.GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, true);
        }
        yield return new WaitForSeconds(2);
        Destroy(_CardInGo);
        ATMTrainingPanelTextManager._Instance.AddText("恭喜您已完成\n<color=red> 插卡 </color>练习\n接下来是\n <color=red>取钱</color> 练习");
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
       
        yield return new WaitForSeconds(ATMAudioManager.PlayEF("ATM_finish2"));
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
       
    }

    private IEnumerator MoneyOutTip()
    {
        ATMScreenManager._Instance.SetScreenByType(SCREENTYPE.TRANSFEROUT);
        _MoneyOutGo.SetActive(true);
        foreach (Transform childImage in _MoneyOutGo.transform)
        {
            if (childImage.GetComponent<Image>() != null)
            {
                childImage.GetComponent<Image>().CrossFadeAlpha(0, 0f, true);
                childImage.GetComponent<Image>().CrossFadeAlpha(1f, 2, true);
            }
        }
        ATMTrainningTransferOut._Instance.SendMoneyOut();
        ATMMaoyeye._EventMoneyOut += () =>
         ATMTaskTipFingerManager._Instance.ShowTipFinger(ATMTrainingBankNote.Instance.GetMoneyGo().transform);
        ATMMaoyeye._EventMoneyBeenTaken += Continue;
        while (!_isContinue)
        {
            yield return new WaitForEndOfFrame();
        }
        _MoneyOutGo.transform.Find("Text").GetComponent<Text>().text = "";
        _isContinue = false;
        yield return new WaitForSeconds(5);
        foreach (Transform childImage in _MoneyOutGo.transform)
        {
            if (childImage.GetComponent<Image>() != null)
                childImage.GetComponent<Image>().CrossFadeAlpha(0f, 2, true);
        }
        yield return new WaitForSeconds(3);
        Destroy(_MoneyOutGo);
        ATMTrainingPanelTextManager._Instance.AddText("恭喜您已完成所有练习\n接下来将返回主界面");
        yield return StartCoroutine(ATMTrainingPanelTextManager._Instance.NextText());
       
        yield return new WaitForSeconds(ATMAudioManager.PlayEF("ATM_finish3"));
    }


    public IEnumerator NextTip()
    {
        if (_SecondPanelAction.Count > 0)
        {
            yield return StartCoroutine( _SecondPanelAction.Dequeue());
        }
        yield return null;
    }
	
    void OnDestroy()
    {
        Instance = null;
    }
}
