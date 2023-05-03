using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuHUD;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
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

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuHUD.SetActive(false);
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

    public void Save()
    {
        Debug.Log("SALVATAGGIO DA FARE");
    }

    public void Options()
    {
        Debug.Log("Opzioni da fare");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public static void setGameIsPaused(bool pause)
    {
        gameIsPaused = pause;
    }

}
