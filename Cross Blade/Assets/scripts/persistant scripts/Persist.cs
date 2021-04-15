using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour
{
    static GameObject constDataSingelton;
    void Start()
    {
        if (constDataSingelton == null)
        {
            DontDestroyOnLoad(this);
            constDataSingelton = this.gameObject;
        }
        else if(constDataSingelton != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        
    }
}
