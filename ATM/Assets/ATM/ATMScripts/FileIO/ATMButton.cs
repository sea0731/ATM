using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ATMButton : MonoBehaviour
{

    public string ScreenName;
    public string ButtonName;

    Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            ATMFileIO.WriteToFilePerformance(DateTime.Now - ATMDataManager.Instance.GetStartTime(), ScreenName + ","+ ButtonName);
        });
    }
   
}
