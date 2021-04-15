using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteModelPos : NetworkedBehaviour
{
    GameObject cam = null;
    float modelHeight = 0;

    //public NetworkedVarFloat scale = new NetworkedVarFloat(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly, SendTickrate = -1 },0f);
    public LocalModelPos local = null;
    public GameObject bodyCenter;

    private void Awake()
    {
        float scale = this.transform.parent.parent.GetComponentInParent<MpStart>().scale.Value;
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = bodyCenter.transform.position;
        this.transform.rotation = Quaternion.Euler(0, bodyCenter.transform.eulerAngles.y, 0); //positions the remote modedl
    }
}
