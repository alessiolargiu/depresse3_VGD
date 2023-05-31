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
    public bool postProcessing;
    public OptionMenu optionMenu;
    public GameObject HUD;
    public FirstPersonController player;
    private LoadingScene ls;

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
        //player = FindObjectOfType<FirstPersonController>();
        ls = GetComponent<LoadingScene>();
    }

    private void Update()
    {
        if(optionMenu == null)
        {
            optionMenu = FindObjectOfType<OptionMenu>();
        }

    }

    public void Save()
    {
        PlayerPrefs.SetInt("sensibilita", sensibilita);
        PlayerPrefs.SetInt("vitaInfinita", boolToInt(vitaInfinita));
        PlayerPrefs.SetInt("staminaInfinita", boolToInt(staminaInfinita));
        PlayerPrefs.SetInt("fullEquip", boolToInt(fullEquip));
        if(SceneManager.GetActiveScene().name == "ArenaNight")
        {
            PlayerPrefs.SetString("currentScene", "ArenaDay");
        }
        else
        {
            PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);
        } 
        PlayerPrefs.SetInt("fullscreen", boolToInt(optionMenu.fullscreen.isOn));
        PlayerPrefs.SetInt("vsync", boolToInt(optionMenu.vsync.isOn));
        PlayerPrefs.SetInt("currentResolution", optionMenu.resolutionDropdown.resIndex);
        PlayerPrefs.SetFloat("volume",  optionMenu.volume.value);

        PlayerPrefs.SetInt("currentMission", GetComponent<GetLatestMission>().GetCurrentMission());

        if(player == null)
        {
            player = this.GetComponentInChildren<FirstPersonController>();
        }

        SavePlayerData playerData = new SavePlayerData()
        {
            position = player.gameObject.transform.position,
            rotation = player.gameObject.transform.rotation,
            scale = player.gameObject.transform.localScale,
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

    public void LoadGame()
    {
        //controllo se ho dei dati salvati
        if(PlayerPrefs.GetInt("vitaInfinita", -1) != -1)
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

            GetComponent<GetLatestMission>().SetCurrentMission(PlayerPrefs.GetInt("currentMission"));

            var savedPlayer = JsonUtility.FromJson<SavePlayerData>(PlayerPrefs.GetString("playerData"));
            player.gameObject.transform.position = savedPlayer.position;
            //player.gameObject.transform.position += new Vector3(0, 10f, 0);
            player.gameObject.transform.rotation = savedPlayer.rotation;
            player.gameObject.transform.localScale = savedPlayer.scale;
            player.currentHealth = savedPlayer.currentHealth;
            player.currentStamina = savedPlayer.currentStamina;
            if (GetComponent<GetLatestMission>().GetCurrentMission() != 1)
            { 
                player.availableHelmets = savedPlayer.availableHelmets;
                player.availableChests = savedPlayer.availableChests;
                player.availableWeapons = savedPlayer.availableWeapons;
                player.availableShields = savedPlayer.availableShields;
                player.availablePotions = savedPlayer.availablePotions;
            }
            
            //SceneManager.LoadScene(PlayerPrefs.GetString("currentScene"));
            StartCoroutine(ls.LoadAsynchronously(PlayerPrefs.GetString("currentScene"), false));
        }

    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        player.currentHealth = player.maxHealth;
        StartCoroutine(ls.LoadAsynchronously("Casa", true));
    }

    public void StartLoading(string name, bool newLoad)
    {
        StartCoroutine(GetComponent<LoadingScene>().LoadAsynchronously(name, newLoad));
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
