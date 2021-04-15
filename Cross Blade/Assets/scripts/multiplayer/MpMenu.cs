using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MpMenu : MonoBehaviour
{
    OptionHolder options;
    SceanLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        loader = GameObject.Find("constData").GetComponent<SceanLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void host()
    { 
        options.isHost = true;
    }

    public void cliant()
    {
        options.isHost = false;
    }

    public void load()
    {
        options.SetPlayerHeight();
        loader.LoadScene("VoidMP");
    }


}
