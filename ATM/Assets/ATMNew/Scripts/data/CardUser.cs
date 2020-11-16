using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataSync;

namespace LabData
{
    public class  UserCard: LabDataBase
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }
        public string Name { get; set; }

        public UserCard()
        {
            UserID = "6227888877776666556";
            Password = ATMTaskInfoPool.Password[UnityEngine.Random.Range(0, ATMTaskInfoPool.Password.Length)];
            Balance = 300000;
            Name = "王小明";

        }
        public UserCard(string id, string password, int balance, string name)
        {
            UserID = id;
            Password = password;
            Balance = balance;
            Name = name;

        }

    }
   
}

