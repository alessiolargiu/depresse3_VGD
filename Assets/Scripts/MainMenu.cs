using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject optionMenu;

    public void NewGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.setGameIsPaused(false);
        Debug.Log("Da modificare");
        SceneManager.LoadScene("TestAndre 1");
    }

    public void LoadGame()
    {
        Debug.Log("Caricamento da fare");
    }

    public void Options()
    {
        Debug.Log("Opzioni da fare");
        optionMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
