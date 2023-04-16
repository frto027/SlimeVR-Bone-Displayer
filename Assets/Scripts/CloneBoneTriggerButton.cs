using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBoneTriggerButton : ControlElement
{
    public JsonPoseReceiver receiver;
    // Start is called before the first frame update
    void Start()
    {
        onclick.AddListener(() =>
        {
            receiver.isEnableSecondMarker = !receiver.isEnableSecondMarker;
        });
    }

    // Update is called once per frame
    protected override void Update()
    {
        defaultColor = receiver.isEnableSecondMarker ? Color.green : Color.red;
        base.Update();
    }


}
