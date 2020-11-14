using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ATMConfig
{
    public static int _PassWordLength = 6;
    public static int _AccountLength = 19;
    public static int _BalanceLength = 20;
    public static int _MoneyLength = 9;
    public static float _tipTime = 10f;
    public static string _id = "0";
    public static int _level;
}

public static class ATMTaskFromUI
{
    private static ATMTASKTYPE taskFromUI;
    private static bool _isTipon;

    public static bool IsTipon
    {
        get
        {
            return _isTipon;
        }

        set
        {
            _isTipon = value;
        }
    }

    public static ATMTASKTYPE TaskFromUI
    {
        get
        {
            return taskFromUI;
        }

        set
        {
            taskFromUI = value;
        }
    }
}

public static class CardUserManager
{
    public static CardUser _CardUser0 = new CardUser("6227888877776666556", 123456, 300000, "思欣跃");
    public static CardUser _CardUser1 = new CardUser("6227888877776666555", 123456, 300000, "康费德");
    //public static CardUser _CardUser2 = new CardUser("789", 123456, 300000, "王五");
    private static Dictionary<string, CardUser> _CardUserDic;

    private static Dictionary<string, CardUser> GetDic()
    {
        if (_CardUserDic == null)
        {
            _CardUserDic = new Dictionary<string, CardUser>();
            AddAllCardUserInDic();

        }
        return _CardUserDic;
    }

    private static void AddAllCardUserInDic()
    {
        _CardUserDic.Add(_CardUser0.UserID, _CardUser0);
        _CardUserDic.Add(_CardUser1.UserID, _CardUser1);
       // _CardUserDic.Add(_CardUser2.UserID, _CardUser2);
    }

    public static string CheckUserNameByID(string id)
    {
        string name = "";
        if (GetDic().ContainsKey(id))
        {
            name = GetDic()[id].Name;
        }
        return name;
    }

    public static int CheckUserBalanceByID(string id)
    {
        int balance = 0;
        if (GetDic().ContainsKey(id))
        {
            balance = GetDic()[id].Balance;
        }
        return balance;
    }

    public static bool TransferInCard(string id, int money)
    {
        if (CheckUserNameByID(id) != "")
        {
            _CardUserDic[id].TransferIn(money);
            return true;
        }
        return false;
    }

    public static int TransferOutByID(string id, int money)
    {
        if (CheckUserNameByID(id) != "")
        {
            return _CardUserDic[id].TransferOut(money);
        }
        return 0;
    }

}

public class CardUser
{
    private string _UserID;
    private int _PassWord;
    private int _Balance;
    private string _Name;

    public string UserID
    {
        get
        {
            return _UserID;
        }

        set
        {
            _UserID = value;
        }
    }

    public string Name
    {
        get
        {
            return _Name;
        }

        private set
        {
            _Name = value;
        }
    }

    public int Balance
    {
        get
        {
            return _Balance;
        }

        private set
        {
            _Balance = value;
        }
    }

    public int PassWord
    {
        get
        {
            return _PassWord;
        }

        set
        {
            _PassWord = value;
        }
    }

    public CardUser(string id, int password, int balance, string name)
    {
        UserID = id;
        PassWord = password;
        Balance = balance;
        Name = name;
    }

    public int TransferOut(int money)
    {
        if (money <= Balance)
        {
            Balance -= money;
            return money;
        }
        else
        {
            return 0;
        }
    }

    public void TransferIn(int money)
    {
        Balance += money;
    }

    public bool CheckPassword(string password)
    {
        if (PassWord + "" == password)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
