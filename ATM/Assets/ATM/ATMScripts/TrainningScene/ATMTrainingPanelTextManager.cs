using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ATMTrainingPanelTextManager : MonoBehaviour {

    public static ATMTrainingPanelTextManager _Instance;
    public Text _text;

    private Queue<string> _textQueue;
    void Awake()
    {
        _Instance = this;
        _textQueue = new Queue<string>();
    }
    void Start()
    {
        
    }
    public void AddText(string s)
    {
        _textQueue.Enqueue(s);

    }

    public IEnumerator NextText()
    {
        if (_textQueue.Count > 0)
        {
            GetComponent<Image>().raycastTarget = true;
            GetComponent<Image>().CrossFadeAlpha(1, 1f, true);

            (_text as Graphic).CrossFadeAlpha(0, 1, true);
            yield return new WaitForSeconds(1);
            _text.text = _textQueue.Dequeue();
            (_text as Graphic).CrossFadeAlpha(1, 1, true);
            yield return new WaitForSeconds(1);
        }
        else
        {
            _text.text = "";
            (GetComponent<Image>() as Graphic).CrossFadeAlpha(0, 2f, true);
            yield return new WaitForSeconds(2);
            GetComponent<Image>().raycastTarget=false;
        }
           
        yield return null;  
    }

    void OnDestroy()
    {
        _Instance = null;
    }
}
