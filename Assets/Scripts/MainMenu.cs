using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("TestAndre");
        Debug.Log("Da modificare");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("TestAndre");
        Debug.Log("Da modificare");
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
