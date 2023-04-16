using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerObject : MonoBehaviour
{
    public Vector3 start, end;
    private Vector3 scalevec = new Vector3(0.001f,0.001f,0);
    
    public ControlElement.OperateMode rulerOperate;
    public Transform controller;

    public static bool[] ruler_enabled = new bool[3];
    public static bool[] ruler_displaying = new bool[3];
    public static string ruler_text;

    public bool isMirror = false;
    public RulerObject mirror;

    

    public enum CtrlMode
    {
        Normal,SetStart,SetEnd
    }
    public static CtrlMode mode;

    private void Start()
    {
    }

    public float getLength()
    {
        return scalevec.z;
    }

    // Update is called once per frame
    void Update()
    {
        int idx = 0;
        switch (rulerOperate)
        {
            case ControlElement.OperateMode.RulerCtrl1: idx = 0;break;
            case ControlElement.OperateMode.RulerCtrl2: idx = 1;break;
            case ControlElement.OperateMode.RulerCtrl3: idx = 2;break;
        }

        GetComponent<Renderer>().enabled = ruler_enabled[idx];

        bool should_display_text = false;

        if (isMirror)
        {
            //mirror don't have any operate
        }
        else
        {
            if (ControlElement.operateMode == rulerOperate)
            {
                var ctrl_action = Valve.VR.SteamVR_Input.GetAction<Valve.VR.SteamVR_Action_Boolean>("ruler_ctrl");
                if (ctrl_action.stateDown)
                {
                    if (mode == CtrlMode.Normal)
                    {
                        start = controller.position;
                    }
                }
                if (ctrl_action.state)
                {
                    switch (mode)
                    {
                        case CtrlMode.Normal:
                            end = controller.position; break;
                        case CtrlMode.SetStart:
                            start = controller.position; break;
                        case CtrlMode.SetEnd:
                            end = controller.position; break;
                    }
                    should_display_text = true;
                }
            }
        }

        transform.localPosition = (start + end) / 2;
        if (transform.parent)
        {
            transform.LookAt(transform.parent.TransformPoint(end));
        }
        else
        {
            transform.LookAt(end);
        }
        scalevec.z = (end - start).magnitude;
        transform.localScale = scalevec;

        if (!isMirror)
        {
            if (should_display_text)
            {
                ruler_displaying[idx] = true;
                ruler_text = getLength().ToString("0.00") + "m";
            }
            else
            {
                ruler_displaying[idx] = false;
            }
        }

        if (mirror)
        {
            mirror.start = start;
            mirror.end = end;
        }

    }
}
