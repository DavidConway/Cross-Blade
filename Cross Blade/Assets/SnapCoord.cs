using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCoord : MonoBehaviour
{
    //overides a objects coword
    public bool snapY;
    public bool snapX;
    public bool snapZ;
    public float y;
    public float x;
    public float z;

    private void Update()
    {
        if (snapY)
        {
            this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
        }

        if (snapX)
        {
            this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
        }

        if (snapZ)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, z);
        }
    }
}
