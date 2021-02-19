using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OptionHolder : MonoBehaviour
{

    public bool leftHanded = false;
    public bool moveByLooking = false;
    public float height = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPlayerHeight()
    {
        GameObject eyes = GameObject.Find("XR Rig").GetComponent<XRRig>().cameraGameObject;
        Vector3 headCenter = (eyes.transform.localPosition + ((eyes.transform.forward * -1) * 0.1f));
        height = headCenter.y + 0.15f;
    }

    // Update is called once per frame

}
