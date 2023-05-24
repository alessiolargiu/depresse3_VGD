using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLatestMission : MonoBehaviour
{

    private static int currentMission=0;
    private GameObject missioni;


    void Awake(){
        StartCoroutine(findMissioni());
    }

    void Update(){
        Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
    }

    public void setCurrentMission(int crnt){
        currentMission=crnt;
        missioni.GetComponent<MissionTracking>().setCurrentMission(currentMission);
    }


    IEnumerator findMissioni(){
        while(GameObject.Find("Missioni")==null){
            yield return null;
        }
        missioni = GameObject.Find("Missioni");
        yield return null;
    }
}
