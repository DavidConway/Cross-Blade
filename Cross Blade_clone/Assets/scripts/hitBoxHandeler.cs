using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class hitBoxHandeler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        GameObject colitionPoint = collision.collider.gameObject;
        WeponSound wepSound = colitionPoint.GetComponentInParent<WeponSound>();
        try
        {
            weponHandeler wepon = collision.gameObject.GetComponentInParent<weponHandeler>();
            //debugLogConsole.uiLog(colitionPoint.gameObject.tag + " "+ wepon.active.ToString());
            if (wepon.active)
            {
                if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enamyWepon"))
                {
                    if (colitionPoint.tag != "blunts") //the blut is used only defensively
                    {
                        wepon.active = false;
                        bool playsound = true;

                        //debugLogConsole.uiLog(wepon.gameObject.name + " deactive " + myCol.tag + " " + colitionPoint.tag);
                        if (colitionPoint.tag == "pummels") // pummel has a saepecel effect
                        {
                            if (myCol.tag == "head")//stun
                            {
                                debugLogConsole.uiLog("headBash");
                            }
                            else if (myCol.tag == "leftSide" || myCol.tag == "rightSide") //push
                            {
                                debugLogConsole.uiLog(myCol.tag + " bunt");

                            }
                        }
                        else if (colitionPoint.tag == "points")
                        {
                            debugLogConsole.uiLog(myCol.tag + " stab");
                        }
                        else
                        {
                            if (wepon.side != Side.none && wepon.height != Height.none)//makes sure wepon is active
                            {
                                if (wepon.side == Side.left) // checks side is active
                                {
                                    switch (wepon.height) // checks hight active
                                    {
                                        case Height.top:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "rightArm"
                                                   || myCol.tag == "head" || myCol.tag == "leftSholder"
                                                   || myCol.tag == "rightSholder" || myCol.tag == "rightSide")
                                                {
                                                    debugLogConsole.uiLog("top left slash");
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("top left slash failed");
                                                    playsound = false;
                                                    break;
                                                }

                                            }
                                        case Height.mid:
                                            {
                                                if (myCol.tag == "rightArm" || myCol.tag == "head" ||
                                                    myCol.tag == "rightSide" || myCol.tag == "rightLeg")
                                                {
                                                    debugLogConsole.uiLog("mid left slash");
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("mid left slash failed");
                                                    playsound = false;
                                                    break;
                                                }
                                            }
                                        case Height.bot:
                                            if (myCol.tag == "rightArm" || myCol.tag == "rightSide" ||
                                                myCol.tag == "rightLeg")
                                            {
                                                debugLogConsole.uiLog("bot left slash");
                                                break;
                                            }
                                            else
                                            {
                                                debugLogConsole.uiLog("bot left slash failed");
                                                playsound = false;
                                                break;
                                            }
                                    }
                                }

                                else if (wepon.side == Side.right)
                                {

                                    switch (wepon.height) // checks hight active
                                    {
                                        case Height.top:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "rightArm"
                                                   || myCol.tag == "head" || myCol.tag == "leftSholder"
                                                   || myCol.tag == "rightSholder" || myCol.tag == "leftSide")
                                                {
                                                    debugLogConsole.uiLog("top right slash");
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("top right slash failed");
                                                    playsound = false;
                                                    break;
                                                }

                                            }
                                        case Height.mid:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "head" ||
                                                    myCol.tag == "leftSide" || myCol.tag == "leftLeg")
                                                {
                                                    debugLogConsole.uiLog("mid right slash");
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("mid right slash failed");
                                                    playsound = false;
                                                    break;
                                                }
                                            }
                                        case Height.bot:
                                            if (myCol.tag == "leftArm" || myCol.tag == "leftSide" ||
                                                myCol.tag == "leftLeg")
                                            {
                                                debugLogConsole.uiLog("bot right slash");
                                                break;
                                            }
                                            else
                                            {
                                                debugLogConsole.uiLog("bot right slash failed");
                                                playsound = false;
                                                break;
                                            }
                                    }
                                }

                                else if (wepon.side == Side.chop && wepon.height == Height.chop)
                                {
                                    debugLogConsole.uiLog(myCol.tag);

                                }
                            }
                        }
                        if (playsound)
                        {
                            wepSound.PlayHit();
                        }
                        else
                        {
                            wepSound.PlayBlock();
                        }
                    }
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
