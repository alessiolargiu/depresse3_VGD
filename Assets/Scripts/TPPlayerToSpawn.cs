using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPlayerToSpawn : MonoBehaviour
{
    void Awake(){
        GameObject.Find("PlayerProtagonista").transform.position = transform.position;
    }
}
