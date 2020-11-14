using UnityEngine;
using System.Collections;

public class ATMTrainingBankNote : MonoBehaviour {
    public static ATMTrainingBankNote Instance;
	// Use this for initialization
	void Awake()
    {
        Instance = this;
    }
	void OnDestroy()
    {
        Instance = null;
    }
	// Update is called once per frame
	void Update () {
	
	}
    public GameObject GetMoneyGo()
    {
        return gameObject;
    }
}
