using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMCardslot : MonoBehaviour
{
    public static bool _Hascard = false;
    //public Button _BtnCardChosen;
    //public Image _CardPanel;
    //public Image _CardSlot;

    //void Awake()
    //{
    //    _BtnCardChosen.onClick.AddListener(OnBtnCardChosenClick);
    //}

    void OnEnable()
    {
        ATMRealCard._EventCardIn += CardIn;
    }

    void OnDisable()
    {
        ATMRealCard._EventCardIn -= CardIn;
        _Hascard = false;
    }

    //public void OnBtnCardChosenClick()
    //{

    //}

    void CardIn()
    {
        if (_Hascard == false)
        {
            StartCoroutine(CardInIE());
            _Hascard = true;
        }
    }
    void CardInFinished()
    {
      
        ATMAudioManager.PlayEF("PassWordInput");
        ATMScreenManager._Instance.SetCardUser(CardUserManager._CardUser0);
        ATMTaskManager._Instance.TaskDone(ATMTASKTYPE.CARDIN);
        ATMScreenManager._Instance.SetScreenByType(SCREENTYPE.INPUTPASSWORD);
    }

    IEnumerator CardInIE()
    {
        float waitlength = ATMAudioManager.PlayEF("CardIn");
      yield return new WaitForSeconds(waitlength);
      
        CardInFinished();
    }
}
