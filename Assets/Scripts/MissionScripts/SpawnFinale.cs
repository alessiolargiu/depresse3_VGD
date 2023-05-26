using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinale : MonoBehaviour
{
    private GameObject GameManager;

    void Awake(){
        StartCoroutine(findManager());
    }



    IEnumerator findManager(){
        while(GameObject.Find("GameManager")==null){
            yield return null;
        }
        GameManager = GameObject.Find("GameManager");
        GameManager.GetComponent<GetLatestMission>().setCurrentMission(8);
        StartCoroutine(GameManager.GetComponent<LoadingScene>().LoadAsynchronously("TestALessioMappa", false));
        yield return null;
    }
}
