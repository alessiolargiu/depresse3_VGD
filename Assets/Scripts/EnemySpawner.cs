using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    private GameObject player;
    private Transform playerTrans;
    public GameObject[] toSpawn;

    private bool cycleStarted;

    public bool spawnerForMission;

    public int minSpawnTime;
    public int maxSpawnTime;

    public float distanza;

    // Start is called before the first frame update
    void Start(){
        
        player = GameObject.Find("protagonista");
        playerTrans = player.transform;
        if(spawnerForMission){
            StartCoroutine(SpawnEnemyStart());
        }
    }

    // Update is called once per frame
    void Update(){
        float dist = Vector3.Distance(playerTrans.position, transform.position);
        if(dist<distanza && cycleStarted==false){
            StartCoroutine(SpawnEnemy());
            Debug.Log("SPAwNER Sono pronto a spawnare");
            cycleStarted=true;
        } else if(dist>distanza){
            Debug.Log("SPAwNER Non sono pronto a spawnare");
            cycleStarted=false;
        }        
    }

    IEnumerator SpawnEnemy(){
        int i=0;
        int whenToSpawn;
        while(cycleStarted==false){
            int whoToSpawn = Random.Range(0, toSpawn.Length);
            Vector3 randPos = new Vector3(Random.Range(0,20),0,Random.Range(0,20));
            GameObject spawned = Instantiate(toSpawn[whoToSpawn], transform.position + randPos, transform.rotation);
            spawned.SetActive(true);
            toSpawn[i].GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(transform.position+randPos);
            whenToSpawn = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(whenToSpawn);
        }

    }

    IEnumerator SpawnEnemyStart(){
        int i=0;
        int whenToSpawn;
        while(i<3){
            int whoToSpawn = Random.Range(0, toSpawn.Length);
            Vector3 randPos = new Vector3(Random.Range(0,20),0,Random.Range(0,20));
            GameObject spawned = Instantiate(toSpawn[whoToSpawn], transform.position + randPos, transform.rotation);
            spawned.SetActive(true);
            toSpawn[i].GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(transform.position+randPos);
            whenToSpawn = Random.Range(10, 20);
            i++;
        }
        yield return null;
    }
}
