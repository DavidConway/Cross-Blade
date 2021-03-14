using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mpload : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {

        OptionHolder options = GameObject.Find("constData").GetComponent<OptionHolder>();
        if (options.isHost)
        {
            NetworkingManager.Singleton.StartHost();
        }
        else
        {
            NetworkingManager.Singleton.StartClient();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
