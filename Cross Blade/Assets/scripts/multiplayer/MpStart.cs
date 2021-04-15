using MLAPI;
using MLAPI.NetworkedVar;
using MLAPI.Transports.UNET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpStart : NetworkedBehaviour
{
    public GameObject player;
    OptionHolder options;
    [SerializeField]
    private SkinnedMeshRenderer playerBox = null;
    float modelHeight = 0;
    public bool spawned = false;
    public GameObject preSpawn;
    public GameObject local;
    public GameObject remote;
    public NetworkedVarFloat scale = new NetworkedVarFloat(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, 0f);
    public NetworkedVarInt connected = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, 0);
    public bool singel = false;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }


    void Start()
    {
        preSpawn = GameObject.Find("preConnect");

        this.transform.position = new Vector3(0, 0, 0);
        if (IsLocalPlayer || GameObject.Find("MPManager") == null)
        {
            options = GameObject.Find("constData").GetComponent<OptionHolder>();
            modelHeight = playerBox.bounds.size.y; //gets hight of model being used
            scale.Value = options.height / modelHeight; //gets scale
            changeLayers(remote.transform.parent.gameObject, "player");//needed to holp find enamy
            Destroy(remote);
        }
        else
        {
            Destroy(local);//destroy uneded local
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            connected.Value = NetworkingManager.Singleton.ConnectedClients.Count;
        }
        if (!spawned)
        {
            if (scale.Value != 0 && connected.Value >= 2)
            {
                //Destroy(preSpawn.gameObject)
                preSpawn.gameObject.SetActive(false);
                player.SetActive(true);
                spawned = true;
            }
        }

    }

    void changeLayers(GameObject obj, string layer) // changes objects layer then iterates troy eatch child and dose the same
    {
        obj.layer = LayerMask.NameToLayer(layer);
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            changeLayers(obj.transform.GetChild(i).gameObject, layer);
        }
    }
}
