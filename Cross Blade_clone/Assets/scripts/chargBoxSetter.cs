using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargBoxSetter : MonoBehaviour
{
    public Height height;
    public Side side;
    // Start is called before the first frame update


    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "handel" && other.gameObject.layer == LayerMask.NameToLayer("playerWeapon"))
        {
            weponHandeler wepon = other.GetComponentInParent<weponHandeler>();
            WeponSound weponSound = other.GetComponentInParent<WeponSound>();
            //debugLogConsole.uiLog(other.gameObject.name + " side: " + wepon.side + " height: " + wepon.height);
            //debugLogConsole.uiLog("this" + " side: " + side + " height: " + height);
            if (!wepon.active || wepon.height != height || wepon.side != side)
            {
                wepon.active = true;
                wepon.height = height;
                wepon.side = side;
                weponSound.PlayCharge();
            }
        }
    }
}


