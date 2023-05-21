using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsActive : MonoBehaviour
{
    public GameObject objectsOnMap;
    void Awake()
    {
        objectsOnMap.SetActive(true);
    }

}
