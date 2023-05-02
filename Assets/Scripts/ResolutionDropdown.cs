using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    
    public TMP_Text currentResolution;
    public int resIndex;
    public TMP_Dropdown dropdown;


    private void Start()
    {

        bool foundRes = false;
        foreach(TMP_Dropdown.OptionData option in dropdown.options)
        {
            if(option.text == Screen.width + " x " + Screen.height)
            {
                foundRes = true;
            }
        }

        if(!foundRes)
        {
            string newRes = Screen.width + " x " + Screen.height;
            List<string> list = new List<string>{newRes};
            dropdown.AddOptions(list);
        }

        currentResolution.text = Screen.width + " x " + Screen.height;
        dropdown.value = dropdown.options.Count - 1;
    }

    public void SetResolution(int index)
    {
        resIndex = index;
    }

}
