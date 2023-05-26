using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTracking : MonoBehaviour
{
    
    public GameObject [] missions;
    private static int currentMission=0;

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
        currentMission=n;
        UpdateMissions();
    }

    public static int getCurrentMission()
    {
        return currentMission;
    }

}
