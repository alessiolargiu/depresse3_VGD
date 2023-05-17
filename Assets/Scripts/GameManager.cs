using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int sensibilita;
    public bool vitaInfinita;
    public bool staminaInfinita;
    public bool fullEquip;
    private OptionMenu optionMenu;
    private FirstPersonController player;

    private void Start()
    {
        optionMenu = FindObjectOfType<OptionMenu>();
        player = FindObjectOfType<FirstPersonController>();
    }

    public void Save()
    {
        Debug.Log("DA FARE MOLTO BENE!!!!!");

        PlayerPrefs.SetInt("sensibilita", sensibilita);
        PlayerPrefs.SetInt("vitaInfinita", boolToInt(vitaInfinita));
        PlayerPrefs.SetInt("staminaInfinita", boolToInt(staminaInfinita));
        PlayerPrefs.SetInt("fullEquip", boolToInt(fullEquip));
        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("fullscreen", boolToInt(optionMenu.fullscreen));
        PlayerPrefs.SetInt("vsync", boolToInt(optionMenu.vsync));
        PlayerPrefs.SetInt("currentResolution", optionMenu.resolutionDropdown.resIndex);
        PlayerPrefs.SetFloat("volume",  optionMenu.volume.value);

        player.SavePlayerData();

        PlayerPrefs.Save();

    }

    public void Load()
    {
        Debug.Log("DA FARE MOLTO BENE!!!!!");

        SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene"));

        sensibilita = PlayerPrefs.GetInt("sensibilita");
        vitaInfinita = intToBool(PlayerPrefs.GetInt("vitaInfinita"));
        staminaInfinita = intToBool(PlayerPrefs.GetInt("staminaInfinita"));
        fullEquip = intToBool(PlayerPrefs.GetInt("fullEquip"));
        optionMenu.fullscreen.isOn = intToBool(PlayerPrefs.GetInt("fullscreen"));
        optionMenu.vsync.isOn = intToBool(PlayerPrefs.GetInt("vsync"));
        optionMenu.fullscreen.isOn = intToBool(PlayerPrefs.GetInt("fullscreen"));
        optionMenu.resolutionDropdown.resIndex = PlayerPrefs.GetInt("currentResolution");
        optionMenu.volume.value = PlayerPrefs.GetFloat("volume");

        player.LoadPlayerData();

    }

    private int boolToInt(bool b)
    {
        return b ? 1 : 0;
    }

    private bool intToBool(int b)
    {
        return b == 1; 
    }

}
