using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weponHandeler : NetworkedBehaviour
{
    public NetworkedVarInt side = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.Everyone }, 0);
    public NetworkedVarInt height = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.Everyone }, 0);
    public NetworkedVarBool active = new NetworkedVarBool(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.Everyone }, false);
    public NetworkedVarBool highGround = new NetworkedVarBool(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.Everyone }, false);
    private WeponSound wepSound;
    private bool inWepon;
    private List<GameObject> colidingWepons;
    void Start()
    {
        side.Value = (int)Side.none;
        height.Value = (int)Height.none;
        wepSound = GetComponent<WeponSound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inWepon)
        {
            this.active.Value = false;
        }
    }

    //wepon colider handeler
    private void OnCollisionEnter(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        if (collision.gameObject.layer == LayerMask.NameToLayer("enamyWepon") && !colidingWepons.Contains(collision.gameObject))
        {
            if (myCol.tag == "blunts")
            {
                colidingWepons.Add(collision.gameObject);
                inWepon = true;
                wepSound.PlayBlock();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (colidingWepons.Contains(collision.gameObject))
        {
            colidingWepons.Remove(collision.gameObject);
        }
        if(colidingWepons.Count == 0)
        {
            inWepon = false;
        }
    }


}

public enum Side
{
    none,
    left,
    right,
    chop
}

public enum Height
{
    none,
    chop,
    top,
    mid,
    bot
}
