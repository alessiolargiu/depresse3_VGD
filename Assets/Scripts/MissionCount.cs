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
            StartCoroutine(waitToDie());
        }
    }

    public void addDeath(){
        deaths++;
    }

    IEnumerator waitToDie(){
        yield return new WaitForSeconds(3f);
        cutScene.SetActive(true);
        StartCoroutine(cutScene.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
    }
}

