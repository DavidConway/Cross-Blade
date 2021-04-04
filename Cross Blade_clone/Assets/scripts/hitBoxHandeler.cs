using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class hitBoxHandeler : MonoBehaviour
{
    public mpHitCounter hitCounter;
    private bool hitting = false;

    private float hitTime = 0;
    bool playsound = false;
    WeponSound wepSound;
    private bool enamyWep;

  


    private void Update()
    {
        if (hitTime >= 0)
        {
            hitTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        playsound = false;
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        GameObject colitionPoint = collision.collider.gameObject;
        wepSound = colitionPoint.GetComponentInParent<WeponSound>();
        try
        {
            if (hitTime <= 0) {
                weponHandeler wepon = collision.gameObject.GetComponentInParent<weponHandeler>();
                if (wepon.active.Value && !wepon.inWepon)
                {
                    if (wepon.highGround.Value)
                    {
                        wepon.height.Value -= 1;
                        if (wepon.height.Value <= 0)
                        {
                            wepon.height.Value = 1;
                        }
                    }

                    enamyWep = false;
                    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("enamyWepon"))
                    {
                        enamyWep = true;
                    }

                    if (colitionPoint.tag != "blunts") // blunts is defensive only
                    {

                        if (enamyWep)
                        {
                            wepon.active.Value = false;
                        }

                        hitting = true; // make sure only one hit is being checked at a time;

                        if (colitionPoint.tag == "points")
                        {
                            hit();
                            return;
                        }

                        else if (colitionPoint.tag == "edges") //if blade is hit
                        {
                            //if attack is from high ground treat it as if it is freom one height up
                            if (wepon.highGround.Value)
                            {
                                wepon.height.Value -= 1;
                                if (wepon.height.Value <= 0)
                                {
                                    wepon.height.Value = 1;
                                }
                            }

                            if (wepon.side.Value != (int)Side.none && wepon.height.Value != (int)Height.none)//makes sure wepon is active
                            {
                                if (wepon.side.Value == (int)Side.left)
                                {

                                    switch (wepon.height.Value) // checks hight active
                                    {
                                        case (int)Height.top:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "rightArm"
                                                   || myCol.tag == "head" || myCol.tag == "leftSholder"
                                                   || myCol.tag == "rightSholder" || myCol.tag == "rightSide")
                                                {
                                                    hit();
                                                    return;
                                                }
                                                else
                                                {
                                                    block();
                                                    return;
                                                }

                                            }
                                        case (int)Height.mid:
                                            {
                                                if (myCol.tag == "rightArm" || myCol.tag == "head" ||
                                                    myCol.tag == "rightSide" || myCol.tag == "rightLeg")
                                                {
                                                    hit();
                                                    return;
                                                }
                                                else
                                                {
                                                    block();
                                                    return;
                                                }
                                            }
                                        case (int)Height.bot:
                                            {
                                                if (myCol.tag == "rightArm" || myCol.tag == "rightSide" ||
                                                    myCol.tag == "rightLeg")
                                                {
                                                    hit();
                                                    return;
                                                }
                                                else
                                                {
                                                    block();
                                                    break;
                                                }
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
                                                    hit();
                                                    return;
                                                }
                                                else
                                                {
                                                    block();
                                                    return;
                                                }

                                            }
                                        case (int)Height.mid:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "head" ||
                                                    myCol.tag == "leftSide" || myCol.tag == "leftLeg")
                                                {
                                                    hit();
                                                    return;
                                                }
                                                else
                                                {
                                                    block();
                                                    return;
                                                }
                                            }
                                        case (int)Height.bot:
                                            {
                                                if (myCol.tag == "leftArm" || myCol.tag == "leftSide" ||
                                                    myCol.tag == "leftLeg")
                                                {
                                                    hit();
                                                    return;
                                                }
                                                else
                                                {
                                                    block();
                                                    return;
                                                }
                                            }
                                    }

                                }

                                else if (wepon.side.Value == (int)Side.chop)
                                {
                                    if (myCol.tag == "head" || myCol.tag == "leftArm" || myCol.tag == "rightArm" ||
                                       myCol.tag == "leftSholder" || myCol.tag == "rightSholder")
                                    {
                                        hit();
                                        return;
                                    }
                                    else
                                    {
                                        block();
                                        return;
                                    }
                                }
                            }
                        }

                        hitting = false;
                    }

                }
            }
        }

        catch
        {
        }
    }

    void playSound()
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

    void hit()
    {
        playsound = true;
        if (enamyWep)
        {
            hitCounter.countHit();
        }
        hitTime = 1;
        hitting = false;
        playSound();
    }

    void block()
    {
        playsound = false;
        hitting = false;
        playSound();
    }
}
