﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camDirection : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position = new Vector3(cam.transform.position.x,0,cam.transform.position.z);

    }
}
