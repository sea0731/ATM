using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ATMSelectSceneManager : MonoBehaviour {
    public Dropdown _dropDown;
    public Button _ExitBtn;
    public Button videoBtn;

    void Awake()
    {
        ATMTaskFromUI.IsTipon = true;
        //_ExitBtn.onClick.AddListener( ()=>
        //{
        //    GameDataManager.isMainUI = true;
        //    StartCoroutine(ScenesManager.BeginLoader("MainUI", true));
        //});
        //videoBtn.onClick.AddListener(VideoBtnClick);
    }

    public void SceneSelectBtnClick(int i)
    {         
        switch (i)
        {
            case 0:
                ATMTaskFromUI.TaskFromUI = ATMTASKTYPE.CHECK;
               
                break;
            case 1:
                ATMTaskFromUI.TaskFromUI = ATMTASKTYPE.DRAW;
                break;
            case 2:
                ATMTaskFromUI.TaskFromUI = ATMTASKTYPE.TRANSFER;
                break;
            default:
                break;
        }
        ATMConfig._level = i;
    }

    private void VideoBtnClick()
    {
        //CurrentGameData.currentGameNum = 2;
        SceneManager.LoadScene("VideoScene");
      
    }

    public void LoadMainScene()
    {
        SceneSelectBtnClick(_dropDown.value);
        SceneManager.LoadScene("ATMMain");
        //ATMConfig._id = GameDataManager._userID;
    }
    public void LoadTrainingScene()
    {
        SceneSelectBtnClick(_dropDown.value);
        SceneManager.LoadScene("ATMTraining");
    }
}
