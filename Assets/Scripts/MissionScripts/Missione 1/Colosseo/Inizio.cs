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
        cutScene.SetActive(true);
        StartCoroutine(cutScene.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){oggettiGioco.SetActive(true); DestroyObject(gameObject);}}));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
