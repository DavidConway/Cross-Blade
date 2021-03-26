using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerWeponStart : NetworkedBehaviour
{
    public GameObject localLeft, localRight, remoteLeft, RemoteRight;
    public GameObject[] wepons;
    private OptionHolder options;
    GameObject left;
    GameObject right;
    private bool weponSpawned = false;
    private NetworkedVarInt weponLeft = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly},-1);
    private NetworkedVarInt weponRight = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly},-1);

    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        if (IsLocalPlayer || GameObject.Find("MPManager") == null)
        {
            options = GameObject.Find("constData").GetComponent<OptionHolder>();
            left = Instantiate(wepons[options.leftWepon],localLeft.transform);
            right = Instantiate(wepons[options.rightWepon], localRight.transform);

            left.transform.localPosition = new Vector3(0, 0, 0);
            right.transform.localPosition = new Vector3(0, 0, 0);

            weponLeft.Value = options.leftWepon;
            weponRight.Value = options.rightWepon;

            weponSpawned = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("MPManager") != null)
        {
            if (!IsLocalPlayer && !weponSpawned)
            {
                trySpawnWepons();
            }
        }
    }

    void trySpawnWepons()
    {
        if (weponLeft.Value == -1 || weponRight.Value == -1)
        {
            return;
        }
        else
        {
            left = Instantiate(wepons[weponLeft.Value], remoteLeft.transform);
            right = Instantiate(wepons[weponRight.Value], RemoteRight.transform);

            left.transform.localPosition = new Vector3(0, 0, 0);
            right.transform.localPosition = new Vector3(0, 0, 0);
            changeLayers(left.gameObject, "enamyWepon");
            changeLayers(right.gameObject, "enamyWepon");
            weponSpawned = true;
        }
    }

    void changeLayers(GameObject obj, string layer) // changes objects layer then iterates troy eatch child and dose the same
    {
        obj.layer = LayerMask.NameToLayer(layer);
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            changeLayers(obj.transform.GetChild(i).gameObject, layer);
        }
    }
}
