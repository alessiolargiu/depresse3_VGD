using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLatestMission : MonoBehaviour
{

    private static int currentMission;
    private GameObject missioni;
    private GameObject[] spawners;

    void Awake(){
        StartCoroutine(findMissioni());
    }

    void Update(){
        Debug.Log("Awake:" + SceneManager.GetActiveScene().name + " ---" + currentMission);
    }

    public void SetCurrentMission(int crnt){
        currentMission =crnt;
        if(missioni!=null){
            if(crnt>=6){
                spawners = GameObject.FindGameObjectsWithTag("enemySpawner");
                foreach(GameObject spawner in spawners){
                    spawner.GetComponent<EnemySpawner>().canSpawn=true;
                }
            }
            
            missioni.GetComponent<MissionTracking>().setCurrentMission(currentMission);
        }
        
    }

    public int GetCurrentMission()
    {
        return currentMission;
    }


    IEnumerator findMissioni(){
        while(GameObject.Find("Missioni")==null){
            yield return null;
        }
        missioni = GameObject.Find("Missioni");
        SetCurrentMission(currentMission);
        yield return null;
    }
}
