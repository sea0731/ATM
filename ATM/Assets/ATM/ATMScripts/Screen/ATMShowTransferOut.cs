using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMShowTransferOut : MonoBehaviour
{
    public AudioClip _Machine;
    public Text _ShowMessage;
    public Text _TransferOuting;

    //public Button _BtnConfirm;
    //void Awake()
    //{
    //    _BtnConfirm.onClick.AddListener(OnBtnConfirmClick);
    //}

    void OnEnable()
    {
        StartCoroutine(ShowMessageIE());

    }

    IEnumerator ShowMessageIE()
    {
        // _BtnConfirm.gameObject.SetActive(false);
        _ShowMessage.gameObject.SetActive(false);
        _TransferOuting.gameObject.SetActive(true);
        if (_Machine != null)
        {
            yield return new WaitForSeconds(_Machine.length);
        }
        _TransferOuting.gameObject.SetActive(false);
        _ShowMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        //  _BtnConfirm.gameObject.SetActive(true);
    }

}
