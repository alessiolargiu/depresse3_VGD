using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject gameManager;
    private string myCheckpoint;


    void Awake()
    {
        StartCoroutine(findManager());
        myCheckpoint = gameObject.name;
        gameManager.GetComponent<MissionTracking>().addMission(myCheckpoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator findManager(){
        while(GameObject.Find("GameManager")==null){
            yield return null;
        }
        gameManager = GameObject.Find("GameManager");
        yield return null;
    }
}
