using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class optionsScript : MonoBehaviour
{

    [SerializeField]
    Toggle leftHand;
    [SerializeField]
    Toggle moveDirection;
    [SerializeField]
    /*TMPro.TMP_Text height;
    [SerializeField]*/
    Movement current;
    OptionHolder options;
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        current = GameObject.Find("XR Rig").GetComponent<Movement>();
        leftHand.isOn = options.leftHanded;
        moveDirection.isOn = options.moveByLooking;


        leftHand.onValueChanged.AddListener(delegate { leftHandToggel(leftHand); });
        moveDirection.onValueChanged.AddListener(delegate { moveChange(moveDirection);});
        //height.text = options.height + "M";
    }

    // Update is called once per frame
    void Update()
    {
    }

    void leftHandToggel(Toggle i)
    {
        options.leftHanded = i.isOn;
        current.useRightHand = i.isOn;
    }

    void moveChange(Toggle i)
    {
        options.moveByLooking = i.isOn;
        current.useHeadDirection = i.isOn;
    }

    public void setHight()
    {
        GameObject eyes = GameObject.Find("XR Rig").GetComponent<XRRig>().cameraGameObject;
        Vector3 headCenter = (eyes.transform.localPosition + ((eyes.transform.forward * -1) * 0.1f));
        options.height = headCenter.y + 0.25f;
        //height.text = options.height + "M";
    }
}
