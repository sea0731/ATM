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
}
