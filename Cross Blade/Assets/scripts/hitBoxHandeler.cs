using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class hitBoxHandeler : MonoBehaviour
{
    public mpHitCounter hitCounter;

    private void OnCollisionEnter(Collision collision)
    {
        bool playsound = false;
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        GameObject colitionPoint = collision.collider.gameObject;
        WeponSound wepSound = colitionPoint.GetComponentInParent<WeponSound>();
        try
        {
            weponHandeler wepon = collision.gameObject.GetComponentInParent<weponHandeler>();
            //debugLogConsole.uiLog(colitionPoint.gameObject.tag + " "+ wepon.active.ToString());
            if (wepon.active.Value)
            {
                playsound = true;
                //if attack is from high ground treat it as if it is freom one height up
                if (wepon.highGround.Value)
                {
                    wepon.height.Value -= 1;
                    if(wepon.height.Value <= 0)
                    {
                        wepon.height.Value = 1;
                    }
                }

                if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enamyWepon") && this.gameObject.layer == LayerMask.NameToLayer("player"))
                {
                    if (colitionPoint.tag != "blunts") //the blut is used only defensively
                    {
                        wepon.active.Value = false;
                        

                        //debugLogConsole.uiLog(wepon.gameObject.name + " deactive " + myCol.tag + " " + colitionPoint.tag);
                        if (colitionPoint.tag == "pummels") // pummel has a saepecel effect
                        {
                            /*if (myCol.tag == "head")//stun
                            {
                                debugLogConsole.uiLog("headBash");
                            }
                            else if (myCol.tag == "leftSide" || myCol.tag == "rightSide") //push
                            {
                                debugLogConsole.uiLog(myCol.tag + " bunt");

                            }*/
                            playsound = false;
                        }
                        else if (colitionPoint.tag == "points")
                        {
                            hitCounter.countHit();
                        }
                        else
                        {
                            if (wepon.side.Value != (int)Side.none && wepon.height.Value != (int)Height.none)//makes sure wepon is active
                            {
                                if (wepon.side.Value == (int)Side.left) // checks side is active
                                {
                                    switch (wepon.height.Value) // checks hight active
                                    {
                                        case (int)Height.top:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "rightArm"
                                                   || myCol.tag == "head" || myCol.tag == "leftSholder"
                                                   || myCol.tag == "rightSholder" || myCol.tag == "rightSide")
                                                {
                                                    hitCounter.countHit();
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("top left slash failed");
                                                    playsound = false;
                                                    break;
                                                }

                                            }
                                        case (int)Height.mid:
                                            {
                                                if (myCol.tag == "rightArm" || myCol.tag == "head" ||
                                                    myCol.tag == "rightSide" || myCol.tag == "rightLeg")
                                                {
                                                    hitCounter.countHit();
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("mid left slash failed");
                                                    playsound = false;
                                                    break;
                                                }
                                            }
                                        case (int)Height.bot:
                                            if (myCol.tag == "rightArm" || myCol.tag == "rightSide" ||
                                                myCol.tag == "rightLeg")
                                            {
                                                hitCounter.countHit();
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

                                else if (wepon.side.Value == (int)Side.right)
                                {

                                    switch (wepon.height.Value) // checks hight active
                                    {
                                        case (int)Height.top:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "rightArm"
                                                   || myCol.tag == "head" || myCol.tag == "leftSholder"
                                                   || myCol.tag == "rightSholder" || myCol.tag == "leftSide")
                                                {
                                                    hitCounter.countHit(); 
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("top right slash failed");
                                                    playsound = false;
                                                    break;
                                                }

                                            }
                                        case (int)Height.mid:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "head" ||
                                                    myCol.tag == "leftSide" || myCol.tag == "leftLeg")
                                                {
                                                    hitCounter.countHit();
                                                    break;
                                                }
                                                else
                                                {
                                                    debugLogConsole.uiLog("mid right slash failed");
                                                    playsound = false;
                                                    break;
                                                }
                                            }
                                        case (int)Height.bot:
                                            if (myCol.tag == "leftArm" || myCol.tag == "leftSide" ||
                                                myCol.tag == "leftLeg")
                                            {
                                                hitCounter.countHit();
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

                                else if (wepon.side.Value == (int)Side.chop && wepon.height.Value == (int)Height.chop)
                                {
                                    hitCounter.countHit();

                                }
                            }
                        }
                    }
                }

                if (this.gameObject.layer == LayerMask.NameToLayer("enamy"))
                {
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
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
}
