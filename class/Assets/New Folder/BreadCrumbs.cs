using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCrumbs : MonoBehaviour {

    Vector3 previousPosition;
    float counter = 0;
    public GameObject BC;
	// Use this for initialization
	void Start () {
        previousPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentLocation = transform.position;
        float distance = Vector3.Distance(previousPosition, currentLocation);
        if (distance > 1.0f)
        {
            previousPosition = currentLocation;
            GameObject g = (GameObject)Instantiate(BC, currentLocation, Quaternion.identity);
            g.name = "BC" + counter;
            counter++;


        }
		
	}
}
