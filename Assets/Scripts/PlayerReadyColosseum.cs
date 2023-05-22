using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyColosseum : MonoBehaviour
{
    public ChestEquip [] chests;
    public ShieldEquip [] shields;
    public HelmetEquip [] helmets;


    public GameObject cutscenefinale;
    
    public GameObject trigger;

    private int chestLen;
    private int shieldLen;
    private int helmetLen;


    void Start()
    {
        chestLen = chests.Length;
        shieldLen = shields.Length;
        helmetLen = helmets.Length;  
    }

    // Update is called once per frame
    void Update(){
        if(chests.Length<chestLen && shields.Length<shieldLen && helmets.Length<helmetLen){
            cutscenefinale.SetActive(true);
            trigger.SetActive(true);
            StartCoroutine(cutscenefinale.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
        }
    }
}
