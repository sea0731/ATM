using DataSync;

namespace GameData
{
    public class GobalData : LabDataBase
    {     

        /// <summary>
        /// 游戏的主UI场景名
        /// </summary>
        public const string MainUiScene = "MainUI";
        public const string MainScene = "ATMMainScene";
        
        public const int GameUIManagerWeight = 0;
        public const int GameEntityManagerWeight = 30;
        public const int GameSceneManagerWeight = 50;
        public const int GameTaskManagerWeight = 80;
        public const int GameDataManagerWeight = 100;

        public const int _PassWordLength = 6;
        public const int _AccountLength = 19;
        public const int _BalanceLength = 20;
        public const int _MoneyLength = 9;
        public const float _tipTime = 10f;
    }

}

