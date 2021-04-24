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

    public bool hitting;
    private Material mat;
    public MeshRenderer renderer;
    private float wepSoundDelay = 1;
    public List<GameObject> weponsIn = new List<GameObject>();

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
        if(wepSoundDelay >= 0)
        {
            wepSoundDelay -= Time.deltaTime;
        }

        if (this.active.Value && inWepon())
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
    private void OnCollisionEnter(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        weponHandeler other = collision.gameObject.GetComponent<weponHandeler>();
        if (collision.gameObject.layer == LayerMask.NameToLayer("enamyWepon") || collision.gameObject.layer == LayerMask.NameToLayer("playerWeapon"))
        {
            other.weponsIn.Add(this.gameObject);
             if (myCol.tag == "blunts")
            {
                if(active.Value)
                {
                    debugLogConsole.uiLog(this.gameObject.name + " " + collision.gameObject.name);
                    other.active.Value = false;
                    if (collision.gameObject.layer == LayerMask.NameToLayer("enamyWepon"))
                    {
                        hitBoxHandeler.hitTime = 1;
                    }
                    if (wepSoundDelay <= 0)
                    {
                        wepSoundDelay = 1;
                        wepSound.PlayBlock();
                    }
                }
      
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("enamyWepon") || collision.gameObject.layer == LayerMask.NameToLayer("playerWeapon"))
        {
            try{
                weponHandeler other = collision.gameObject.GetComponent<weponHandeler>();
                other.weponsIn.Remove(this.gameObject);
            }
            catch
            {

            }
        }
    }

    public bool inWepon()
    {
        if(weponsIn .Count > 0)
        {
            return true;
        }
        else
        {
            return false;
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
