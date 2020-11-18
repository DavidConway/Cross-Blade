using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weponHandeler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject myCol = collision.contacts[0].thisCollider.gameObject;
        Debug.Log(myCol.tag);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject myCol = this.gameObject;
        Debug.Log(myCol.tag);
    }
}
