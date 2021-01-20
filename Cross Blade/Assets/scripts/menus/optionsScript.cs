using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsScript : MonoBehaviour
{

    [SerializeField]
    Toggle leftHand;
    [SerializeField]
    Toggle moveDirection;
    [SerializeField]
    OptionHolder options;
    [SerializeField]
    Movement current;
    // Start is called before the first frame update
    void Start()
    {
        current = GameObject.Find("XR Rig").GetComponent<Movement>();
        leftHand.isOn = options.leftHanded;
        moveDirection.isOn = options.moveByLooking;

        leftHand.onValueChanged.AddListener(delegate { leftHandToggel(leftHand); });
        moveDirection.onValueChanged.AddListener(delegate { moveChange(moveDirection);});
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
}
