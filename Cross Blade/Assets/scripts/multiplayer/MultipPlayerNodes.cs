using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Serialization;
using System.Security.Policy;
using System.IO;

public class MultipPlayerNodes : NetworkedBehaviour
{
    public float stopDist = 0.05f;

    private float lastPacket;
    public int gitter = 5;
    private Queue<Vector3> oldHeadPos = new Queue<Vector3>();
    private Queue<Vector3> oldLeftPos = new Queue<Vector3>();
    private Queue<Vector3> oldRightPos = new Queue<Vector3>();

    private Vector3 headTarget,leftTarget,rightTarget;


    public GameObject playerHead, playerLeft, playerRight,playerBodyC, nodeBody, nodeLeft, nodeRight , local , remote;

    NetworkedVarDouble updateTime = new NetworkedVarDouble(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly });
    double lastUpdateTime = 0;
    
    void Start()
    {
        stopDist = 0.05f;
        gitter = 5;
        if (IsLocalPlayer)
        {
            remote.SetActive(false);
        }
        else
        {
            local.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        double currentEpochTime = (System.DateTime.UtcNow - epochStart).TotalMilliseconds; // gets time sins epock

        if (IsOwner)
        {
            //updates pos data
            updateTime.Value = currentEpochTime; // time update takes plays

            nodeBody.transform.position = playerBodyC.transform.position;
            nodeBody.transform.rotation = playerBodyC.transform.rotation;

            nodeRight.transform.position = playerRight.transform.position;
            nodeRight.transform.rotation = playerRight.transform.rotation;

            nodeLeft.transform.position = playerLeft.transform.position;
            nodeLeft.transform.rotation = playerLeft.transform.rotation;
        }
    }
}



