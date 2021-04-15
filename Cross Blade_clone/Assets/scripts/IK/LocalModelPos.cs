using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI.NetworkedVar;
using UnityEngine.Animations.Rigging;
using static UnityEngine.Animations.Rigging.RigBuilder;

public class LocalModelPos : MonoBehaviour
{
    public bool debug = false;
    public float debugScale = 0.8f;
    public GameObject remote;


    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private SkinnedMeshRenderer playerBox = null;


    GameObject cam = null;
    float modelHeight = 0;

    private OptionHolder options;
    float scale;



    private void Awake()
    {

        if (!debug)
        {
            options = GameObject.Find("constData").GetComponent<OptionHolder>();
            // this.transform.localScale = new Vector3(scale, scale, scale); //applyes scal
            try
            {
                scale = this.transform.parent.parent.GetComponentInParent<MpStart>().scale.Value;
            }
            catch
            {
                modelHeight = playerBox.bounds.size.y; //gets hight of model being used
                scale = options.height / modelHeight; //gets scale*/
            }
            this.transform.localScale = new Vector3(scale, scale, scale);
            options.scale = scale;
            debugLogConsole.uiLog("scale: " +scale);
            debugLogConsole.uiLog("player.height: " + options.height);
            debugLogConsole.uiLog("modelHeight: " + modelHeight);
        }
        else
        {
            this.transform.localScale = new Vector3(debugScale, debugScale, debugScale); //applyes scale
            remote.transform.localScale = new Vector3(debugScale, debugScale, debugScale);
        }
    }
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        this.GetComponent<RigBuilder>().enabled = true;


    }

    // Update is called once per frame
    void Update() 
    {
        if(cam == null) // fail safe
        {
            cam = GameObject.Find("Main Camera");
        }
        Vector3 neck = player.transform.position + ((player.transform.forward * -1) * (0.1f*scale)) + ((player.transform.up * -1) * (0.1f*scale)); // places model root at players hight down from cam (kinda)
        this.transform.position = (neck) + (Vector3.down * (options.height - (0.45f * scale)));
        this.transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
    }
}
