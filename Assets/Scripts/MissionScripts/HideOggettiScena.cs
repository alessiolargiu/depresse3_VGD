using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOggettiScena : MonoBehaviour
{   

    public GameObject cutscene;
    public GameObject oggettiScena;


    // Update is called once per frame
    void Update()
    {
        if(cutscene.activeSelf){
            oggettiScena.SetActive(false);
        } else oggettiScena.SetActive(true);
        
    }
}
