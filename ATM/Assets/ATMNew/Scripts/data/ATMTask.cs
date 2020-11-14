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
        public static ATMTASKTYPE taskFromUI { get; set; }
        public static bool _isTipon { get; set; }

        public ATMTask()
        {
            taskFromUI = ATMTASKTYPE.EMPTY;
            _isTipon = true;
        }
        public ATMTask(ATMTASKTYPE taskType, bool tipon)
        {
            taskFromUI = taskType;
            _isTipon = tipon;
        }

    }
}

