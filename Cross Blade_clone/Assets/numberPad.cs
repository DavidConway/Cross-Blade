using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberPad : MonoBehaviour
{
    // Start is called before the first frame update
    static string text;
    static bool enterPressed;

    private void OnEnable()
    {
        text = "";
    }

    public void type(string i)
    {
        text = text + i;
    }

    public void backSpace()
    {
        if (text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
    }

    static public string getText()
    {
        return text;
    }

    private void Update()
    {
    }

    public void pressedEnter()
    {
        enterPressed = true;
    }

    static public bool getEnter()
    {
        bool outP = enterPressed;
        enterPressed = false;
        return outP;
    }
}
