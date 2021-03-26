using MLAPI;
using MLAPI.NetworkedVar;
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
    public GameObject local;
    public GameObject remote;
    public NetworkedVarFloat scale = new NetworkedVarFloat(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, 0f);

    // Start is called before the first frame update
    private void Awake()
    {
        
    }


    void Start()
    {
        this.transform.position = new Vector3(0, 0, 0);
        if (IsLocalPlayer || GameObject.Find("MPManager") == null)
        {
            options = GameObject.Find("constData").GetComponent<OptionHolder>();
            modelHeight = playerBox.bounds.size.y; //gets hight of model being used
            scale.Value = options.height / modelHeight; //gets scale
            Destroy(remote);
        }
        else
        {
            Destroy(local);//destroy uneded local
        }
        //GameObject go =Instantiate(player, this.transform);
        //go.transform.localPosition = new Vector3(0, 0, 0);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            if (scale.Value != 0)
            {
                player.SetActive(true);
                spawned = true;
            }
        }
    }
}
