using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ATMAudioManager : MonoBehaviour
{
    public AudioClip[] Audios;

    private static Dictionary<string, AudioClip> _ACDic;
    private static AudioSource _ASE;

    void Awake()
    {
        if (_ACDic == null)
        {
            _ACDic = new Dictionary<string, AudioClip>();
        }
        for (int i = 0; i < Audios.Length; i++)
        {
            _ACDic.Add(Audios[i].name, Audios[i]);
        }
        _ASE = gameObject.AddComponent<AudioSource>();
    }
    void OnDestroy()
    {
        if(_ACDic!=null)
            _ACDic.Clear();
    }
    public static float PlayEF(string ac)
    {
        if (_ACDic.ContainsKey(ac))
        {
            PlayEf(_ACDic[ac]);
            return _ACDic[ac].length;
        }
        else
        {
            Debug.LogError("without " + ac + " audioclip");
            return 0;
        }
    }


    private static void PlayEf(AudioClip ac)
    {
        if (ac != null)
        {
            _ASE.clip = ac;
            _ASE.Play();
        }
        else
        {
            Debug.LogError("ac is null");
        }
    }

}
