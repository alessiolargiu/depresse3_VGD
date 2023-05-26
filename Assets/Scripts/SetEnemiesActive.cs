using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemiesActive : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemySpawner;
    void Awake()
    {
        enemySpawner.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
