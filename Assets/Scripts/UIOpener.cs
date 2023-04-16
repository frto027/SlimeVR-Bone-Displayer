using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOpener : MonoBehaviour
{
    public GameObject uielement;
    public GameObject controller_sphere;
    // Start is called before the first frame update
    void Start()
    {
        uielement.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(ControlElement.operateMode == ControlElement.OperateMode.NoUI && Valve.VR.SteamVR_Input.GetAction<Valve.VR.SteamVR_Action_Boolean>("InteractUI").stateDown)
        {
            HUDText.displayLicense = false;
            ControlElement.operateMode = ControlElement.OperateMode.UI;
            uielement.SetActive(true);
            uielement.transform.position = controller_sphere.transform.position;
            uielement.transform.rotation = controller_sphere.transform.rotation;
        }
        if (Valve.VR.SteamVR_Input.GetAction<Valve.VR.SteamVR_Action_Boolean>("reset_ui").stateDown)
        {
            HUDText.displayLicense = false;
            uielement.transform.position = controller_sphere.transform.position;
            uielement.transform.rotation = controller_sphere.transform.rotation;
        }
    }
}
