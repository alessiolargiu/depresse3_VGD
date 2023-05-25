using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;
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

        
        //loadingScreen = GameObject.Find("LoadingCanvas").transform.GetChild(0).gameObject;
        //loadingText = loadingScreen.transform.Find("Current Load").GetComponent<TMP_Text>();
        //slider = loadingScreen.GetComponentInChildren<Slider>();

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
            loadingText.text = progress * 100f + "%";
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
        else
        {
            
        }

        if (loadingScreen != null)
        {
            yield return new WaitForSeconds(1);
            loadingScreen.SetActive(false);
            if (!newLoad)
            {
                var savedPlayer = JsonUtility.FromJson<SavePlayerData>(PlayerPrefs.GetString("playerData"));
                Debug.Log("coordinate player" + savedPlayer.position);
                player.gameObject.transform.position = savedPlayer.position + new Vector3(0, 10f, 0);
            }
        }

    }
}
