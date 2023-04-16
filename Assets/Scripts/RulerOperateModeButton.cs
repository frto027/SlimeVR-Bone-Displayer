using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerOperateModeButton : ControlElement
{
    public RulerObject.CtrlMode mode;
    void Start()
    {
        onclick.AddListener(() => {
            if (RulerObject.mode == mode)
                RulerObject.mode = RulerObject.CtrlMode.Normal;
            else
                RulerObject.mode = mode;
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        defaultColor = mode == RulerObject.mode ? Color.yellow : Color.gray;
        base.Update();
    }
}
