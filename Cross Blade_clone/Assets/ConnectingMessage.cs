using MLAPI.Transports.UNET;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class ConnectingMessage : MonoBehaviour
{
    OptionHolder options;
    public TextMeshProUGUI text;
    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        if (options.isHost)
        {
            string i = new WebClient().DownloadString("http://checkip.dyndns.org/"); // gets web string with ip in it
            int startPoint = i.IndexOf(":") + 1;
            int endPoint = i.IndexOf("<", startPoint);
            i = i.Substring(startPoint, endPoint - startPoint);

            UnetTransport connection = GameObject.Find("MPManager").GetComponent<UnetTransport>();
            string info = "IP: " + i + "\n" + "Relay Port: " + GameObject.Find("MPManager").GetComponent<UnetTransport>().ConnectPort;
            text.text = info;
        }
        else
        {
            text.text = "Connecting...";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
