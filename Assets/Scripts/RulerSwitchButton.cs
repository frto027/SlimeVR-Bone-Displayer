using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerSwitchButton : ControlElement
{
    public OperateMode ruler;


    private void Start()
    {
        int idx = 0;
        switch (ruler)
        {
            case OperateMode.RulerCtrl1: idx = 0; break;
            case OperateMode.RulerCtrl2: idx = 1; break;
            case OperateMode.RulerCtrl3: idx = 2; break;
        }
        onclick.AddListener(() =>
        {
            RulerObject.ruler_enabled[idx] = !RulerObject.ruler_enabled[idx];
            if (RulerObject.ruler_enabled[idx])
                operateMode = ruler;
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        int idx = 0;
        switch (ruler)
        {
            case OperateMode.RulerCtrl1:idx = 0;break;
            case OperateMode.RulerCtrl2:idx = 1;break;
            case OperateMode.RulerCtrl3:idx = 2;break;
        }
        defaultColor = RulerObject.ruler_enabled[idx] ? Color.green : Color.red;
        base.Update();
    }
}
