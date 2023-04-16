using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUIButton : ControlElement
{
    // Start is called before the first frame update
    public GameObject uielement;
    void Start()
    {
        onclick.AddListener(() => {
            operateMode = OperateMode.NoUI;
            uielement.SetActive(false);
        });
    }

}
