using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.setGameIsPaused(false);
        Debug.Log("Da modificare");
        SceneManager.LoadScene("TestAndre");
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.setGameIsPaused(false);
        Debug.Log("Da modificare");
        SceneManager.LoadScene("TestAndre");
    }

    public void Options()
    {
        Debug.Log("Opzioni da fare");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
