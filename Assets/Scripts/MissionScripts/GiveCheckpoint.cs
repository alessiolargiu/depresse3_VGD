using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCheckpoint : MonoBehaviour
{

    private GameObject GameManager;
    public int missionNumber;

    void Awake(){
        StartCoroutine(findManager());
    }



    IEnumerator findManager(){
        while(GameObject.Find("GameManager")==null){
            yield return null;
        }
        GameManager = GameObject.Find("GameManager");
        GameManager.GetComponent<GetLatestMission>().SetCurrentMission(missionNumber);
        yield return null;
    }
}
