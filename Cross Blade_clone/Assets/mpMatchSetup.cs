using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class mpMatchSetup : MonoBehaviour
{

    [SerializeField]
    private TMP_Dropdown mode;

    [SerializeField]
    private TMP_InputField hits;


    private bool valed = false;
    OptionHolder options;
    public GameObject numpad;
    private bool activeText = false;

    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        List<string> modes = new List<string> { "First To", "Best Of" };
        mode.AddOptions(modes);

        updateSelect(0);

        mode.onValueChanged.AddListener(delegate { dropChange(mode); });

        hits.onValueChanged.AddListener(delegate { textChanged(hits); });
    }

    private void textChanged(TMP_InputField hits)
    {
        TextMeshProUGUI textCol = GameObject.Find("numText").GetComponent<TextMeshProUGUI>();
        textCol.color = Color.black;
        try
        {
            if((Modes)mode.value == Modes.FirstTo)
            {
                options.hitTo = int.Parse(hits.text);
                valed = true;
            }
            else
            {
                if (int.Parse(hits.text) % 2 == 1)
                {
                    options.hitTo = int.Parse(hits.text);
                    valed = true;
                }
                else
                {
                    textCol.color = Color.red;
                    valed = false;
                }
            }
        }
        catch
        {
            textCol.color = Color.red;
            valed = false;
        }
    }

    private void updateSelect(int v)
    {
        options.mode = (Modes)v;
    }


    private void dropChange(TMP_Dropdown mode)
    {
        int newShow = mode.value;
        updateSelect(newShow);
    }

   public void trySwap(GameObject on)
    {
        if (valed && !activeText)
        {
            on.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (hits.isFocused || activeText)
        {
            if (!numpad.activeSelf) //turns on numpad
            {
                activeText = true;
                numpad.SetActive(true);
            }
            hits.text = numberPad.getText();//updates info
            options.ip = numberPad.getText();
            if (numberPad.getEnter()) // disabel numpad if enter is pressed
            {
                activeText = false;
                numpad.SetActive(false);
            }
        }
    }

}
