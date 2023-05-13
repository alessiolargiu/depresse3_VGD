using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public GameObject HUD;
    public GameObject player;

    public IEnumerator LoadAsynchronously(string name)
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
