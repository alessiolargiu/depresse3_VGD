using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteInvisibleWalls : MonoBehaviour
{

    public GameObject trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(trigger==null){
            DestroyObject(gameObject);
        }
        
    }
}
