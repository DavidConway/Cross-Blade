using MLAPI;
using MLAPI.NetworkedVar;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mpHitUI : NetworkedBehaviour
{

    public TextMeshProUGUI player;
    public TextMeshProUGUI enamy;

    public void UpdateDisplay(string newInt, bool isPlayer)
    {
        
        if (isPlayer)
        {
            enamy.text = newInt;
        }
        else
        {
            player.text = newInt;
        }
        this.transform.parent.GetComponentInParent<ResetCoord>().reset();
    }
}
