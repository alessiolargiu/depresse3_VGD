using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGigante : MonoBehaviour
{
    // Start is called before the first frame update

    public LayerMask giocatore;


    private bool aia;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision col){

        if(col.gameObject.CompareTag("player") && aia==false){
            Debug.Log("sto toccanado lucius");
            aia=true;
            col.gameObject.GetComponent<FirstPersonController>().TakeDamage(5,transform,3);
        }

    }

    public void OnCollisionExit(Collision col){

        if(col.gameObject.CompareTag("player")){
            aia=false;
        }

    }
}
