using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargBoxSetter : MonoBehaviour
{
    public Height height;
    public Side side;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        weponHandeler wepon = other.GetComponentInParent<weponHandeler>();
        if (other.gameObject.tag == "handel")
        {
            wepon.active = true;
            wepon.height = height;
            wepon.side = side;
        }
    }
}


