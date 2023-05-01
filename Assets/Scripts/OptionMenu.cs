using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

    public Toggle fullscreen;
    public Toggle vsync;
    public Dropdown resolution;
    public Slider volume;
    public Slider sensibility;
    public List<ResItem> resolutions = new List<ResItem>();

    // Start is called before the first frame update
    void Start()
    {
        fullscreen.isOn = Screen.fullScreen;
        if(QualitySettings.vSyncCount == 0)
        {
            vsync.isOn = false;
        }
        else
        {
            vsync.isOn = true;
        }
    }

    // Update is called once per frame
    public void ApplySettings()
    {
        Screen.fullScreen = fullscreen.isOn;
        if(vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal;
    public int vertical;
}