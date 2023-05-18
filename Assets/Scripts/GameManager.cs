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
    public OptionMenu optionMenu;
    public GameObject HUD;
    private FirstPersonController player;

    [System.Serializable]
    public class SavePlayerData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public int currentHealth;
        public float currentStamina;
        public List<int> availableWeapons = new List<int>();
        public List<int> availableShields = new List<int>();
        public List<int> availableChests = new List<int>();
        public List<int> availableHelmets = new List<int>();
        public List<int> availablePotions = new List<int>();

    }

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
        PlayerPrefs.SetInt("fullscreen", boolToInt(optionMenu.fullscreen.isOn));
        PlayerPrefs.SetInt("vsync", boolToInt(optionMenu.vsync.isOn));
        PlayerPrefs.SetInt("currentResolution", optionMenu.resolutionDropdown.resIndex);
        PlayerPrefs.SetFloat("volume",  optionMenu.volume.value);

        SavePlayerData playerData = new SavePlayerData()
        {
            position = transform.position,
            rotation = transform.rotation,
            scale = transform.localScale,
            currentHealth = player.currentHealth,
            currentStamina = player.currentStamina,
            availableHelmets = player.availableHelmets,
            availableChests = player.availableChests,
            availableWeapons = player.availableWeapons,
            availableShields = player.availableShields,
            availablePotions = player.availablePotions
        };

        PlayerPrefs.SetString("playerData", JsonUtility.ToJson(playerData));

        PlayerPrefs.Save();

    }

    public void Load()
    {
        player.gameObject.SetActive(true);
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sensibilita = PlayerPrefs.GetInt("sensibilita");
        vitaInfinita = intToBool(PlayerPrefs.GetInt("vitaInfinita"));
        staminaInfinita = intToBool(PlayerPrefs.GetInt("staminaInfinita"));
        fullEquip = intToBool(PlayerPrefs.GetInt("fullEquip"));
        optionMenu.fullscreen.isOn = intToBool(PlayerPrefs.GetInt("fullscreen"));
        optionMenu.vsync.isOn = intToBool(PlayerPrefs.GetInt("vsync"));
        optionMenu.fullscreen.isOn = intToBool(PlayerPrefs.GetInt("fullscreen"));
        optionMenu.resolutionDropdown.resIndex = PlayerPrefs.GetInt("currentResolution");
        optionMenu.volume.value = PlayerPrefs.GetFloat("volume");

        var savedPlayer = JsonUtility.FromJson<SavePlayerData>(PlayerPrefs.GetString("playerData"));
        transform.position = savedPlayer.position;
        transform.rotation = savedPlayer.rotation;
        transform.localScale = savedPlayer.scale;
        player.currentHealth = savedPlayer.currentHealth;
        player.currentStamina = savedPlayer.currentStamina;
        player.availableHelmets = savedPlayer.availableHelmets;
        player.availableChests = savedPlayer.availableChests;
        player.availableWeapons = savedPlayer.availableWeapons;
        player.availableShields = savedPlayer.availableShields;
        player.availablePotions = savedPlayer.availablePotions;

        Debug.Log("DA FARE MOLTO BENE!!!!!");

        SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene"));

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
