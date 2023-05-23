using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTracking : MonoBehaviour
{
    

    public GameObject missioni;

    public string currentMission;
    public string checkPoint;

    private GameObject currentMissionGO;
    private GameObject currentCheckPointGO;


    void Awake(){
        
        //GameObject.Find("/" + missioni.name + "/" + currentMission + "/" + checkPoint);
        //resumeLastMission();
        StartCoroutine(waitUntilExists());
    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log("SBORRAFRESCA" + "/" + missioni.name + "/" + currentMission + "/" + checkPoint);
       //Debug.Log("SBORRACALDA" + currentCheckPointGO.name);
    }

    public void addMission(string msn){
        currentMission=msn;
    }

    public void resumeLastMission(){

        
        currentCheckPointGO = GameObject.Find(checkPoint);


        //GameObject.Find("Equip Container Weapon/" + currentWeapon.name + "/Collider").transform;    

        GameObject [] checkpoints = GameObject.FindGameObjectsWithTag("checkpoint");

        foreach(GameObject GO in checkpoints){
            GO.SetActive(false);
        }

        GameObject [] missions = GameObject.FindGameObjectsWithTag("missioni");
        
        foreach(GameObject GO in missions){
            GO.SetActive(false);
        }

        currentMissionGO.SetActive(true);
        currentCheckPointGO.SetActive(true);      
        
    }

    IEnumerator waitUntilExists(){
        while(GameObject.Find("Missioni")==null || GameObject.Find(currentMission)==null || GameObject.Find(checkPoint)==null){
            yield return null;
        }
        missioni = GameObject.Find("Missioni");
        currentMissionGO = GameObject.Find(currentMission);
        currentCheckPointGO = GameObject.Find(checkPoint);

        resumeLastMission();
    }
}
