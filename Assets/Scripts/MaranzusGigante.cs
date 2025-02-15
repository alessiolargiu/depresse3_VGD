using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MaranzusGigante : MonoBehaviour{

    public GameObject cutsceneafter;


    public CameraShake shake;
    public float shakeIntns = 5;
    public float shakeTime = 1;


    public NavMeshAgent agent;

    public Animator anim;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public AudioSource self;
    public AudioClip pugnoSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public static string whoIsAttacking = null;
    private string myself;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool isDead;
    private bool outOfReach;

    public GameObject marker;
    private Renderer markerColor;

    private bool imActive;

    [Header("Caratteristiche Maranza")]
    //se è un bastardo ti attacca pure se qualcun altro ti sta attaccando
    public bool bastardo;
    public float health;
    public float maxHealth;
    public int dmg;
    public HealthBar healthBar;

    [Header("Gestione Sandalo")]
    public AudioClip sandaloSound;
    public bool isASandaloThrower;
    public Transform attackPoint;
    public float throwForce;
    private float throwForceAbs;
    public float throwUpwardForce;
    public GameObject objectToThrow;
    public float throwCooldown;
    bool readyToThrow;


    private bool stop;
    public float fistRange;
    public Transform leg;

    public LayerMask playerLayer;




    private void Awake(){
        player = GameObject.Find("PlayerProtagonista").transform;
        markerColor = marker.GetComponent<Renderer>();
        markerColor.material.SetColor("_Color", Color.red);
        agent = GetComponent<NavMeshAgent>();
        alreadyAttacked=randomBoolean();
        if(alreadyAttacked==true) Invoke(nameof(ResetAttack), timeBetweenAttacks);
        isDead=false;
        outOfReach=true;
        imActive=true;
        readyToThrow=true;
        //attackPoint=transform;

        myself = GetInstanceID().ToString();
        //healthBar.slider.maxValue = maxHealth;
        //healthBar.slider.value = maxHealth;
    }

    private void Update(){
        if(imActive==false){
            marker.SetActive(false);
        }
        if(Resources.FindObjectsOfTypeAll<HealthBarGigante>()[0] != null)
        {
            healthBar = Resources.FindObjectsOfTypeAll<HealthBarGigante>()[0].GetComponent<HealthBar>();
            healthBar.slider.maxValue = maxHealth;
            healthBar.slider.value = health;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if(health>0){

            if(bastardo){
                if (!playerInSightRange && !playerInAttackRange) {
                    Patroling();
                }
                if (playerInSightRange && !playerInAttackRange) {
                    ChasePlayer();
                }
                if (playerInAttackRange && playerInSightRange) AttackPlayer();
            } else {
                if (!playerInSightRange && !playerInAttackRange) {
                    if(whoIsAttacking==myself){
                        whoIsAttacking=null;
                    }
                    Patroling();
                }
                if (playerInSightRange && !playerInAttackRange && (whoIsAttacking==null || whoIsAttacking==myself) ) {
                    whoIsAttacking=myself;
                    ChasePlayer();
                }
                if (playerInAttackRange && playerInSightRange && (whoIsAttacking==myself)) AttackPlayer();
            }

        } else if (health <= 0 && isDead==false){ 
            DestroyEnemy(); 
            isDead=true;
        }
    }

    private void Patroling(){

        outOfReach=true;
        agent.speed=1f;
        marker.SetActive(false);
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

    private void ChasePlayer(){   
        outOfReach=false;
        if(imActive) marker.SetActive(true);
        agent.speed = 3f;
        float dist = Vector3.Distance(player.position, transform.position);

        if(stop!=true){
            StartCoroutine(shakeRun(2f, 0.3f, 0.3f));
            anim.SetFloat("vertical", 1,  1f, Time.deltaTime * 10f );  
        } else anim.SetFloat("vertical", 0,  1f, Time.deltaTime * 10f );

        /*
        Vector3 targetDirection = player.position - transform.position;
        float singleStep = 1 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);*/
        agent.SetDestination(player.position);
        if(randomBoolean() && readyToThrow && isASandaloThrower){
            ThrowSandalo();
        }

    }

    private void AttackPlayer(){
        if(imActive) marker.SetActive(true);
        //Make sure enemy doesn't move
        anim.SetFloat("vertical", 0,  1f, Time.deltaTime * 10f);
        agent.SetDestination(transform.position);
        Vector3 targetDirection = player.position - transform.position;
        float singleStep = 1 * Time.deltaTime;
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);
        //NO QUESTO NO
        //transform.LookAt(player);

        if (alreadyAttacked==false)
        {
            ///Attack code here
            alreadyAttacked=true;
            anim.SetTrigger("punching");

            ///End of attack code
            StartCoroutine(Attack(leg, 1.5f));
            alreadyAttacked = true;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public float TakeDamage(int damage){
        Debug.Log("Sto subendo danno e sono il gigante");
       if(health>0){
            anim.SetTrigger("gothit");
            self.Stop();
            self.PlayOneShot(hitSound);
            health -= damage;
        }

        if(health>70 && health<100){
            markerColor.material.SetColor("_Color", Color.red);
        } else if(health>40 && health<70){
            markerColor.material.SetColor("_Color", Color.yellow);
        } else if(health>0 && health<40){
            markerColor.material.SetColor("_Color", Color.black);
        }
        if(health > 0)
        {
            healthBar.SetHealth((int)health);
        }
        else
        {
            healthBar.SetHealth(0);
        }
        
        return health;
    }

    private void ThrowSandalo(){

        self.PlayOneShot(sandaloSound, 1f);
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, transform.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        projectile.GetComponent<SandaloScript>().beenUsed=true;

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow(){
        readyToThrow = true;
    }

    private void DestroyEnemy(){
        self.Stop();
        whoIsAttacking=null;
        self.PlayOneShot(deathSound);
        StartCoroutine(shakeRun(10f, 2f, 6.5f));
        anim.SetTrigger("dying");
    }

    public void DeathDetect(string msg){
        if(msg=="finito"){
            cutsceneafter.SetActive(true);
            DestroyObject(gameObject);
        }
    }

    public void Morte(int msg){
        if(msg==1){
            cutsceneafter.SetActive(true);
            DestroyObject(gameObject);
        }
    }

    private void OnDrawGizmosSelected(){
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

    public bool OutOfReach(){
        return outOfReach;
    }

    public void setActiveEnemy(bool act){
        imActive=act;
    }

    IEnumerator Attack(Transform point, float time){
        /*yield return new WaitForSeconds(time);
        //shake.ShakeCamera(shakeIntns, shakeTime);
        Collider[] hitPlayer = Physics.OverlapSphere(point.position, fistRange, playerLayer);

        foreach(Collider playerIt in hitPlayer){
            float singleStep = 1 * Time.deltaTime;
            
            playerIt.GetComponentInParent<FirstPersonController>().TakeDamage(dmg, transform, 3);
        }*/
        self.PlayOneShot(pugnoSound, 1f);
        yield return new WaitForSeconds(timeBetweenAttacks);
        ResetAttack();
    }


    IEnumerator shakeRun(float ints, float timeshk, float time){
        yield return new WaitForSeconds(time);
        //shake.ShakeCamera(ints, timeshk);
    }
    
}
