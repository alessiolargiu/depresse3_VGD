using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCount : MonoBehaviour
{
    int deaths;
    public int desiredDeaths;


    // Update is called once per frame
    void Update()
    {

        if(deaths>=desiredDeaths){

        }
    }

    public void addDeath(){
        deaths++;
    }
}

