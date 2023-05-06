using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackDetect : MonoBehaviour
{
    public bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollison(Collision col){
        Debug.Log("Sto toccando roba");
        isColliding=true;
    }

    void OnCollisionExit(Collision col){
        Debug.Log("Sto toccando niente");
        isColliding=false;
    }
}
