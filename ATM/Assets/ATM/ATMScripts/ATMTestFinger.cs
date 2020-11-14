using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ATMTestFinger : MonoBehaviour
{
    public Button _A;
    public GameObject _B;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ATMTaskTipFingerManager._Instance.ShowTipFinger(_A.transform);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ATMTaskTipFingerManager._Instance.ShowTipFinger(_B.transform);
        }
    }
}
