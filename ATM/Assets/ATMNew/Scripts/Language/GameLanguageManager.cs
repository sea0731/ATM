using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLanguageManager : IGameManager
{
    int IGameManager.Weight { get; }
    void IGameManager.ManagerInit()
    {
        //instance = this;
        loadLanguage();
    }
    IEnumerator IGameManager.ManagerDispose()
    {
        yield return null;
    }

    private void loadLanguage()
    {
       /* //加載文件
        TextAsset ta = Resources.Load<TextAsset>(language.ToString());
        if (ta == null)
        {
            Debug.LogWarning("沒有這個語言的翻譯文件");
            return;
        }
        //獲取每一行
        string[] lines = ta.text.Split('\n');
        //獲取key value
        for (int i = 0; i < lines.Length; i++)
        {
            //檢測
            if (string.IsNullOrEmpty(lines[i]))
                continue;
            //獲取 key:kv[0] value kv[1]
            string[] kv = lines[i].Split(':');
            //保存到字典
            dict.Add(kv[0], kv[1]);
            Debug.Log(string.Format("key:{0}, value:{1}", kv[0], kv[1]));
        }*/
    }
}
