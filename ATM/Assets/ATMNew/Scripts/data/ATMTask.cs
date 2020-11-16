using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;
public enum ATMTASKTYPE
{
    EMPTY,
    CARDIN,
    PASSWORD,
    TRANSFER,
    DRAW,
    CHECK,
    CARDOUT,
    FINISHED
}

namespace LabData
{
    public class ATMTask : LabDataBase
    {
        public ATMTASKTYPE taskFromUI { get; set; }
        public bool _isTipon { get; set; }
        public int money { get; set; } 

        public ATMTask()
        {
            taskFromUI = ATMTASKTYPE.EMPTY;
            _isTipon = true;
        }
        public ATMTask(ATMTASKTYPE taskType, bool tipon, int money)
        {
            taskFromUI = taskType;
            _isTipon = tipon;
            this.money = money;
        }

    }

    public static class ATMTaskInfoPool
    {
        public static string[] Password = new string[] { "123456", "985608", "153256", "236754", "978435", "345433" };
        public static int[] Money = new int[] { 1000, 3000, 100, 500, 5000, 10000 };
    }
}

