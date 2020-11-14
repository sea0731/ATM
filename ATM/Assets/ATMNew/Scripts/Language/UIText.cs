using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class UIText : MonoBehaviour
{
    [SerializeField]
    private string key;
    // Use this for initialization
    void Start()
    {
        if (!string.IsNullOrEmpty(key))
        {
            string value = LanguageMgr.Instance.GetText(key);
            if (!string.IsNullOrEmpty(value))
            {
                string[] subs = value.Split(',');
                string outputText = subs[0];

                for(int i = 1; i < subs.Length; i++)
                {
                    outputText += '\n' + subs[i];
                }
                gameObject.GetComponent<Text>().text = outputText;
            }
        }
    }
}