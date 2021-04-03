﻿using MLAPI;
using MLAPI.Spawning;
using MLAPI.Transports.UNET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using System.Net;
using System;

public class Mpload : MonoBehaviour
{
    public GameObject player;
    public float startTime;
    UnityEngine.Ping testPing;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        testPing = new UnityEngine.Ping(GameObject.Find("MPManager").GetComponent<UnetTransport>().MLAPIRelayAddress);//pings relay server

        startTime = Time.time;
        OptionHolder options = GameObject.Find("constData").GetComponent<OptionHolder>();
        UnetTransport connection = GameObject.Find("MPManager").GetComponent<UnetTransport>();

        if (options.isHost)
        {

            // geives a unic port so multiple hosts can be done from one network
            int port = getPort();
            connection.ConnectPort = port;
            connection.ServerListenPort = port;

            NetworkingManager.Singleton.StartHost();
        }
        else
        {
            connection.ConnectPort = options.port;
            connection.ServerListenPort = options.port;
            connection.ConnectAddress = options.ip;
            NetworkingManager.Singleton.StartClient();
        }
    }

    private int getPort()
    {
        int i;
        UnityEngine.Random.InitState((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
        i = UnityEngine.Random.Range(1000,65000);

        return i;
    }

    // Update is called once per frame
    void Update()
    {
        if(!NetworkingManager.Singleton.IsConnectedClient && !NetworkingManager.Singleton.IsServer)
        {
           
            if (Time.time - startTime > 5)
            {
                //checks if conection fail was a lack of relay
                if (testPing.isDone)
                {
                    print("ding");
                }
                else
                {
                    print("dong");
                }


                NetworkingManager.Singleton.StopClient();
                GameObject.Find("constData").GetComponent<SceanLoader>().LoadScene("mainMenu");
            }
        }
    }
}
