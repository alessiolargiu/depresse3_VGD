using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrendiSpada : MonoBehaviour
{
    public GameObject scenaCorrente;
    public GameObject prossimaScena;
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("player")){
            scenaCorrente.SetActive(false);
            prossimaScena.SetActive(true);
        }
    }
}
