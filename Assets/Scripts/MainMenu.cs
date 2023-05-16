using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject optionMenu;
    private LoadingScene ls;

    private void Start()
    {
        ls = FindObjectOfType<LoadingScene>();
    }

    public void NewGame()
    {
        Debug.Log("Da modificare");
        StartCoroutine(ls.LoadAsynchronously("TestALessioMappa"));
    }

    public void LoadGame()
    {
        Debug.Log("Caricamento da fare");
        StartCoroutine(ls.LoadAsynchronously("TestALessioMappa"));
    }

    public void Options()
    {
        optionMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    

}
