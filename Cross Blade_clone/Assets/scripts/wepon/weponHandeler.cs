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
    void Start()
    {
        side.Value = (int)Side.none;
        height.Value = (int)Height.none;
        wepSound = GetComponent<WeponSound>();
    }

    // Update is called once per frame
    void Update()
    {
        //debugLogConsole.uiLog(gameObject.name+ " active: " + this.active.ToString() + " side: " +this.side.ToString()+ " height: " + this.height.ToString());
    }

    //wepon colider handeler
    private void OnCollisionEnter(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        if (collision.gameObject.layer == LayerMask.NameToLayer("enamyWepon"))
        {
            if (myCol.tag == "blunts")
            {
                this.active.Value = false;
                wepSound.PlayBlock();
            }
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
