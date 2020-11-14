using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMScreenTransferIn : MonoBehaviour
{
    public Button _BtnConfirm;
    public Button _BtnReturn;
    public Text _TextTransferInValue;

    void Awake()
    {
        BtnRegister();
    }

    void OnEnable()
    {
        _TextTransferInValue.text = "";
    }

    void BtnRegister()
    {
        _BtnConfirm.onClick.AddListener(OnBtnConfirmClick);
        _BtnReturn.onClick.AddListener(OnBtnReturnClick);
    }


    void OnBtnConfirmClick()
    {
        if (_TextTransferInValue.text == "")
        {
            return;
        }
        else
        {
            int money = int.Parse(_TextTransferInValue.text);
            if (CardUserManager.TransferInCard(ATMScreenManager._Instance.CurrentCardUser.UserID, money))
            {
                Debug.LogError("存钱成功");
                _TextTransferInValue.text = 0 + "";
            }
            else
            {
                Debug.LogError("存钱失败");
            }
        }
    }

    void OnBtnReturnClick()
    {
        if (_TextTransferInValue.text == "")
        {
            ATMScreenManager._Instance.CurrentScreen = SCREENTYPE.USERPANEL;
        }
        else
        {
            Debug.LogError("请先点击确定存钱");
        }

    }


}
