using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionTracking : MonoBehaviour
{
    
    public GameObject [] missions;
    private static int currentMission = 0;

    void Awake(){
        UpdateMissions();
    }

    public void UpdateMissions(){
        int i=0;
        foreach(GameObject GO in missions){
            if(i==currentMission){
                GO.SetActive(true);
            } else GO.SetActive(false);
            i++;
        }
    }

    public void setCurrentMission(int n){
        if (SceneManager.GetActiveScene().name == "TestALessioMappa")
        {
            GameObject crntMission = GameObject.Find("Missioni").transform.GetChild(currentMission).gameObject;
            FindObjectOfType<Compass>().RemoveQuestMarker(crntMission.GetComponentInChildren<QuestMarker>());
        }
        currentMission =n;
        UpdateMissions();
    }

    public static int getCurrentMission()
    {
        return currentMission;
    }

}
