using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistCollisonCheck : MonoBehaviour
{

    private bool doDamage;

    // Start is called before the first frame update
    void Start()
    {
        doDamage=false;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision){  
        Debug.Log("Ho p so di star toccando cose");
        if(collision.gameObject.CompareTag("enemy")){
            Debug.Log("Ho p so di star toccando il nemico");
            if(doDamage){
                Debug.Log("Ho p so di togliere vit al nemico");
                collision.other.GetComponent<NPCFollowPathController>().TakeDamage(10);
            }
        }

    }

    public void PunchCollison(string msg){

        if(msg=="hit"){
            doDamage=true;
            Debug.Log("Ho picchiato");
        }
    }

    public void PunchExit(string msg){

        if(msg=="nohit"){
            doDamage=false;
            Debug.Log("Ho finito di picchiare");
        }
    }

    public bool getDamage(){
        return doDamage;
    }
}
