using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWeponInHand : NetworkedBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            this.transform.position = this.transform.parent.position;
        }
    }
}
