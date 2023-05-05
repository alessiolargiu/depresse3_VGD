using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScene : MonoBehaviour
{   
    public GameObject cutscene;
    public GameObject containerMissione;
    // Start is called before the first frame update
    void Awake()
    {
        cutscene.SetActive(true);
        StartCoroutine(cutscene.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){
        containerMissione.SetActive(false);
        DestroyObject(gameObject);

        }}));
    }

}
