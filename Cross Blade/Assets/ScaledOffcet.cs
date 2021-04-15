using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledOffcet : MonoBehaviour
{
    private float scale;
    private OptionHolder options;
    public GameObject rist;
    public Vector3 rotOff;
    public Vector3 posOff;
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        // this.transform.localScale = new Vector3(scale, scale, scale); //applyes scal
        try
        {
            scale = this.transform.parent.parent.GetComponentInParent<MpStart>().scale.Value;
        }
        catch
        {
            //gets hight of model being used
            scale = options.scale; //gets scale*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        rist.transform.localRotation = Quaternion.Euler(rotOff);
        rist.transform.localPosition = posOff * scale;
    }
}
