using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteThingOnComand : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject thing;

    void Awake(){
        DestroyObject(thing);
    }

}
