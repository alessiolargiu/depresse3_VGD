using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMainMission : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject gameManager;
    private string myMission;


    void Awake()
    {
        StartCoroutine(findManager());
        myMission = gameObject.name;
        gameManager.GetComponent<MissionTracking>().addMission(myMission);
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
