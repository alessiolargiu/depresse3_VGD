using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuHUD;
    public GameObject optionHUD;
    public GameObject HUD;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(FindObjectOfType<Tutorial>() == null)
            {
                if (gameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
            else if (!FindObjectOfType<Tutorial>().tutorialShowing)
            {
                if (gameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuHUD.SetActive(false);
        if(optionHUD.activeSelf)
        {
            optionHUD.SetActive(false);
        }
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuHUD.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Menu()
    {
        GameObject.Find("TestoMissione").GetComponent<TMP_Text>().text = "";
        FindObjectOfType<FirstPersonController>().gameObject.SetActive(false);
        pauseMenuHUD.SetActive(false);
        optionHUD.SetActive(false);
        if(GameObject.Find("Compass") != null)
        {
            while(GameObject.Find("Compass").transform.childCount > 0)
            {
                Destroy(GameObject.Find("Compass").transform.GetChild(0).gameObject);
            }
        }
        HUD.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    public static void setGameIsPaused(bool pause)
    {
        gameIsPaused = pause;
    }

}
