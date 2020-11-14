using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMScreenTransferOutThird : MonoBehaviour,IATMTipScreen
{
    public Button _BtnReturn;
    public Button _BtnGoOn;
    public Text _ShowMessage;
    public GameObject _maoyeye;
    public const ATMTASKTYPE TASKTYPE = ATMTASKTYPE.DRAW;
    void Awake()
    {
        BtnRegister();
    }
    void OnEnable()
    {
        SetTip();
    }
    public void SetTip()
    {
        if(ATMMaoyeye._IsShow)
            SetTip(_maoyeye.transform, false);
        else if (ATMTaskManager.CurrentTaskType != TASKTYPE)
            SetTip(_BtnReturn.transform, false);
        else
            SetTip(_BtnGoOn.transform, false);
    }
    void BtnRegister()
    {
        _BtnReturn.onClick.AddListener(OnReturnClick);
        _BtnGoOn.onClick.AddListener(OnBtnGoOnClick);
    }
    private void SetTip(Transform go, bool keyboardEffect)
    {
        ATMFingerTip._instance.GoTip = go;
        ATMFingerTip._instance.KeyboardEffect = keyboardEffect;
    }
    void OnReturnClick()
    {
        if (ATMMaoyeye._IsShow)
        {
            StartCoroutine(ShowMessageIE());
            return;
        }
        ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.USERPANEL;

    }

    void OnBtnGoOnClick()
    {
        if (ATMMaoyeye._IsShow)
        {
            StartCoroutine(ShowMessageIE());
            return;
        }
        ATMScreenTransferOut._Instance.SetGOActive(0);
    }


    IEnumerator ShowMessageIE()
    {
        _ShowMessage.text = "请先取走你的钱";
        yield return new WaitForSeconds(2f);
        _ShowMessage.text = "";
    }
}
