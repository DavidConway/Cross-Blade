using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpNode : NetworkedBehaviour
{
    OptionHolder options;
    private SkinnedMeshRenderer playerBox = null;
    float modelHeight = 0;
    public GameObject player;
    public SkinnedMeshRenderer[] models;
    public NetworkedVarFloat scale = new NetworkedVarFloat(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly, SendTickrate = -1 },0f);
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        this.transform.position = new Vector3(0, 0, 0);
        /*while (NetworkingManager.num)
        {

        }*/
        if (IsLocalPlayer)
        {
            playerBox = models[0];
            modelHeight = playerBox.bounds.size.y; //gets hight of model being used
            scale.Value = options.height / modelHeight;
            options = GameObject.Find("constData").GetComponent<OptionHolder>();
            scale.Value = options.height / modelHeight;
            GameObject go = Instantiate(player,this.transform);
            //go.GetComponent<NetworkedObject>().Spawn();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
