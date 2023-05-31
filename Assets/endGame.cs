using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{

    public GameObject bartolus;
    private GameObject GameManager;
    private bool stop;

    

    void Awake(){
        StartCoroutine(findManager());
        FindObjectOfType<FirstPersonController>().gameObject.SetActive(false);
        GameObject.Find("HUD").SetActive(false);
        //GameObject.Find("Panel Pause").SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }




    IEnumerator findManager(){
        while(GameObject.Find("GameManager")==null){
            yield return null;
        }
        GameManager = GameObject.Find("GameManager");
        SceneManager.LoadScene("MainMenuScene");
        yield return null;
    }
}
