using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonHandeler : MonoBehaviour
{
    OptionHolder options;
    private void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
    }

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
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void setHight()
    {
        GameObject eyes = GameObject.Find("XR Rig").GetComponent<XRRig>().cameraGameObject;
        Vector3 headCenter = (eyes.transform.localPosition + ((eyes.transform.forward * -1) * 0.1f));
        options.height = headCenter.y + 0.40f;
        //height.text = options.height + "M";
    }
}
