using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBoneMirrorButton : ControlElement
{
    public JsonPoseReceiver receiver;
    // Start is called before the first frame update
    void Start()
    {
        onclick.AddListener(() => { receiver.isMirror = !receiver.isMirror; });
    }

    // Update is called once per frame
    protected override void Update()
    {
        defaultColor = receiver.isMirror ? Color.green : Color.red;
        base.Update();
    }
}
