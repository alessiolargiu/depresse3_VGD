using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] toSpawn;
    public int howManyTimes;
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update(){
    }

    IEnumerator SpawnEnemy(){

        for(int i=0;i<=howManyTimes;i++){
            int whenToSpawn = Random.Range(1, 20);
            yield return new WaitForSeconds(whenToSpawn);
            int whoToSpawn = Random.Range(0, toSpawn.Length);
            GameObject spawned = Instantiate(toSpawn[whoToSpawn], transform.position, transform.rotation);
            spawned.SetActive(true);
        }

    }
}
