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
        if (other.gameObject.tag == "handel" && other.gameObject.layer == LayerMask.NameToLayer("playerWeapon"))
        {
            weponHandeler wepon = other.GetComponentInParent<weponHandeler>();
            WeponSound weponSound = other.GetComponentInParent<WeponSound>();
            if ((!wepon.active.Value || wepon.height.Value != (int)height || wepon.side.Value != (int)side) && !wepon.inWepon)
            {
                wepon.active.Value = true;
                wepon.height.Value = (int)height;
                wepon.side.Value = (int)side;
                weponSound.PlayCharge();
            }
        }
    }
}


