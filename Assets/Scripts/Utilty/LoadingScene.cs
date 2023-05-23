using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static GameManager;

public class LoadingScene : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text loadingText;
    public GameObject HUD;
    public GameObject player;

    public IEnumerator LoadAsynchronously(string name, bool newLoad)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(name);

        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            loadingScreen = GameObject.Find("LoadingScreenMenu").transform.GetChild(0).gameObject;
        }
        else
        {
            loadingScreen = GameObject.Find("LoadingScreen").transform.GetChild(0).gameObject;
        }
        loadingText = loadingScreen.transform.Find("Current Load").GetComponent<TMP_Text>();
        slider = loadingScreen.GetComponentInChildren<Slider>();

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
            loadingText.text = slider.value.ToString() + "%";
            yield return null;
        }

        if (newLoad)
        {
            Transform spawnPoint = null;
            while (spawnPoint == null)
            {
                spawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
                yield return null;
            }
             
            if(spawnPoint != null)
            {
                Debug.Log("Sto spawnando nello spawnpoint");
                player.transform.position = spawnPoint.position;
                Debug.Log("Ho spawnato il player nello spawnpoint");
            }
        }

        if (loadingScreen != null)
        {
            yield return new WaitForSeconds(1);
            loadingScreen.SetActive(false);
        }

        /*else
        {
            var savedPlayer = JsonUtility.FromJson<SavePlayerData>(PlayerPrefs.GetString("playerData"));
            player.gameObject.transform.position = savedPlayer.position;
        }*/
    }
}
