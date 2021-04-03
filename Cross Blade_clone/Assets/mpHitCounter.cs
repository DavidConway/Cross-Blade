using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mpHitCounter : NetworkedBehaviour
{


    public NetworkedVarInt hits = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly },0);
    [SerializeField]
    private mpHitUI ui;
    private int lastVal = 0;

    private void Update()
    {
        if (lastVal != hits.Value || ui == null) // updates ui if hits changed
        {
            if(ui == null)
            {
                ui = GameObject.Find("Main Camera").GetComponentInChildren<mpHitUI>();
            }
            if (IsLocalPlayer)
            {
                ui.UpdateDisplay(hits.Value.ToString(), true);
                lastVal = hits.Value;
            }
            else
            {
                ui.UpdateDisplay(hits.Value.ToString(), false);
                lastVal = hits.Value;
            }
        }
    }

    public void countHit()
    {
        hits.Value++;
    }
}
