using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cittadino : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator anim;

    public bool pathpnd;

    public LayerMask whatIsGround;

    private AudioSource self;

    public AudioClip smettila;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public bool pedIsDying;
    public int shelfLife;

    private Transform playerTrans;

    public AudioClip [] randomPhrase;
    private bool justTalked;

    public bool debugTarget;

    public float timer;
    private void Start(){
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        self = GetComponent<AudioSource>();
        playerTrans = GameObject.Find("PlayerProtagonista").transform;
    }

    private void Awake(){
        if(pedIsDying){
            StartCoroutine(CountToDeath());
        }

        StartCoroutine(CheckIfCorrect());
    }

    private void Update(){
        Patroling();
        //RandomThingToSay();

        timer += Time.deltaTime;
    }

    private void Patroling(){
        agent.speed=1.5f;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet){
            agent.SetDestination(walkPoint);
            if(agent.pathPending || timer >= 10){
                SearchWalkPoint();
                timer = 0;
            }
            anim.SetFloat("vertical", 0.5f);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint(){
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);


    
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Transform budino = SearchNearestMovePoint();
        //walkPoint = new Vector3(budino.position.x, transform.position.y, budino.position.z);
        float distToPoint = Vector3.Distance(transform.position, walkPoint);
        if (distToPoint>=10){
            walkPointSet = true;
        }
            

    }

    private Transform SearchNearestMovePoint(){
        GameObject [] movePointsGO = GameObject.FindGameObjectsWithTag("MovePoint");
        Transform [] movePointsT = new Transform[movePointsGO.Length];;

        for(int i = 0; i < movePointsGO.Length; i++){
            movePointsT[i] = movePointsGO[i].transform;
        }


        if(movePointsT.Length!=0){
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (Transform t in movePointsT)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            float distanza = Vector3.Distance(tMin.position, currentPos);
            if(distanza<4){
                return transform;
            } else return tMin;
        } else return transform;
    }

    public void StopIt(){
        if(self.isPlaying==false){
            anim.SetTrigger("gothit");
            StartCoroutine(OnTimeSound(self, smettila, 1f, 0f, false));   
        }
    }

    IEnumerator OnTimeSound(AudioSource src, AudioClip clp, float volume, float delay, bool delayAfter){
        if(delayAfter){
            src.PlayOneShot(clp, volume);
            yield return new WaitForSeconds(delay);
        } else{
            yield return new WaitForSeconds(delay);
            src.PlayOneShot(clp, volume);
        }
        
        
    }

    IEnumerator CountToDeath(){
        yield return new WaitForSeconds(shelfLife);
        DestroyObject(gameObject);
    }

    IEnumerator CheckIfCorrect(){
        yield return new WaitForSeconds(1f);
        if(agent.isOnNavMesh==false){
            DestroyObject(gameObject);
        }
    }
    
    public void RandomThingToSay(){
        float dist = Vector3.Distance(playerTrans.position, transform.position);

        if(dist<20){
            Debug.Log("distanza npc " + dist);
        }
        
        if(dist<20 && self.isPlaying==false && justTalked==false){ 
            int n = Random.Range(0, randomPhrase.Length);
            StartCoroutine(OnTimeSound(self, randomPhrase[n], 1f, 3f, false)); 
            justTalked=true; 
        }

        if(dist>20){
            justTalked=false;
        }
    }

    void OnTriggerStay(Collider collision){
        if (collision.gameObject.tag == "NotAcceptableNavMeshArea"){
            DestroyObject(gameObject);
        }
    }
    
}
