using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBoneControlButton : ControlElement
{
    // Start is called before the first frame update
    void Start()
    {
        onclick.AddListener(() => { operateMode = OperateMode.AlterBone; });
    }

    // Update is called once per frame
    protected override void Update()
    {
        defaultColor = operateMode == OperateMode.AlterBone ? Color.yellow : Color.gray;
        base.Update();
    }


}
