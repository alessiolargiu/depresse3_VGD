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

        loadingScreen.SetActive(true);
        PauseMenu.setGameIsPaused(false);
        if (name != "MainMenuScene")
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            HUD.SetActive(true);
            player.SetActive(true);
        }
        if (GameObject.Find("GoTo") != null)
        {
            GameObject.Find("GoTo").SetActive(false);
        }
        if(GameObject.Find("GameOver") != null)
        {
            GameObject.Find("GameOver").SetActive(false);
        }
        

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            slider.value = progress;
            loadingText.text = (int)(progress * 100f) + "%";
            yield return null;
        }

        if (newLoad)
        {
            Transform spawnPoint = null;
            while (spawnPoint == null)
            {
                spawnPoint = GameObject.Find("PlayerSpawnPoint").GetComponent<Transform>();
                yield return null;
            }
             
            if(spawnPoint != null)
            {
                player.transform.position = spawnPoint.position;
            }
        }
        while(loadingScreen == null){
            loadingScreen = GameObject.Find("Loading Container");
        } 
        if (loadingScreen != null)
        {
            yield return new WaitForSeconds(1.5f);
            loadingScreen.SetActive(false);
            if (!newLoad)
            {
                var savedPlayer = JsonUtility.FromJson<SavePlayerData>(PlayerPrefs.GetString("playerData"));
                player.gameObject.transform.position = savedPlayer.position/* + new Vector3(0, 10f, 0)*/;
            }
        }

        GetComponent<GameManager>().Save();

    }


}
