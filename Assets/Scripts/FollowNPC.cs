using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowNPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public Transform puntoArrivo;
    public Transform player;
    public LayerMask whatIsGround;

    public float distMinima;


    private bool outOfReach;

    private bool imActive;

    private float distPlayer;
    private float distPunto;
    private void Awake(){
    
        agent = GetComponent<NavMeshAgent>();
        
        imActive=true;

    }

    private void Update(){
        distPlayer = Vector3.Distance(player.position, transform.position);
        distPunto = Vector3.Distance(puntoArrivo.position, transform.position);
        Debug.Log(distPunto);
        GoToTarget();
    }

    private void SearchWalkPoint(){
    }

    private void GoToTarget(){   

        if(distPunto>=0.5f && distPlayer<=distMinima){
            agent.speed = 1.5f;
            anim.SetFloat("vertical", 0.5f,  1f, Time.deltaTime * 10f );  
            Vector3 targetDirection = puntoArrivo.position - transform.position;
            float singleStep = 1 * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            agent.SetDestination(puntoArrivo.position);
        } else {
            agent.speed = 0f;
            anim.SetFloat("vertical", 0,  1f, Time.deltaTime * 10f );  
            Vector3 targetDirection = puntoArrivo.position - transform.position;
            float singleStep = 1 * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            agent.SetDestination(puntoArrivo.position);
        }
        

    }


    
}
