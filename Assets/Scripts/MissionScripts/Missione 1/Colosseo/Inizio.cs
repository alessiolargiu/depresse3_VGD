using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inizio : MonoBehaviour
{
    public GameObject cutScene;
    public GameObject oggettiGioco;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(waitForGameManager());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitForGameManager(){
        while(GameObject.Find("GameManager")==false){
            yield return null;
        }

        yield return new WaitForSeconds(2);
        cutScene.SetActive(true);
        StartCoroutine(cutScene.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){oggettiGioco.SetActive(true); DestroyObject(gameObject);}}));
    }
}
