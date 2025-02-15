using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BartolusUcciso : MonoBehaviour
{

    public GameObject bartolus;
    private GameObject GameManager;
    private bool stop;

    

    void Awake(){
        stop=false;
    }



    // Update is called once per frame
    void Update()
    {
        if(bartolus==null && stop==false){
            stop=true;
            StartCoroutine(findManager());
        }
    }



    IEnumerator findManager(){
        while(GameObject.Find("GameManager")==null){
            yield return null;
        }
        GameManager = GameObject.Find("GameManager");
        GameManager.GetComponent<GameManager>().StartLoading("MappaCutsceneFinale", false);
        yield return null;
    }
}
