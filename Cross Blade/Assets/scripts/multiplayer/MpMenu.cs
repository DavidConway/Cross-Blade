using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MpMenu : MonoBehaviour
{
    OptionHolder options;
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
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
        SceneManager.LoadScene("VoidMP", LoadSceneMode.Single);
    }


}
