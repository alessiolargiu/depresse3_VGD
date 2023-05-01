using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeOfView : MonoBehaviour
{
    private bool amSeeingPlayer;

    void Start(){
        amSeeingPlayer=false;
    }

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("player")){
            amSeeingPlayer=true;
            Debug.Log("Vedo il player");
        }
        
    }

    public void OnTriggerExit(){
        Debug.Log("Non vedo il player");
        amSeeingPlayer=false;
    }

    public bool GetPlayerSight(){
        return amSeeingPlayer;
    }
}
