using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public GameObject option;

    public Toggle fullscreen;
    public Toggle vsync;
    public Toggle postProcessing;
    public GameObject blur;
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

    public TMP_Text textCurrentMission;
    public Toggle skipToBoss;

    private GameManager gameManager;

    private GetLatestMission glm;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        glm = FindObjectOfType<GameManager>().GetComponent<GetLatestMission>();
        if (blur == null)
        {
            blur = gameManager.transform.Find("Blur").gameObject;
        }
        fullscreen.isOn = Screen.fullScreen;
        vitaInfinitaToggle.isOn = gameManager.vitaInfinita;
        postProcessing.isOn = blur.activeSelf;
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

    private void Update()
    {
        textCurrentMission.text = "Missione Attuale: " + (glm.GetCurrentMission() + 1);
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
        gameManager.postProcessing = postProcessing.isOn;
        blur.SetActive(postProcessing.isOn);

        if (skipToBoss.isOn)
        {
            GameObject.Find("GoToArenaCanvas").transform.GetChild(0).gameObject.SetActive(true);
        }

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

    public void SkipMission()
    {
        FirstPersonController fpc = Resources.FindObjectsOfTypeAll<FirstPersonController>()[0];
        GetLatestMission glm = FindObjectOfType<GameManager>().GetComponent<GetLatestMission>();
        if(glm.GetCurrentMission() < 7)
        {
            if(glm.GetCurrentMission() == 2) {
                fpc.availableWeapons.Add(1);
                Resources.FindObjectsOfTypeAll<HUDInventoryWeapon>()[0].SetInventory(fpc.GetInventory(), fpc.GetAvailableWeapons());
                glm.SetCurrentMission(4);
            }
            else
            {
                glm.SetCurrentMission(glm.GetCurrentMission() + 1);
            } 
            if(SceneManager.GetActiveScene().name == "TestALessioMappa")
            {
                //StartCoroutine(SearchSpawnPoint());
                GameObject.Find("MissionSkipper").GetComponent<MissionSkipper>().MissionSkipperLaunch();
            }
            
            
        }
    }

    /*
    IEnumerator SearchSpawnPoint()
    {
        while(SceneManager.GetActiveScene().name != "TestALessioMappa")
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        while(PauseMenu.gameIsPaused)
        {
            yield return null;
        }
        FindObjectOfType<FirstPersonController>().transform.position = GameObject.Find("PlayerSpawnPoint").transform.position;
    }*/

}
