using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSphereDistanceBtn : ControlElement
{
    public int distance = 1;
    // Start is called before the first frame update
    void Start()
    {
        onclick.AddListener(() =>
        {
            ControlerSpherePosition.distanceMultiply = distance;
            if(ControlerSpherePosition.pos == ControlerSpherePosition.Position.Back && distance < 15)
            {
                ControlerSpherePosition.distanceMultiply = 15;
            }
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        defaultColor = ControlerSpherePosition.distanceMultiply == distance ? Color.yellow : Color.gray;
        base.Update();
    }
}
