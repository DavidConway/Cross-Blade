using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mpInput : MonoBehaviour
{
    public TMP_InputField ip;
    public TMP_InputField port;
    private OptionHolder options;
    public GameObject numpad;
    private int activeSelect = 0;


    private void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
    }

    private void Update()
    {
        
        //gets whats in focus
        if (ip.isFocused)
        {
            activeSelect = 1;
        }
        if (port.isFocused)
        {
            activeSelect = 2;
        }

        switch (activeSelect)
        {
            case 0: //disabels numpad if non is active
                if (numpad.activeSelf)
                {
                    numpad.SetActive(false);
                }
                break;

            case 1:
                if (!numpad.activeSelf) //turns on numpad
                {
                    numpad.SetActive(true);
                }
                ip.text = numberPad.getText();//updates info
                options.ip = numberPad.getText();
                if (numberPad.getEnter()) // disabel numpad if enter is pressed
                {
                    activeSelect = 0;
                }
                break;

            case 2:
                if (!numpad.activeSelf)
                {
                    numpad.SetActive(true);
                }
                port.text = numberPad.getText();

                try // trys update port if it can not be parsed the text gose red
                {
                    if(GameObject.Find("portText").GetComponent<TextMeshProUGUI>().color == Color.red)
                    {
                        GameObject.Find("portText").GetComponent<TextMeshProUGUI>().color = Color.black;
                    }
                    options.port = int.Parse(numberPad.getText());
                }
                catch
                {
                    GameObject.Find("portText").GetComponent<TextMeshProUGUI>().color = Color.red;
                }

                if (numberPad.getEnter())
                {
                    activeSelect = 0;
                }
                break;
        }
    }
}
