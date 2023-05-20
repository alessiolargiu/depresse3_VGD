using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CittadinoSpawner : MonoBehaviour
{   
    //da finire, funziona a merda, però mi sono reso conto che gli npc non influiscono COSì TANTO sulla memoria, quindi si possono anche lasciare prefissati
    private GameObject player;
    private Transform playerTrans;
    public GameObject[] toSpawn;

    private bool cycleStarted;


    // Start is called before the first frame update
    void Start(){
        
        player = GameObject.Find("protagonista");
        playerTrans = player.transform;
        StartCoroutine(SpawnCittadinoStart());
    }

    // Update is called once per frame
    void Update(){
        float dist = Vector3.Distance(playerTrans.position, transform.position);
        if(dist<10 && cycleStarted==false){
            StartCoroutine(SpawnCittadino());
            Debug.Log("SPAwNER Sono pronto a spawnare");
            cycleStarted=true;
        } else if(dist>10){
            Debug.Log("SPAwNER Non sono pronto a spawnare");
            cycleStarted=false;
        }        
    }

    IEnumerator SpawnCittadino(){
        int i=0;
        int whenToSpawn;
        while(cycleStarted==false){
            int whoToSpawn = Random.Range(0, toSpawn.Length);
            Vector3 randPos = new Vector3(Random.Range(0,20),0,Random.Range(0,20));
            GameObject spawned = Instantiate(toSpawn[whoToSpawn], transform.position + randPos, transform.rotation);
            spawned.SetActive(true);
            toSpawn[i].GetComponent<UnityEngine.AI.NavMeshAgent>().Warp(transform.position+randPos);
            whenToSpawn = Random.Range(10, 30);
            yield return new WaitForSeconds(whenToSpawn);
        }

    }

    IEnumerator SpawnCittadinoStart(){
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
