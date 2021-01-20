using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandeler : MonoBehaviour
{
    
    public void menuOff(GameObject off)
    {
        off.SetActive(false);
    }

    public void menuOn(GameObject on)
    {
        on.SetActive(true);
    }

    public void test(string test)
    {
        debugLogConsole.uiLog(test);
    }

    public void quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
