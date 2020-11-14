using UnityEngine;
using System.Collections;

public class ATMScreenManager : MonoBehaviour
{
    public static ATMScreenManager _Instance;
    private CardUser _CurrentCardUser = null;

    public static DelCard _EventCardOut;
    /// <summary>
    /// 插拔卡
    /// </summary>
    public CardUser CurrentCardUser
    {
        get
        {
            return _CurrentCardUser;
        }
        set
        {
            _CurrentCardUser = value;
        }
    }
    public GameObject[] Screens;
    private SCREENTYPE currentScreen;
    /// <summary>
    /// 转换屏幕状态
    /// </summary>
    public SCREENTYPE CurrentScreen
    {
        get
        {
            return currentScreen;
        }

        set
        {
            currentScreen = value;
            SetScreenByType(currentScreen);

            if (currentScreen == SCREENTYPE.WELCOME)
            {
                if (_CurrentCardUser != null)
                {
                    EjectCardUser();
                }
            }
        }
    }


    void Awake()
    {
        _Instance = this;
        CurrentScreen = SCREENTYPE.WELCOME;
    }

    public void SetCardUser(CardUser carduser)
    {
        if (CurrentCardUser != null)
        {
            Debug.LogError("请先退卡");
            return;
        }
        CurrentCardUser = carduser;
    }

    public void EjectCardUser()
    {
        CurrentCardUser = null;
        ATMCardslot._Hascard = false;
        ATMTaskManager._Instance.TaskDone(ATMTASKTYPE.CARDOUT);
        if (_EventCardOut != null)
        {
            _EventCardOut();
        }
       
    }

    public void SetScreenByType(SCREENTYPE screenType)
    {
        int index = (int)screenType;
        for (int i = 0; i < Screens.Length; i++)
        {
            Screens[i].SetActive(false);
        }
        Screens[index].SetActive(true);
    }

    public bool CheckPassWord(string password)
    {
        if (_CurrentCardUser == null)
        {
            CurrentScreen = SCREENTYPE.WELCOME;
            Debug.LogError("系统错误，请重新插卡");
            return false;
        }
        else
        {
            return _CurrentCardUser.CheckPassword(password);
        }
    }
}

public enum SCREENTYPE
{
    WELCOME,
    INPUTPASSWORD,
    USERPANEL,
    TRANSFER,
    TRANSFEROUT,
    TRANSFERIN,
    BALANCE
}
