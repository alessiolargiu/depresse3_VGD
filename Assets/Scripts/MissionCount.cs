using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCount : MonoBehaviour
{
    int deaths;
    public int desiredDeaths;
    public GameObject cutScene;
    bool stop;


    // Update is called once per frame
    void Update()
    {

        if(deaths>=desiredDeaths && stop==false){
            stop=true;
            cutScene.SetActive(true);
            StartCoroutine(cutScene.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
        }
    }

    public void addDeath(){
        deaths++;
    }
}

