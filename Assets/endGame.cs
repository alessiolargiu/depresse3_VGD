using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{

    public GameObject bartolus;
    private GameObject GameManager;
    private bool stop;

    

    void Awake(){
        StartCoroutine(findManager());
    }




    IEnumerator findManager(){
        while(GameObject.Find("GameManager")==null){
            yield return null;
        }
        GameManager = GameObject.Find("GameManager");
        GameManager.GetComponent<GameManager>().StartLoading("MainMenuScene", false);
        yield return null;
    }
}
