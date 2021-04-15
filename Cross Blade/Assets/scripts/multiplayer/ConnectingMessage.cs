using MLAPI;
using MLAPI.Transports.UNET;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class ConnectingMessage : MonoBehaviour
{
    static OptionHolder options;
    static public TextMeshProUGUI text;
    static public GameObject outOfPlay;
    private GameObject outOfPlaySetup;
    static bool needNetwork = true;
    void Start()
    {
        text = this.GetComponentInChildren<TextMeshProUGUI>();
        text.color = Color.white;
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        outOfPlaySetup = GameObject.Find("preConnect");
        outOfPlay = outOfPlaySetup;
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

    static public void back()
    {
        try
        {
            stopNet();
        }
        catch
        {

        }
            
        options.gameObject.GetComponent<SceanLoader>().LoadScene("mainMenu");
    }
    public void backButtonHook()
    {
        back();
    }

    static public void ConnectionFailed(string Message)
    {
        if (needNetwork)
        {
            GameObject[] allMP = GameObject.FindGameObjectsWithTag("mpNode");
            foreach (GameObject i in allMP)
            {
                i.SetActive(false);
            }
            outOfPlay.SetActive(true);
            text.text = Message;
        }

    }

    static void stopNet()
    {
        try
        {
            if (options.isHost)
            {
                NetworkingManager.Singleton.StopHost();
            }
            else
            {
                NetworkingManager.Singleton.StopClient();
            }
        }
        catch
        {

        }
    }

    static public void victory()
    {
        gameBordClear();
        outOfPlay.SetActive(true);
        text.text = "Victory";
        text.color = Color.green;
        needNetwork = false;
    }

    static public void defeat()
    {
        gameBordClear();
        outOfPlay.SetActive(true);
        text.text = "Defeat";
        text.color = Color.red;
        needNetwork = false;
    }

    static private void gameBordClear()
    {
        List<GameObject> allMP = new List<GameObject>();
        allMP.AddRange(GameObject.FindGameObjectsWithTag("mpNode"));
        allMP.AddRange(GameObject.FindGameObjectsWithTag("weaponRoot"));
        foreach (GameObject i in allMP)
        {
            i.SetActive(false);
        }
    }
    
}
