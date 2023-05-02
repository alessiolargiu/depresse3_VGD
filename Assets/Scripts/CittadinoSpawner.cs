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
    }

    // Update is called once per frame
    void Update(){
        float dist = Vector3.Distance(playerTrans.position, transform.position);
        if(dist<10  && cycleStarted==false){
            Debug.Log("SPAwNER Sono pronto a spawnare");
            cycleStarted=true;
            StartCoroutine(SpawnCittadino());
        } else {
            Debug.Log("SPAwNER Non sono pronto a spawnare");
            cycleStarted=false;
        }        
    }

    IEnumerator SpawnCittadino(){
        int i=0;
        while(cycleStarted && i<=20){
            i++;
            int whenToSpawn = Random.Range(5, 20);
            yield return new WaitForSeconds(whenToSpawn);
            int whoToSpawn = Random.Range(0, toSpawn.Length);
            GameObject spawned = Instantiate(toSpawn[whoToSpawn], transform.position, transform.rotation);
            spawned.SetActive(true);
        }

    }
}
