using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pauseMenuHUD;
    public GameObject optionHUD;
    public FirstPersonController player;
    public LoadingScene ls;
    public GameObject gameOver;

    private void Start()
    {
        //player = FindObjectOfType<FirstPersonController>();
        //ls = GetComponent<LoadingScene>();
    }

    private void Update()
    {
        if (gameManager.optionMenu == null)
        {
            gameManager.optionMenu = FindObjectOfType<OptionMenu>();
        }

    }

    public void LoadGame()
    {
        //controllo se ho dei dati salvati
        if (PlayerPrefs.GetInt("vitaInfinita", -1) != -1)
        {
            player.gameObject.SetActive(true);
            gameManager.HUD.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameManager.sensibilita = PlayerPrefs.GetInt("sensibilita");
            gameManager.vitaInfinita = intToBool(PlayerPrefs.GetInt("vitaInfinita"));
            gameManager.staminaInfinita = intToBool(PlayerPrefs.GetInt("staminaInfinita"));
            gameManager.fullEquip = intToBool(PlayerPrefs.GetInt("fullEquip"));
            gameManager.optionMenu.fullscreen.isOn = intToBool(PlayerPrefs.GetInt("fullscreen"));
            gameManager.optionMenu.vsync.isOn = intToBool(PlayerPrefs.GetInt("vsync"));
            gameManager.optionMenu.fullscreen.isOn = intToBool(PlayerPrefs.GetInt("fullscreen"));
            gameManager.optionMenu.resolutionDropdown.resIndex = PlayerPrefs.GetInt("currentResolution");
            gameManager.optionMenu.volume.value = PlayerPrefs.GetFloat("volume");

            GetComponent<GetLatestMission>().SetCurrentMission(PlayerPrefs.GetInt("currentMission"));

            var savedPlayer = JsonUtility.FromJson<SavePlayerData>(PlayerPrefs.GetString("playerData"));
            player.gameObject.transform.position = savedPlayer.position;
            //player.gameObject.transform.position += new Vector3(0, 5f, 0);
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
            //GameObject.Find("GameOver").SetActive(false);
            StartCoroutine(ls.LoadAsynchronously(PlayerPrefs.GetString("currentScene"), false));
        }

    }

    public void Menu()
    {
        FindObjectOfType<FirstPersonController>().gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "TestALessioMappa")
        {
            if (GameObject.Find("Oggetti Equipaggiamento") != null)
            {
                for (int i = 0; i < GameObject.Find("Oggetti Equipaggiamento").transform.childCount; i++)
                {
                    if (GameObject.Find("Oggetti Equipaggiamento").transform.GetChild(i).gameObject.activeSelf)
                    {
                        FindObjectOfType<Compass>().RemoveQuestMarker(GameObject.Find("Oggetti Equipaggiamento").transform.GetChild(i).GetComponent<QuestMarker>());
                    }
                }
            }
        }
        pauseMenuHUD.SetActive(false);
        optionHUD.SetActive(false);
        gameManager.HUD.SetActive(false);
        gameOver.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        PauseMenu.setGameIsPaused(false);
        
        gameManager.StartLoading("MainMenuScene", false);
    }

    private bool intToBool(int b)
    {
        return b == 1;
    }
}