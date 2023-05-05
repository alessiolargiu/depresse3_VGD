using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineMissione : MonoBehaviour
{   
    public GameObject container;
    // Start is called before the first frame update
    void Awake()
    {
        DestroyObject(container);
    }


}
