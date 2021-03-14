using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerStartUp : NetworkedBehaviour
{
    public GameObject localLeft, localRight, remoteLeft, RemoteRight;
    public GameObject[] wepons;
    private OptionHolder options;
    public NetworkedVarInt weponLeft = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly, SendTickrate = -1 });
    public NetworkedVarInt weponRight = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly, SendTickrate = -1 });
    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        GameObject left;
        GameObject right;
        if (IsLocalPlayer)
        {
            options = GameObject.Find("constData").GetComponent<OptionHolder>();
            left = Instantiate(wepons[options.leftWepon],localLeft.transform);
            right = Instantiate(wepons[options.rightWepon], localRight.transform);

            left.transform.localPosition = new Vector3(0, 0, 0);
            right.transform.localPosition = new Vector3(0, 0, 0);

            weponLeft.Value = options.leftWepon;
            weponRight.Value = options.rightWepon;
        }
        else
        {
            left = Instantiate(wepons[weponLeft.Value], remoteLeft.transform);
            right = Instantiate(wepons[weponRight.Value],  RemoteRight.transform);

            left.transform.localPosition = new Vector3(0, 0, 0);
            right.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
