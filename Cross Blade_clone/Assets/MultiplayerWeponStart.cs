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
    public GameObject left;
    public GameObject right;
    private NetworkedVarInt weponSpawned = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, 0);
    private NetworkedVarInt weponLeft = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly},-1);
    private NetworkedVarInt weponRight = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly},-1);
    private NetworkedVarULong id = new NetworkedVarULong(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly });
    private NetworkedVarULong leftId = new NetworkedVarULong(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly });
    private NetworkedVarULong rightId = new NetworkedVarULong(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly });


    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        
        if (GameObject.Find("MPManager") == null || IsLocalPlayer )
        {
            options = GameObject.Find("constData").GetComponent<OptionHolder>();
            weponLeft.Value = options.leftWepon;
            weponRight.Value = options.rightWepon;
            if (GameObject.Find("MPManager") != null)
            {
                id.Value = NetworkingManager.Singleton.LocalClientId;
            }
            

        }
        if (IsServer && GameObject.Find("MPManager") != null)
        {
            left.GetComponent<NetworkedObject>().Spawn();
            right.GetComponent<NetworkedObject>().Spawn();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("MPManager") != null)
        {
            if (IsServer && weponSpawned.Value == 0) // phase one of wepon spawn instanceate and assine
            {
                trySpawnWepons();
            }
            if (IsLocalPlayer && weponSpawned.Value == 1) // phase 2 attache wepon to hand
            {
                left = GetNetworkedObject(leftId.Value).gameObject;
                right = GetNetworkedObject(rightId.Value).gameObject;

                left.transform.parent = localLeft.transform;
                right.transform.parent = localRight.transform;
                left.transform.localPosition = new Vector3(0, 0, 0);
                right.transform.localPosition = new Vector3(0, 0, 0);
                Quaternion zeroRote = new Quaternion(0, 0, 0, 0);
                zeroRote.eulerAngles = new Vector3(0, 0, 0);
                left.transform.localRotation = zeroRote;
                right.transform.localRotation = zeroRote;
                /*if (!IsHost) //need to rotate wepon if not host
                {
                    right.transform.localScale = new Vector3(-1, 1, 1); // mirros in right hand
                    right.transform.localScale = new Vector3(-1, 1, 1); // mirros in right hand
                }
                else
                {
                    right.transform.localScale = new Vector3(-1, 1, -1); // mirros in right hand
                    right.transform.localScale = new Vector3(1, 1, -1); // mirros in right hand
                }*/



                weponSpawned.Value = 2;
            }
            if(!IsLocalPlayer && weponSpawned.Value == 2)//phase 3 turn non player wepons to enamy wepons
            {
                left = GetNetworkedObject(leftId.Value).gameObject;
                right = GetNetworkedObject(rightId.Value).gameObject;
                changeLayers(left.gameObject, "enamyWepon");
                changeLayers(right.gameObject, "enamyWepon");
            }
        }
        else
        {
            if (weponSpawned.Value == 0)
            {
                left = Instantiate(wepons[options.leftWepon],localLeft.transform);
                right = Instantiate(wepons[options.rightWepon],localRight.transform);
                left.transform.localPosition = new Vector3(0, 0, 0);
                right.transform.localPosition = new Vector3(0, 0, 0);
                right.transform.localScale = new Vector3(-1, 1, 1); // mirros in left hand
                weponSpawned.Value = 1;
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

            GameObject leftNew = Instantiate(wepons[weponLeft.Value]); // spawns wepons
            GameObject rightNew = Instantiate(wepons[weponRight.Value]);
            

            NetworkedObject leftNet = leftNew.GetComponent<NetworkedObject>(); // gets networkobject comp
            NetworkedObject rightNet = rightNew.GetComponent<NetworkedObject>();

            

            leftNet.Spawn();
            leftNet.ChangeOwnership(id.Value);
            rightNet.Spawn();
            rightNet.ChangeOwnership(id.Value);
            //changeLayers(left.gameObject, "enamyWepon");
            //changeLayers(right.gameObject, "enamyWepon");
            leftId.Value = leftNet.NetworkId;
            rightId.Value = rightNet.NetworkId;// assigns
            weponSpawned.Value = 1;
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
