using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Maranzus : MonoBehaviour
{
    public NavMeshAgent agent;

    public Animator anim;

    public Transform player;
    private Transform stranglingPoint;

    public LayerMask whatIsGround, whatIsPlayer;

    public AudioSource self;
    public AudioClip pugnoSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public static string whoIsAttacking = null;
    private string myself;

    private bool dead;


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
    public int dmg;

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
    public Transform fist;

    public LayerMask playerLayer;

    public ConeOfView coneOfView;

    public bool awareOfPlayer;

    private bool stopThrow;

    private bool strangled;

    public float speedWalking;
    public float speedRunning;



    public bool enemyIsDying;
    public int shelfLife;


    public bool isCounting;

    public GameObject [] pozioni;
    private float timer;


    private void Awake(){
        if(enemyIsDying){
            StartCoroutine(CountToDeath());
        }
        player = GameObject.Find("PlayerProtagonista").transform;
        stranglingPoint = GameObject.Find("StranglingPoint").transform;
        markerColor = marker.GetComponent<Renderer>();
        markerColor.material.SetColor("_Color", Color.red);
        agent = GetComponent<NavMeshAgent>();
        alreadyAttacked=randomBoolean();
        if(alreadyAttacked==true) Invoke(nameof(ResetAttack), timeBetweenAttacks);
        isDead=false;
        outOfReach=true;
        imActive=true;
        readyToThrow=true;
        stopThrow=true;
        //attackPoint=transform;
        myself = GetInstanceID().ToString();
        StartCoroutine(CheckIfCorrect());
        Patroling();
    }

    private void Update(){

        timer += Time.deltaTime;


        if(imActive==false){
            marker.SetActive(false);
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if(health>0 && player.GetComponentInParent<FirstPersonController>().GetActiveInternal() == true){

            if(bastardo){
                if (!playerInSightRange && !playerInAttackRange) {
                    Patroling();
                    awareOfPlayer=false;
                }
                if (playerInSightRange && !playerInAttackRange && (coneOfView.GetPlayerSight()==true || awareOfPlayer)) {
                    ChasePlayer();
                    awareOfPlayer=true;
                }
                if (playerInAttackRange && playerInSightRange && strangled==false) AttackPlayer();
            } else {
                if (!playerInSightRange && !playerInAttackRange) {
                    if(whoIsAttacking==myself){
                        whoIsAttacking=null;
                    }
                    Patroling();
                    awareOfPlayer=false;
                }
                if (playerInSightRange && !playerInAttackRange && (whoIsAttacking==null || whoIsAttacking==myself) && (coneOfView.GetPlayerSight()==true || awareOfPlayer)){
                    whoIsAttacking=myself;
                    awareOfPlayer=true;
                    ChasePlayer();
                }
                if (playerInAttackRange && playerInSightRange && (whoIsAttacking==myself) && strangled==false) AttackPlayer();
            }


            
        } else if (health <= 0 && isDead==false){ 
            DestroyEnemy(); 
            isDead=true;
        }


    }

    private void LateUpdate(){
        if(strangled){
                transform.rotation = stranglingPoint.rotation;
                transform.position = stranglingPoint.position + new Vector3(0f, 0f, 0f);
        }
        
    }

    private void Patroling(){

        outOfReach=true;
        agent.speed=speedWalking;
        marker.SetActive(false);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet){
            agent.SetDestination(walkPoint);
            if(agent.pathPending || timer >= 10){
                SearchWalkPoint();
                timer=0;
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

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer(){   
        awareOfPlayer=true;
        outOfReach=false;
        if(imActive) marker.SetActive(true);
        agent.speed = speedRunning;
        float dist = Vector3.Distance(player.position, transform.position);

        anim.SetFloat("vertical", 1,  1f, Time.deltaTime * 10f );  

        
        Vector3 targetDirection = player.position - transform.position;
        float singleStep = 1 * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
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
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.LookAt(player);

        if (alreadyAttacked==false)
        {
            ///Attack code here
            anim.SetTrigger("punching");
            //player.GetComponent<FirstPersonController>().TakeDamage(dmg, transform, 1);
            self.PlayOneShot(pugnoSound, 1f);
            StartCoroutine(Attack(fist, 0.35f));
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    IEnumerator Attack(Transform point, float time){
        yield return new WaitForSeconds(time);
        Collider[] hitPlayer = Physics.OverlapSphere(point.position, fistRange, playerLayer);

        foreach(Collider playerIt in hitPlayer){
            float singleStep = 1 * Time.deltaTime;
            if(strangled){
                playerIt.GetComponentInParent<FirstPersonController>().TakeDamage(dmg*0.1f, transform, 1);
            } else playerIt.GetComponentInParent<FirstPersonController>().TakeDamage(dmg, transform, 1);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public float TakeDamage(int damage){
        bastardo=true;
        awareOfPlayer=true;
        if(health>0){
            anim.SetTrigger("gothit");
            self.Stop();
            self.PlayOneShot(hitSound);
            health -= damage;
        }

        if(health>50 && health<100){
            markerColor.material.SetColor("_Color", Color.red);
        } else if(health>5 && health<50){
            markerColor.material.SetColor("_Color", Color.yellow);
        } else if(health>0 && health<5){
            markerColor.material.SetColor("_Color", Color.black);
        }

        return health;
    }

    public void GivePozione(){
        if(dead==false){
            dead=true;
            if(pozioni.Length > 0)
            {
                int nPozione = Random.Range(0, pozioni.Length);
                GameObject pozioneToLaunch = Instantiate(pozioni[nPozione], attackPoint.position, transform.rotation);
                pozioneToLaunch.transform.Rotate(new Vector3(180f, 0f, 0f));
                pozioneToLaunch.SetActive(true);
            }
            
        }
        
    }

    private void ThrowSandalo(){
        if(stopThrow){
            anim.SetTrigger("throw");
            StartCoroutine(SandaloLaunch(0.8f));
        }
    }

    IEnumerator SandaloLaunch(float time){
        stopThrow=false;
        yield return new WaitForSeconds(time);
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
        stopThrow=true;
    }

    private void ResetThrow(){
        readyToThrow = true;
    }

    private void DestroyEnemy(){
        self.Stop();
        whoIsAttacking=null;
        self.PlayOneShot(deathSound);
        if(isCounting){
            GameObject.Find("MissionCounter").GetComponent<MissionCount>().addDeath();
        }
        anim.SetTrigger("dying");
    }

    public void DeathDetect(string msg){
        if(msg=="finito"){
            DestroyObject(gameObject);
        }
    }

    public void Morte(int msg){
        if(msg==1){
            GivePozione();
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

    void OnCollisionEnter(Collision collision) {
        /*if((collision.gameObject.tag!="player" || collision.gameObject.tag!="ground" )){
            anim.SetFloat("vertical", 0f);
            stop = true;
        } else */ 
        //stop = false;
    }

    public bool GetAwareness(){
        return awareOfPlayer;
    }

    public void BeingStrangled(){
        anim.SetTrigger("strangled");
        strangled=true;
        StartCoroutine(DeathStrangled(10f));
    }

    IEnumerator DeathStrangled(float time){
        yield return new WaitForSeconds(time);
        player.GetComponent<FirstPersonController>().SetDontMove(false);
        if(isCounting){
            GameObject.Find("MissionCounter").GetComponent<MissionCount>().addDeath();
        }
        GivePozione();
        DestroyObject(gameObject);
    }
    

    IEnumerator CountToDeath(){
        yield return new WaitForSeconds(shelfLife);
        if(awareOfPlayer==false){
            if(isCounting){
                GameObject.Find("MissionCounter").GetComponent<MissionCount>().addDeath();
            }
            DestroyObject(gameObject);
        } else StartCoroutine(CountToDeath());
    }

    IEnumerator CheckIfCorrect(){
        yield return new WaitForSeconds(1f);
        if(agent.isOnNavMesh==false){
            if(isCounting){
                GameObject.Find("MissionCounter").GetComponent<MissionCount>().addDeath();
            }
            DestroyObject(gameObject);
        }
    }
}
