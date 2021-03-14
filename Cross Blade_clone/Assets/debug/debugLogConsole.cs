using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class debugLogConsole : MonoBehaviour
{

    public int max;
    private static int s_max = 10;
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    private static ArrayList test = new ArrayList();


    public static void uiLog(string inString)
    {
        test.Insert(0,inString);
        if (test.Count > s_max){ 
            test.RemoveAt(s_max);
        }
    }
    private void Awake()
    {
        s_max = max;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "";
        foreach (string i in test)
        {
            text.text += "\n"+i;
        }
    }


}
