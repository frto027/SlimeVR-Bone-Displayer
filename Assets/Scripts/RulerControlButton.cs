using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerControlButton : ControlElement
{
    public OperateMode rulerMode;
    void Start()
    {
        onclick.AddListener(() => { operateMode = rulerMode; });
    }

    // Update is called once per frame
    protected override void Update()
    {
        defaultColor = operateMode == rulerMode ? Color.yellow : Color.gray;
        base.Update();
    }
}
