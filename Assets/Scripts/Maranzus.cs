using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maranzus : MonoBehaviour
{
    public NavMeshAgent agent;

    public Animator anim;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public AudioSource self;
    public AudioClip pugnoSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool isDead;


    private void Awake()
    {
        player = GameObject.Find("PlayerProtagonista").transform;
        agent = GetComponent<NavMeshAgent>();
        alreadyAttacked=randomBoolean();
        if(alreadyAttacked==true) Invoke(nameof(ResetAttack), timeBetweenAttacks);
        isDead=false;
    }

    private void Update()
    {
        
        
        //agent.SetDestination(player.position);
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if(health>0){
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        } else if (health <= 0 && isDead==false){ 
            DestroyEnemy(); 
            isDead=true;
        }
    }

    private void Patroling()
    {

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
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {   
        agent.speed = 6f;
        float dist = Vector3.Distance(player.position, transform.position);
        anim.SetFloat("vertical", 1,  1f, Time.deltaTime * 10f );
        Vector3 targetDirection = player.position - transform.position;
        float singleStep = 1 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        anim.SetFloat("vertical", 0,  1f, Time.deltaTime * 10f);
        agent.SetDestination(transform.position);
        Vector3 targetDirection = player.position - transform.position;
        float singleStep = 1 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.LookAt(player);
        Debug.Log("devo attaccare fuori if " + alreadyAttacked);

        if (alreadyAttacked==false)
        {
            Debug.Log("devo attaccare dentro if " + alreadyAttacked);
            ///Attack code here
            anim.SetTrigger("punching");
            player.GetComponent<FirstPersonController>().TakeDamage(10);
            self.PlayOneShot(pugnoSound, 1f);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){

        if(health>0){
            anim.SetTrigger("gothit");
            self.Stop();
            self.PlayOneShot(hitSound);
            health -= damage;
        }
    }


    private void DestroyEnemy(){
        self.Stop();
        self.PlayOneShot(hitSound);
        anim.SetTrigger("dying");
    }

    public void DeathDetect(string msg){
        Debug.Log("Dovrei star morendo");
        if(msg=="finito"){
            
            DestroyObject(gameObject);
        }
    }

    public void Morte(int msg){
        Debug.Log("Dovrei star morendo budino");
        if(msg==1){
            DestroyObject(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    private bool randomBoolean(){
    if (Random.value >= 0.5){
        return true;
    }
    return false;
    }

    
}
