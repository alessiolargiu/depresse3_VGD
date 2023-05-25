using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCutsceneAfterGigante : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cutScene;

    
    void Awake()
    {
        cutScene.SetActive(true);
        StartCoroutine(cutScene.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
    }

}
