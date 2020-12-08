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
        debugLogConsole.uiLog(this.transform.root.gameObject.name+ " active: " + this.active.ToString() + " side: " +this.side.ToString()+ " height: " + this.height.ToString());
    }

    //wepon colider handeler
    private void OnCollisionEnter(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject; // get the wepon part that hit

        if (collision.gameObject.layer.ToString() == "enamy")
        {
            this.active = false;
            if (myCol.tag != "blunts") //the blut is used only defensively
            {
                if (myCol.tag == "pummels") // pummel has a saepecel effect
                {
                    if (collision.gameObject.tag == "head")//stun
                    {

                    }

                    else if (collision.gameObject.tag == "leftSide" || collision.gameObject.tag == "rightSide") //push
                    {

                    }
                }

                else
                {
                    if (side != Side.none && height != Height.none)//makes sure wepon is active
                    {
                        if (side == Side.left) // checks side is active
                        {
                            switch (height) // checks hight active
                            {
                                case Height.top:
                                    {
                                        break;
                                    }
                                case Height.mid:
                                    {
                                        break;
                                    }
                                case Height.bot:
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        debugLogConsole.uiLog("left height error");
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            switch (height)
                            {
                                case Height.top:
                                    {
                                        break;
                                    }
                                case Height.mid:
                                    {
                                        break;
                                    }
                                case Height.bot:
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        debugLogConsole.uiLog("right height error");
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ding" + other.name);
        chargBoxSetter chargBox = other.GetComponent<chargBoxSetter>();
        active = true;
        height = chargBox.height;
        side = chargBox.side;
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
