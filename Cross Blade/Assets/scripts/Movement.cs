using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class Movement : LocomotionProvider
{
    private List<InputDevice> inputHold = new List<InputDevice>();
    private InputDevice rightHand;
    private InputDevice leftHand;

    private OptionHolder options;

    private CharacterController character = null;
    private GameObject eyes = null;
    private Vector3 headCenter;

   // public GameObject test;

    public bool useRightHand;

    public bool useHeadDirection;

    // Start is called before the first frame update
    protected override void Awake()
    {
        character = this.GetComponent<CharacterController>(); //gets charicter controler
        eyes = this.GetComponent<XRRig>().cameraGameObject; //gets camera(players head)
    }

    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        character.height = options.height;
        updateHeadPos();
        useHeadDirection = options.moveByLooking;
        useRightHand = options.leftHanded;

    }

    // Update is called once per frame
    void Update()
    {
        updateHeadPos();
        Move();
    }

    void updateHeadPos() // updatex the heads position and the player controler
    {
        headCenter = (eyes.transform.localPosition + ((eyes.transform.forward * -1) * 0.1f));
        character.center = new Vector3(headCenter.x, (options.height / 2) + character.skinWidth, headCenter.z);
        //test.transform.position = eyes.transform.position + ((eyes.transform.forward * -1) * 0.1f);
    }

    void Move()
    {
        Vector3 direction;
        Vector3 rotation;

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, inputHold); // sets hands
        rightHand = inputHold[0];
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, inputHold);
        leftHand = inputHold[0];

        //gets the hand being used and get the direction the dpad is pointed. if no controler if found direction is zero
        if (rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 baseDirection) && useRightHand)
        {
            direction = new Vector3(baseDirection.x, 0, baseDirection.y);

        }
        else if(leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 baseDirection2))
        {
            direction = new Vector3(baseDirection2.x, 0, baseDirection2.y);
        }
        else
        {
            direction = Vector3.zero;
        }

        // checks to see if head or hand should be used for direcrtion
        if (useRightHand && rightHand.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion baseRotation) && !useHeadDirection )
        {
            rotation = new Vector3(0, baseRotation.eulerAngles.y, 0);

        }
        else if (!useRightHand && leftHand.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion baseRotation2) && !useHeadDirection)
        {
            rotation = new Vector3(0, baseRotation2.eulerAngles.y, 0);
        }
        else if (useHeadDirection)
        {
            rotation = new Vector3(0, eyes.transform.eulerAngles.y, 0);
        }
        else
        {
            rotation = Vector3.zero;
        }

        direction = Quaternion.Euler(rotation) * direction; //cobines direction and rotation

        character.Move((direction*2)*Time.deltaTime); //applyes speed

    }
}
