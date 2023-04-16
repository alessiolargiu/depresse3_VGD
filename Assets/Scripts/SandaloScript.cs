using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandaloScript : MonoBehaviour
{

    public int dmg;
    public bool beenUsed;
    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void OnCollisionEnter(Collision collision){

        if(collision.gameObject.tag == "player"){
        ///Attack code here
            collision.gameObject.GetComponent<FirstPersonController>().TakeDamage(dmg, transform, 2);
            Destroy(gameObject);
            //self.PlayOneShot(pugnoSound, 1f);
        }
        if(beenUsed && (collision.collider.gameObject.layer == LayerMask.NameToLayer("enemy"))==false){
            Destroy(gameObject);
        }
    }
}
