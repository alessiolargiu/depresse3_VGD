using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject optionMenu;
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject HUD;
    public GameObject player;

    public void NewGame()
    {
        Debug.Log("Da modificare");
        StartCoroutine(LoadAsynchronously("TestGadorNuovo 1"));
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.setGameIsPaused(false);
        player.SetActive(true);
        HUD.SetActive(true);
        Debug.Log("Caricamento da fare");
    }

    public void Options()
    {
        optionMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsynchronously (string name)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(name);

        loadingScreen.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.setGameIsPaused(false);
        HUD.SetActive(true);
        player.SetActive(true);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

}
