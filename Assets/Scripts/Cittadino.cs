using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cittadino : MonoBehaviour
{
    public NavMeshAgent agent;

    public Animator anim;

    public LayerMask whatIsGround;

    public AudioSource self;

    public AudioClip smettila;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    private void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        Patroling();
    }

    private void Patroling(){
        agent.speed=3f;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet){
            agent.SetDestination(walkPoint);
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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    public void StopIt(){
        if(self.isPlaying==false){
            anim.SetTrigger("gothit");
            StartCoroutine(OnTimeSound(self, smettila, 1f, 0f));   
        }
    }

    IEnumerator OnTimeSound(AudioSource src, AudioClip clp, float volume, float delay){
        yield return new WaitForSeconds(delay);
        src.PlayOneShot(clp, volume);
    }
    
}
