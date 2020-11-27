using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;
using LabData;


namespace GameData
{
    public class GameFlowData : LabDataBase
    {
        /// <summary>
        /// 语言
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; } = "Test01";


        public UserCard userCard;

        public ATMTask task;
        /// <summary>
        /// FlowData 构造函数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="languageType"></param>
        /// <param name="remindType"></param>
        /// <param name="gameData"></param>
        public GameFlowData(string UserID, UserCard userCard, ATMTask task)
        {
            UserId = UserID;
            this.userCard = userCard;
            this.task = task;
        }

        public GameFlowData()
        {
        }
    }
}