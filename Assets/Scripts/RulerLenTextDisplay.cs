using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerLenTextDisplay : MonoBehaviour
{
    public RulerObject[] rulers;

    // Update is called once per frame
    void Update()
    {
        string text = "";
        for(int i = 0; i < rulers.Length; i++)
        {
            if (RulerObject.ruler_enabled[i])
            {
                text += "³ß×Ó" + (i + 1) + ":" + rulers[i].getLength().ToString("0.00") + "m ";
            }
        }
        GetComponent<TMPro.TMP_Text>().text = text;
    }
}
