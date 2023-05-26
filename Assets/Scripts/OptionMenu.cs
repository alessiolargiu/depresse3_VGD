using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public GameObject option;

    public Toggle fullscreen;
    public Toggle vsync;
    public ResolutionDropdown resolutionDropdown;
    public Slider volume;
    public Slider sensibility;

    public HUDInventoryHelmet hudHelmet;
    public HUDInventoryChest hudChest;
    public HUDInventoryWeapon hudWeapon;
    public HUDInventoryShield hudShield;
    public HUDInventoryPotion hudPotion;

    public Toggle vitaInfinitaToggle;
    public Toggle staminaInfinitaToggle;
    public Toggle fullEquipToggle;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        fullscreen.isOn = Screen.fullScreen;
        vitaInfinitaToggle.isOn = gameManager.vitaInfinita;
        staminaInfinitaToggle.isOn = gameManager.staminaInfinita;
        fullEquipToggle.isOn = gameManager.fullEquip;

        if (QualitySettings.vSyncCount == 0)
        {
            vsync.isOn = false;
        }
        else
        {
            vsync.isOn = true;
        }
        volume.value = AudioListener.volume * 100;
    }

    // Update is called once per frame
    public void ApplySettings()
    {
        Debug.Log("Sto applicando le opzioni");
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
                Screen.SetResolution(2160, 1440, fullscreen.isOn);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, fullscreen.isOn);
                break;
            case 2:
                Screen.SetResolution(1280, 720, fullscreen.isOn);
                break;
        }

        gameManager.vitaInfinita = vitaInfinitaToggle.isOn;
        gameManager.staminaInfinita = staminaInfinitaToggle.isOn;
        gameManager.fullEquip = fullEquipToggle.isOn;
        gameManager.sensibilita = (int)sensibility.value;

        //FindObjectOfType<FirstPersonController>().mouseSens = gameManager.sensibilita;
        //FindObjectOfType<PlayerTarget>().xSpeed = gameManager.sensibilita;
        //FindObjectOfType<PlayerTarget>().ySpeed = gameManager.sensibilita;

        if (fullEquipToggle.isOn)
        {
            hudHelmet.RefreshInventoryItems();
            hudChest.RefreshInventoryItems();
            hudWeapon.RefreshInventoryItems();
            hudShield.RefreshInventoryItems();
            hudPotion.RefreshInventoryItems();
        }

        AudioListener.volume = (volume.value / 100);

        option.SetActive(false);

    }

}
