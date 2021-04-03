using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleStart : MonoBehaviour
{
    public GameObject player;
    OptionHolder options;
    [SerializeField]
    private SkinnedMeshRenderer playerBox = null;
    float modelHeight = 0;
    public bool spawned = false;
    public float scale = 0;
    public int connected =  0;
    public bool singel = false;

    void Start()
    {
        this.transform.position = new Vector3(0, 0, 0);
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        //modelHeight = playerBox.bounds.size.y; //gets hight of model being used
        //scale = options.height / modelHeight; //gets scale
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            if (scale != 0)
            {
                player.SetActive(true);
                spawned = true;
            }
        }
    }
}
