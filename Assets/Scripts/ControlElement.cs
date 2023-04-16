using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlElement : MonoBehaviour
{
    public enum OperateMode
    {
        NoUI,
        UI,
        AlterBone,
        RulerCtrl1,
        RulerCtrl2,
        RulerCtrl3,
    }
    public static OperateMode operateMode = OperateMode.NoUI;

    public int touched_count = 0;
    public UnityEvent onclick;
    public UnityEvent onhold;
    public Color defaultColor = Color.blue;

    // Start is called before the first frame update
    void Start()
    {

    }


    private Renderer mrenderer;
    void SetColor(Color color)
    {
        if (mrenderer == null)
            mrenderer = GetComponent<Renderer>();
        mrenderer.material.SetColor("_Color", color);
    }

    private Dictionary<GameObject, bool> isIn = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, bool> isHold = new Dictionary<GameObject, bool>();
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<ControlStick>() != null)
        {
            isIn[other.gameObject] = true;
            var action = Valve.VR.SteamVR_Input.GetAction<Valve.VR.SteamVR_Action_Boolean>("InteractUI");
            if (action.stateDown)
            {
                // clicked
                onclick.Invoke();
            }
            if (action.state)
            {
                isHold[other.gameObject] = true;
                onhold.Invoke();
            }
            else
            {
                isHold[other.gameObject] = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ControlStick>() != null)
        {
            isIn[other.gameObject] = false;
            isHold[other.gameObject] = false;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        bool isIn_ = false, isHold_ = false;
        foreach (var i in isIn.Values)
            isIn_ |= i;
        foreach (var i in isHold.Values)
            isHold_ |= i;
       
        if (isHold_)
            SetColor(Color.Lerp(defaultColor, Color.black, 0.5f));
        else if (isIn_)
            SetColor(Color.Lerp(defaultColor, Color.white, 0.5f));
        else
            SetColor(defaultColor);
    }
}
