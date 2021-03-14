using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class weponSelect : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown wepon;
    public bool leftHand = false;
    OptionHolder options;
    // Start is called before the first frame update
    void Start()
    {
        options = GameObject.Find("constData").GetComponent<OptionHolder>();
        List<string> levels = new List<string> { "Proto Sword" , "Proto Shield"};
        wepon.AddOptions(levels);

        updateSelect(0);

        wepon.onValueChanged.AddListener(delegate { dropChange(wepon); });
    }

    private void updateSelect(int v)
    {
        if (leftHand)
        {
            options.leftWepon = v;
        }
        else
        {
            options.rightWepon = v;
        }
    }


    private void dropChange(TMP_Dropdown wepon)
    {
        int newShow = wepon.value;
        updateSelect(newShow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
