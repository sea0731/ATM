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
        public string targetID { get; set; }
        public string targetName { get; set; }

        public ATMTask()
        {
            taskFromUI = ATMTASKTYPE.EMPTY;
            _isTipon = true;
        }
        public ATMTask(ATMTASKTYPE taskType, bool tipon, int money)
        {
            taskFromUI = taskType;

            if (taskFromUI == ATMTASKTYPE.TRANSFER) setTarget();

            _isTipon = tipon;
            this.money = money;
        }

        private void setTarget()
        {
            targetID = ATMTaskInfoPool.UserID[UnityEngine.Random.Range(0, ATMTaskInfoPool.UserID.Length)];
            targetName = ATMTaskInfoPool.UserName[UnityEngine.Random.Range(0, ATMTaskInfoPool.UserName.Length)];
        }

    }

    public static class ATMTaskInfoPool
    {
        public static string[] Password = new string[] { "123456", "985608", "153256", "236754", "978435", "345433" };
        public static int[] Money = new int[] { 1000, 3000, 100, 500, 5000, 10000 };
        public static string[] UserID = new string[] { "6227888877776666556", "6231889977772266556", "5627897877306666556", "6227508235076642856", "6265748877723566546" };
        public static string[] UserName = new string[] { "王志明", "黃春嬌", "李四", "張三", "林霖戚", "鄧雨婷" };
    }
}

