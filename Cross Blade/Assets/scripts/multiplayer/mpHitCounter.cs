using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mpHitCounter : NetworkedBehaviour
{


    public NetworkedVarInt hits = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly },0);
    private NetworkedVarInt hitsTo = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, 0);
    private NetworkedVarInt mode = new NetworkedVarInt(new NetworkedVarSettings { WritePermission = NetworkedVarPermission.OwnerOnly }, 0);
    [SerializeField]
    private mpHitUI ui;
    private int lastVal = 0;
    private static int enamyHits;
    private GameObject constData;
    static bool doCheck = false;


    private void Start()
    {
        constData = GameObject.Find("constData");
        if (IsServer)
        {
            OptionHolder options = constData.GetComponent<OptionHolder>();
            hitsTo.Value = options.hitTo;
            mode.Value = (int)options.mode;
        }
    }

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
                doCheck = true;
            }
            else
            {
                ui.UpdateDisplay(hits.Value.ToString(), false);
                lastVal = hits.Value;
                enamyHits = hits.Value;
                doCheck = true;
            }
        }
        if (IsLocalPlayer && doCheck) //if score has change have local play check for a win
        {
            checkWin();
            doCheck = false;
        }
    }

    public void countHit()
    {
        hits.Value++;
    }

    private void checkWin()
    {
        if (IsLocalPlayer)
        {
            if (mode.Value == (int)Modes.BestOf)
            {
                if (hits.Value > hitsTo.Value / 2)
                {
                    ConnectingMessage.defeat();
                }
                else if (enamyHits > hitsTo.Value / 2)
                {
                    ConnectingMessage.victory();
                }
            }
            else
            {
                if (hits.Value >= hitsTo.Value)
                {
                    ConnectingMessage.defeat();
                }
                else if (enamyHits >= hitsTo.Value)
                {
                    ConnectingMessage.victory();
                }
            }
        }
    }
}
