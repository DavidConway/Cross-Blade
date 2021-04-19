using MLAPI;
using MLAPI.NetworkedVar;
using System;
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
    private bool localActive = false;

    public bool inWepon;
    public bool hitting;
    private Material mat;
    public MeshRenderer renderer;

    void Start()
    {
        side.Value = (int)Side.none;
        height.Value = (int)Height.none;
        wepSound = GetComponent<WeponSound>();
        foreach(Material i in renderer.materials)
        {
            if(i.name == "light (Instance)")
            {
                mat = i;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inWepon)
        {
            this.active.Value = false;
        }

        if (localActive != active.Value)
        {
            localActive = !localActive;
            if (localActive)
            {
                mat.SetColor("_EmissionColor", Color.blue);
            }
            else
            {
                mat.SetColor("_EmissionColor", Color.red);
            }
        }
    }

    //wepon colider handeler
    private void OnCollisionStay(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        if (collision.gameObject.layer == LayerMask.NameToLayer("enamyWepon") || collision.gameObject.layer == LayerMask.NameToLayer("playerWeapon"))
        {
             if (myCol.tag == "blunts")
            {
                if(inWepon == false && active.Value)
                {
                    debugLogConsole.uiLog(this.gameObject.name + " " + collision.gameObject.name);
                    collision.gameObject.GetComponent<weponHandeler>().active.Value = false;
                    wepSound.PlayBlock();
                }
                inWepon = true;
                
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("enamyWepon") || collision.gameObject.layer == LayerMask.NameToLayer("playerWeapon")){
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
