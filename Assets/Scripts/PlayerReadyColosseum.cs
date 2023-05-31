using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerReadyColosseum : MonoBehaviour
{

    private FirstPersonController player;
    private Inventory inventory;


    //public ChestEquip [] chests;
    //public ShieldEquip [] shields;
    //public HelmetEquip [] helmets;

        
    public GameObject cutscenefinale;
    
    public GameObject trigger;

    private int chestCounter;
    private int shieldCounter;
    private int helmetCounter;

    private TMP_Text contatore;


    void Start()
    {
        player = GameObject.Find("PlayerProtagonista").GetComponent<FirstPersonController>();
        contatore = GameObject.Find("Contatore").GetComponent<TMP_Text>();
        contatore.gameObject.SetActive(true);
        chestCounter = 0;
        shieldCounter = 0;
        helmetCounter = 0;
    }

    // Update is called once per frame
    void Update(){

        foreach (int i in player.availableChests)
        {
            Debug.Log("indice chest: " + i);
        }
        
        //Da ricontrollare
        chestCounter = player.availableChests.Count - 1;
        shieldCounter = player.availableShields.Count;
        helmetCounter = player.availableHelmets.Count;


        if (chestCounter > 0 && shieldCounter > 0 && helmetCounter > 0){
            cutscenefinale.SetActive(true);
            trigger.SetActive(true);
            StartCoroutine(cutscenefinale.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
        }

        contatore.text = "Hai trovato\n" + chestCounter + " - Busti su 3\n" + shieldCounter + " - Scudi su 3\n" + helmetCounter + " - Elmi su 3";

    }
}
