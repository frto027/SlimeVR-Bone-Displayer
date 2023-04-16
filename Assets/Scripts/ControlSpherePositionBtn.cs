using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSpherePositionBtn : ControlElement
{
    public ControlerSpherePosition.Position pos;

    private void Start()
    {
        onclick.AddListener(() => {
            ControlerSpherePosition.pos = pos;
            if (pos == ControlerSpherePosition.Position.Back && ControlerSpherePosition.distanceMultiply < 15)
                ControlerSpherePosition.distanceMultiply = 15;
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        defaultColor = pos == ControlerSpherePosition.pos ? Color.yellow : Color.gray;
        base.Update();
    }
}
