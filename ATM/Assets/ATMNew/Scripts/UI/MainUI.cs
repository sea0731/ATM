using System.Collections;
using System.Collections.Generic;
using GameData;
using LabData;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameObject launcher;
    public Button startButton;
    public GameObject userEditor;
    public Dropdown userChooser;
    public Button newUserSettingButton;
    public Button userDeletingButton;
    public Dropdown taskEditor;
    public Button userCreatingButton;
    public Button returnButton;
    public InputField userName;
    public InputField userAccount;
    public InputField userPassword;
    public Text inputError;

    private Queue<ATMTask> taskQueue;

    private void Start()
    {
        LabTools.CreateDataFolder<UserCard>();
        launcher.SetActive(true);
        userEditor.SetActive(false);
        startButton.onClick.AddListener(StartButtonClick);
        newUserSettingButton.onClick.AddListener(NewUserSettingButtonClick);
        userDeletingButton.onClick.AddListener(UserDeletingButtonClick);
        userCreatingButton.onClick.AddListener(UserCreatingButtonClick);
        returnButton.onClick.AddListener(ReturnButtonClick);
        SetUserChooserList();
    }

    public void StartButtonClick()
    {
        UserCard userCard = LabTools.GetData<UserCard>(userChooser.captionText.text);

        taskQueue = CreateTaskQueue(SetTaskType());

        GameDataManager.FlowData = new GameFlowData("01", userCard, taskQueue);

        GameSceneManager.Instance.Change2MainScene();
    }

    public void NewUserSettingButtonClick()
    {
        launcher.SetActive(false);
        userEditor.SetActive(true);
        inputError.enabled = false;
    }

    public void UserDeletingButtonClick()
    {
        LabTools.DeleteData<UserCard>(userChooser.captionText.text);
        SetUserChooserList();

    }

    public void UserCreatingButtonClick()
    {
        UserCard userCard = new UserCard();

        CreateUser();
    }

    private void CreateUser()
    {
        if(userName.text != "" && userAccount.text != "" && userPassword.text != "")
        {
            UserCard userCard = new UserCard(userAccount.text, userPassword.text, 300000, userName.text);
            LabTools.WriteData(userCard, userName.text, true);

            SetUserChooserList();

            launcher.SetActive(true);
            userEditor.SetActive(false);
        }
        else
            inputError.enabled = true;
    }

    public void ReturnButtonClick()
    {
        launcher.SetActive(true);
        userEditor.SetActive(false);
    }

    public void SetUserChooserList()
    {
        userChooser.ClearOptions();
        List<string> userDataList = LabTools.GetDataName<UserCard>();
        if( userDataList != null)
        {
            userChooser.AddOptions(userDataList);
        }

        userChooser.value = 0;
    }

    private ATMTASKTYPE SetTaskType()
    {
        switch(taskEditor.value)
        {
            case 0:
                return ATMTASKTYPE.DRAW;
            case 1:
                return ATMTASKTYPE.TRANSFER;
            case 2:
                return ATMTASKTYPE.CHECK;
            default:
                return ATMTASKTYPE.DRAW;
        }
    }

    private Queue<ATMTask> CreateTaskQueue(ATMTASKTYPE type)
    {
        Queue<ATMTask> taskQueue = new Queue<ATMTask>();

        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.CARDIN, false, 0));
        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.PASSWORD, false, 0));
        taskQueue.Enqueue(new ATMTask(ATMTaskFromUI.TaskFromUI, false, ATMTaskInfoPool.Money[UnityEngine.Random.Range(0, ATMTaskInfoPool.Money.Length)]));
        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.CARDOUT, false, 0));
        taskQueue.Enqueue(new ATMTask(ATMTASKTYPE.FINISHED, false, 0));

        return taskQueue;
    }
}
