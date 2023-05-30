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
            scenaCorrente.SetActive(false);
            prossimaScena.SetActive(true);
        }
    }
}
