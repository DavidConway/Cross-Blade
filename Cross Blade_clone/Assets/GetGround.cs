using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGround : MonoBehaviour
{
    public LayerMask mask;
    public GameObject from;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(from.transform.position,Vector3.down, out hit, 100f, mask))
        {
            this.transform.position = hit.point;
        }
    }
}
