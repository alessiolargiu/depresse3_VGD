using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerReadyColosseum : MonoBehaviour
{

    private FirstPersonController player;
    private Inventory inventory;


    public ChestEquip [] chests;
    public ShieldEquip [] shields;
    public HelmetEquip [] helmets;

        
    public GameObject cutscenefinale;
    
    public GameObject trigger;

    private int chestCounter;
    private int shieldCounter;
    private int helmetCounter;

    private int chestLen;
    private int shieldLen;
    private int helmetLen;

    private TMP_Text contatore;


    void Start()
    {
        player = GameObject.Find("PlayerProtagonista").GetComponent<FirstPersonController>();
        chestLen = chests.Length;
        shieldLen = shields.Length;
        helmetLen = helmets.Length; 
        contatore = GameObject.Find("Contatore").GetComponent<TMP_Text>();
        contatore.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update(){

        chestCounter=0;
        shieldCounter=0;
        helmetCounter=0;

        inventory = player.GetInventory();

        foreach(ChestEquip ch in chests){
            foreach(int chI in player.GetAvailableChests()){
                if(ch.index == chI){
                    ch.gameObject.SetActive(false);
                    chestCounter++;
                }
            }
        }

        foreach(ShieldEquip sh in shields){
            if(sh.gameObject.activeSelf){
                shieldCounter++;
            }
        }

        foreach(HelmetEquip he in helmets){
            if(he.gameObject.activeSelf){
                helmetCounter++;
            }
        }


        if(chestCounter<chestLen && shieldCounter<shieldLen && helmetCounter<helmetLen){
            cutscenefinale.SetActive(true);
            trigger.SetActive(true);
            StartCoroutine(cutscenefinale.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
        }

        contatore.text = "Hai trovato\n" + (chestLen - chestCounter) + " - Busti su " + chestLen + "\n" + (shieldLen - shieldCounter) + " - Scudi su " + shieldLen + "\n" + (helmetLen - helmetCounter) + " - Elmi su " + helmetLen;

    }
}
