using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weponHandeler : MonoBehaviour
{
    public Side side;
    public Height height;
    public bool active = false;
    void Start()
    {
        side = Side.none;
        height = Height.none;
    }

    // Update is called once per frame
    void Update()
    {
        //debugLogConsole.uiLog(gameObject.name+ " active: " + this.active.ToString() + " side: " +this.side.ToString()+ " height: " + this.height.ToString());
    }

    //wepon colider handeler
    private void OnCollisionEnter(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        if (collision.gameObject.layer.ToString() == "enamyWepon")
        {
            if (myCol.tag == "blunts")
            {

            }
        }

    }

}

public enum Side
{
    none,
    left,
    right,
    chop
}

public enum Height
{
    none,
    top,
    mid,
    bot,
    chop
}
