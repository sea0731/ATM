using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class ATMCashGO : MonoBehaviour
{
    public Image _CashGo;
    public Button _BtnParent;
    public Button _BtnChild;
    public Text _ValueCash;

    public ATMScreenTransferOutThird _ATMScreenTransferOutThird;
    void Awake()
    {
        _BtnParent.onClick.AddListener(OnBtnClick);
        _BtnChild.onClick.AddListener(OnBtnClick);
    }

    void OnEnable()
    {
        _CashGo.transform.localScale = Vector3.zero;
        _CashGo.StartCoroutine(ShowCashOut());
    }

    IEnumerator ShowCashOut()
    {
        _CashGo.transform.DOLocalMoveY(120, 0.5f);
        _CashGo.transform.DOScale(Vector3.one, 1f);
        yield return new WaitForSeconds(0.5f);
        _CashGo.transform.DOLocalMoveY(40, 0.5f);
    }

    void OnBtnClick()
    {
        gameObject.SetActive(false);
        _ATMScreenTransferOutThird.SetTip();
    }
}
