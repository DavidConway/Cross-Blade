using UnityEngine;
using MLAPI;
using MLAPI.NetworkedVar;

public class MultipPlayerNodes : NetworkedBehaviour
{
    public float stopDist = 0.05f;
    public int gitter = 5;


    public GameObject playerHead, playerLeft, playerRight,playerBodyC,nodeHead, nodeBody, nodeLeft, nodeRight , local , remote;

    NetworkedVarDouble updateTime = new NetworkedVarDouble(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly });
    
    void Start()
    {
        if (IsLocalPlayer)
        {
            nodeHead.layer = LayerMask.NameToLayer("player"); //needed to stop errors in enamy detection
        }
    }

    // Update is called once per frame
    void Update()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        double currentEpochTime = (System.DateTime.UtcNow - epochStart).TotalMilliseconds; // gets time sins epock

        if (IsOwner || GameObject.Find("MPManager") == null)
        {
            //updates pos data
            updateTime.Value = currentEpochTime; // time update takes plays

            nodeHead.transform.position = playerHead.transform.position;
            nodeHead.transform.position = playerHead.transform.position;

            nodeBody.transform.position = playerBodyC.transform.position;
            nodeBody.transform.rotation = playerBodyC.transform.rotation;

            nodeRight.transform.position = playerRight.transform.position;
            nodeRight.transform.rotation = playerRight.transform.rotation;

            nodeLeft.transform.position = playerLeft.transform.position;
            nodeLeft.transform.rotation = playerLeft.transform.rotation;
        }
    }
}



