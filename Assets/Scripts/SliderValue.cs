using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    public Slider slider;
    public TMP_Text text;
    public bool isPercent;

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString();
        if(isPercent)
        {
            text.text += "%";
        }
    }
}
