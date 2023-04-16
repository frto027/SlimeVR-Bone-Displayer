using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceMarker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ParentLine;
    public TMPro.TextMeshPro textMeshPro;
    public Transform targetCamera;

    void Start()
    {
        
    }

    private void Update()
    {
        if (targetCamera == null)
            return;
        textMeshPro.transform.rotation = Quaternion.LookRotation(textMeshPro.transform.position - targetCamera.position);
    }

    public void setName(string name)
    {
        textMeshPro.text = name;
    }

    public void FeedLocalTrans(TraceMarker parent, Vector3 local)
    {
        if (parent == null)
        {
            this.transform.position = local;
            ParentLine.gameObject.SetActive(false);
        }
        else
        {
            Vector3 parentPos = parent.transform.position;
            this.transform.position = parentPos + local;
            ParentLine.transform.localScale = Vector3.one * 0.01f + Vector3.forward * (Mathf.Abs(local.magnitude) - 0.01f);
            ParentLine.transform.position = parentPos + local / 2;
            ParentLine.transform.LookAt(this.transform);
        }

    }
    public void FeedWorldTrans(TraceMarker parent, Vector3 world)
    {
        if (parent == null)
        {
            this.transform.position = world;
            ParentLine.gameObject.SetActive(false);
        }
        else
        {
            Vector3 parentPos = parent.transform.position;
            this.transform.position = world;
            ParentLine.transform.localScale = Vector3.one * 0.001f + Vector3.forward * (Mathf.Abs((world - parentPos).magnitude) - 0.001f);
            ParentLine.transform.position = parentPos + (world - parentPos) / 2;
            ParentLine.transform.LookAt(this.transform);
        }

    }
}
