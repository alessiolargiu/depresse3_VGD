using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

    public Toggle fullscreen;
    public Toggle vsync;
    public ResolutionDropdown resolutionDropdown;
    public Slider volume;
    public Slider sensibility;

    public GameManager gameManager;

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

        switch (resolutionDropdown.resIndex)
        {
            case 0:
                Screen.SetResolution(2160, 1440, fullscreen);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, fullscreen);
                break;
            case 2:
                Screen.SetResolution(1280, 720, fullscreen);
                break;
        }

        gameManager.sensibilità = (int)sensibility.value;

    }

}
