using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDText : MonoBehaviour
{
    public static bool displayLicense = true;


    string GetText(out float fontsize)
    {
        fontsize = 0.7f;
        foreach (var r in RulerObject.ruler_displaying)
        {
            if (r)
            {
                return RulerObject.ruler_text;
            }
        }
        if (displayLicense)
        {
            fontsize = 0.4f;
            return "本程序为@Frto027个人作品\n用于显示SlimeVR数据\n需配合定制的SlimeVR Server使用";
        }
        return "";
    }
    // Update is called once per frame
    void Update()
    {
        float size;
        var tmp = GetComponent<TMP_Text>();
        tmp.text = GetText(out size);
        tmp.fontSize = size;
    }
}
