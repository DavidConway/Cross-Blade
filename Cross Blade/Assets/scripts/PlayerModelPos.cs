using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelPos : MonoBehaviour
{
    [SerializeField]
    private CharacterController player = null;

    [SerializeField]
    private SkinnedMeshRenderer playerBox = null;

    GameObject cam = null;
    float modelHeight = 0;


    void Start()
    {
        cam = GameObject.Find("Main Camera");
        modelHeight = playerBox.bounds.size.y; //gets hight of model being used
        float scale = player.height / modelHeight; //gets scale
        this.transform.localScale = new Vector3(scale, scale, scale); //applyes scale
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.gameObject.transform.position + player.center - (new Vector3(0,player.height/2,0));
        this.transform.rotation = Quaternion.Euler(0,cam.transform.eulerAngles.y,0);
    }
}
