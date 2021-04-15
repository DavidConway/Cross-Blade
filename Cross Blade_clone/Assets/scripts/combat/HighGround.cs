
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighGround : MonoBehaviour
{
    [SerializeField]
    GameObject other;
    [SerializeField]
    GameObject root;
    void Start()
    {
        root = this.transform.parent.parent.gameObject;
        other = tryGetOther();
    }

    // Update is called once per frame
    void Update()
    {
        if (other == null)
        {
            other = tryGetOther();
            return;
        }

        if (this.transform.position.y - other.transform.position.y > this.transform.parent.GetComponentInChildren<CharacterController>().height / 2) // checks to see if enamy is lower then the player by half the player height (the enamy is crouched)
        {
            root.GetComponent<MultiplayerWeponStart>().left.GetComponent<weponHandeler>().highGround.Value = true; //tells wepon it has the highground
            root.GetComponent<MultiplayerWeponStart>().right.GetComponent<weponHandeler>().highGround.Value = true;
        }
        else
        {
            root.GetComponent<MultiplayerWeponStart>().left.GetComponent<weponHandeler>().highGround.Value = false;
            root.GetComponent<MultiplayerWeponStart>().right.GetComponent<weponHandeler>().highGround.Value = false;
        }
    }

    private GameObject tryGetOther()
    {
        foreach(GameObject i in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if(i.name == "head")
            {
                if(i.transform.parent.parent.gameObject != this.transform.parent.parent.gameObject)
                {
                    return i;
                }
            }
        }
        return null;
    }
}
