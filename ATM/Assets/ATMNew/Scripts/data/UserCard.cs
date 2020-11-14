using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;

namespace LabData
{
    public class  CardUser: LabDataBase
    {
        public string UserID { get; set; }
        public int Password { get; set; }
        public int Balance { get; set; }
        public string Name { get; set; }

        public CardUser()
        {
            UserID = "6227888877776666556";
            Password = ATMTaskInfoPool1.Password[UnityEngine.Random.Range(0, ATMTaskInfoPool.Password.Length)];
            Balance = 300000;
            Name = "王小明";

        }
        public CardUser(string id, int password, int balance, string name)
        {
            UserID = id;
            Password = password;
            Balance = balance;
            Name = name;

        }

    }
    public static class ATMTaskInfoPool1
    {
        public static int[] Password = new int[] { 123456, 985608, 153256, 236754, 978435, 345433 };
        public static int[] Money = new int[] { 1000, 3000, 100, 500, 5000, 10000 };
    }
}

