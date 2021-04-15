using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enamyTraking : MonoBehaviour
{
    public bool isEnamy = false;
    private GameObject otherHead;
    private GameObject otherFeet;
    private float otherHight;

    //testing
    public bool highGround;
    public Pos pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (otherHead == null || otherHight == 0)
        {
            if (isEnamy)
            {
                otherHead = (getBasedLayer(LayerMask.NameToLayer("playerCore"))[0]);
                otherHight = GameObject.Find("constData").GetComponent<OptionHolder>().height - 0.15f;
            }
            else
            {
                otherHead = (getBasedLayer(LayerMask.NameToLayer("enamyCore"))[0]);
                foreach (Transform child in otherHead.transform.parent.transform)
                {
                    if (child.gameObject.name == "body")
                    {
                        otherFeet = child.gameObject;
                    }
                }
                otherHight = otherHead.transform.position.y - otherFeet.transform.position.y;
            }
        }
        else //testing remove later;
        {
            highGround = checkHighGround();
            pos = checkSide();
        }
    }

    public Pos checkSide()
    {
        if (otherHead != null)
        {
            float angel = Vector3.SignedAngle(otherHead.transform.position - this.transform.position, this.transform.forward, Vector3.up);
            if (angel < -60 && angel > -120)
            {
                return Pos.Right;
            }
            else if (angel > 60 && angel < 120)
            {
                return Pos.Left;
            }
            else if (angel >= -60 && angel <= 60)
            {
                return Pos.Front;
            }
            else
            {
                return Pos.Back;
            }
        }
        else
        {
            return Pos.Front;
        }
    }

    public bool checkHighGround()
    {
        if (otherHead != null)
        {
            if ((otherHead.transform.position.y - this.transform.position.y) > (otherHight / 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    List<GameObject> getBasedLayer(int layer)
    {
        List<GameObject> found = new List<GameObject>();
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject i in allObjects)
        {
            if(i.gameObject.layer == layer)
            {
                found.Add(i);
            }
        }
        return found;
    }
}

public enum Pos
{
    Front,
    Back,
    Left,
    Right
}
