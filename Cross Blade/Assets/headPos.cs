using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headPos : MonoBehaviour
{
    public GameObject head;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = head.transform.position + offset +((head.transform.forward)*-0.1f);
    }
}
