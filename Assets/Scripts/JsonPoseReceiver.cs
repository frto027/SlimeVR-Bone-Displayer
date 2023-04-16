using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class JsonPoseReceiver : MonoBehaviour
{
    private UnityEngine.UI.Text displayText;
    Dictionary<string, TraceMarker> markers = new Dictionary<string, TraceMarker>();
    Dictionary<string, TraceMarker> secMarkers = new Dictionary<string, TraceMarker>();
    public TraceMarker TraceMarkerPrefab;

    public bool isEnableSecondMarker = false;
    public float distance = 1.5f, angle = 0;

    public Transform TragetCamera;
    public Transform AlterMarkerParent;

    public bool isMirror = false;

    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<UnityEngine.UI.Text>();
    }

    int time = 0;

    private void FixedUpdate()
    {
        if (isEnableSecondMarker && ControlElement.operateMode == ControlElement.OperateMode.AlterBone)
        {
            var action = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("MoveOrRotAlterBone");
            //angle += action.axis.x;
            if (time++ % 30 == 0)
            {
                if (action.axis.y > 0.5)
                    distance += 0.5f;
                else if (action.axis.y > 0.2)
                    distance += 0.2f;
                if (action.axis.y < -0.5)
                    distance -= 0.5f;
                else if (action.axis.y < -0.2)
                    distance -= 0.2f;


                if (action.axis.x > 0.5)
                    angle += 30;
                if (action.axis.x < -0.5)
                    angle -= 30;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        /*var act = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ToggleAlterBone");
        if (act.stateDown)
        {
            isEnableSecondMarker = !isEnableSecondMarker;
        }*/
        
        var act = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ResetAlterBone");
        if (ControlElement.operateMode == ControlElement.OperateMode.AlterBone && isEnableSecondMarker && act.stateDown)
        {
            distance = 1.5f;
            angle = 0;
        }
        AlterMarkerParent.gameObject.SetActive(isEnableSecondMarker);
        act = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ToggleMirror");
        if (ControlElement.operateMode == ControlElement.OperateMode.AlterBone && act.stateDown)
            isMirror = !isMirror;

        string str = JsonPoseReceiveHelper.GetLatestPackage();
        bool ok = true;
        if(str == null)
        {
            str = "package is null";
            ok = false;
        }
        if (displayText)
        {
            displayText.text = str;
        }
        if (ok)
        {
            JsonPosePackage posePackage = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPosePackage>(str);
            if(posePackage != null)
            {
                HandlePackage(markers, posePackage, null,0,0,Vector3.zero, false);
                if (markers.ContainsKey("HMD"))
                    markers["HMD"].gameObject.SetActive(false);
                if (markers.ContainsKey("Head"))
                    markers["Head"].gameObject.SetActive(false);
                if (isEnableSecondMarker)
                {
                    foreach (var m in secMarkers.Values)
                        m.gameObject.SetActive(true);
                    HandlePackage(secMarkers, posePackage, null, distance, angle,
                        /* rotate around the first node(HMD) */
                        Vector3.zero //new Vector3(posePackage.worldTrans[0], posePackage.worldTrans[1], -posePackage.worldTrans[2])
                        ,isMirror
                        );
                    {
                        AlterMarkerParent.position = Vector3.zero;

                        float cos = Mathf.Cos(Mathf.Deg2Rad * angle), sin = Mathf.Sin(Mathf.Deg2Rad * angle);
                        float x = 0, z = 1,xx,zz;
                        xx = x * cos - z * sin;
                        zz = x * sin + z * cos;


                        AlterMarkerParent.position = Vector3.forward * distance;
                        AlterMarkerParent.rotation = Quaternion.LookRotation(new Vector3(xx,0,zz), Vector3.up);
                        AlterMarkerParent.localScale = isMirror ? (Vector3.one - 2 * Vector3.forward) : Vector3.one;
                    }
                }
                else
                {
                    foreach (var m in secMarkers.Values)
                        m.gameObject.SetActive(false);
                }
            }
            else
            {
                ok = false;
            }
        }
    }

    void HandlePackage(Dictionary<string, TraceMarker> markers, JsonPosePackage pkg, TraceMarker parent, float distance, float angle, Vector3 rotCenter, bool isMirror)
    {
        TraceMarker me;
        if (markers.ContainsKey(pkg.name))
        {
            me = markers[pkg.name];
        }
        else
        {
            me = Instantiate(TraceMarkerPrefab);
            me.targetCamera = TragetCamera;
            markers[pkg.name] = me;
            me.setName(pkg.name);
        }

        Vector3 pos = new Vector3(pkg.worldTrans[0], pkg.worldTrans[1], ( isMirror ? 1 : -1 ) * pkg.worldTrans[2]);


        pos.x -= rotCenter.x;
        pos.z -= rotCenter.z;

        //rotate
        float cos = Mathf.Cos(Mathf.Deg2Rad * angle), sin = Mathf.Sin(Mathf.Deg2Rad * angle);
        float x = pos.x, z = pos.z;
        pos.x = x * cos - z * sin;
        pos.z = x * sin + z * cos;

        pos.x += rotCenter.x;
        pos.z += rotCenter.z;
        


        //move forward
        pos.z += distance;

        me.FeedWorldTrans(parent, pos);

        foreach(JsonPosePackage child in pkg.childs)
        {
            HandlePackage(markers, child, me, distance, angle, rotCenter, isMirror);
        }
    }
}

public class JsonPosePackage
{
    public string name;
    public float[] localTrans;
    public float[] worldTrans;
    public List<JsonPosePackage> childs;
}
