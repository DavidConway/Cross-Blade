using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class ResetCoord : NetworkedBehaviour
{
    public GameObject cam;
    public Vector2 offcet = Vector2.zero;

    void Start()
    {
        reset();
    }


    public void reset()
    {
        if (IsLocalPlayer)
        {
            offcet = new Vector2(-cam.transform.localPosition.x, -cam.transform.localPosition.z); // offset of cam
            this.transform.GetChild(0).transform.localPosition = new Vector3(offcet.x, 0, offcet.y);
            if (IsHost)
            {
                this.transform.position = new Vector3(0, 0.60f, -3);
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                this.transform.position = new Vector3(0, 0.60f,3 );
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }
}
