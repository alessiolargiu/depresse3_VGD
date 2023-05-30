using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrendiSpada : MonoBehaviour
{
    public GameObject scenaCorrente;
    public GameObject prossimaScena;
    public QuestMarker questMarker;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("player")){
            FindObjectOfType<Compass>().RemoveQuestMarker(questMarker);
            Destroy(questMarker);
            FindObjectOfType<FirstPersonController>().GetAvailableWeapons().Add(1);
            Resources.FindObjectsOfTypeAll<HUDInventoryWeapon>()[0].SetInventory(FindObjectOfType<FirstPersonController>().GetInventory(), FindObjectOfType<FirstPersonController>().GetAvailableWeapons());
            scenaCorrente.SetActive(false);
            prossimaScena.SetActive(true);
        }
    }
}
