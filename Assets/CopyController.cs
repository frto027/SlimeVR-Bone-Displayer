using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CopyController : MonoBehaviour
{
    public Transform target;
    
    private SteamVR_RenderModel tarChild, child;
    // Start is called before the first frame update
    void Start()
    {
        tarChild = target.GetComponentInChildren<SteamVR_RenderModel>();
        child = GetComponentInChildren<SteamVR_RenderModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (child.index != tarChild.index)
        {
            child.index = tarChild.index;
            child.UpdateModel();
        }
        transform.localPosition = target.localPosition;
        transform.localRotation = target.localRotation;
    }
}
