using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FirstPersonController : MonoBehaviour {
    

    

    public bool isActive = true;
    //Component utili
    private CharacterController controller;
    
    //Parametri di movimento
    public float movementSpeed = 1f;
    public float movementRunSpeed = 1f;
    public float jumpForce = 1;

    private Vector3 velocity;
    public float gravity = -9.81f;
    public float mouseSens = 100f;
    //public Transform cameraTransform;
    private float rotation = 0f;
    public Animator anim;

    //parametri vita del player
    public HealthBar healthBar;
    private int maxHealth = 100;
    private int currentHealth;

    //rifermineto per l'inventario
    private Inventory inventory;
    public HUDInventoryWeapon hudInvWeapon;
    public HUDInventoryShield hudInvShield;
    public HUDInventoryPotion hudInvPotion;
    public HUDInventoryHelmet hudInvHelmet;
    public HUDInventoryChest hudInvChest;

    
    //Parametri partita
    private int score = 0;
    
    //Flag di controllo

    public AudioSource running;
    public AudioSource salto;
    public AudioClip altsalto;
    public AudioClip oldsalto;
    public AudioSource walking;
    public AudioSource waaa;
    public AudioSource pickupsound;
    public AudioClip picksoundclip;
    public AudioSource pugno;
    public AudioSource pugnoAir;
    public AudioClip airleft;
    public AudioClip airright;

    private float horizontalMovement;
    private float verticalMovement;

    private bool fastJump;
    private bool slowJump;

    private float smooth;


    public Transform followTransform;

    private float momentum;


    public Transform rightHand;
    public Transform leftHand;

    public GameObject sword;


    public float attackRange = 0.5f;

    public LayerMask enemyLayers;
    public LayerMask cittadinoLayers;

    private Collider prevCol;

    private Transform currentEnemy;

    private bool altPunching = false;

    private bool outOfReach = true;
    private Collider lastEnemy;

    public Transform legL;
    public Transform legR;

    private int noWeaponCycle = 0;


    public GameObject bangFX;

    private Vector3 move;


    //userflag
    public bool usingController;

    //inputs
    bool jump;
    bool shift;
    float mouseX;
    float mouseY;
    float rightHorizontal;
    float rightVertical;
    bool punchingKey;
    bool swordKey;
    bool crouchKey;

    void Start(){   
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //imposto la vita iniziale
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        smooth = 0.5f;
        fastJump=false;
        slowJump=false;
        bangFX.SetActive(false);
        sword.SetActive(false);
        verticalMovement=1;
    }

    private void Awake(){
        inventory = new Inventory();
        inventory.player = this;
        hudInvWeapon.SetInventory(inventory);
        hudInvShield.SetInventory(inventory);
        hudInvPotion.SetInventory(inventory);
        hudInvHelmet.SetInventory(inventory);
        hudInvChest.SetInventory(inventory);
        verticalMovement=1;
    }

    private void Update(){
        if(isActive){
            InputGet();
            AdjustCamera();
            CrouchCheck();  
            AttackType();
            AudioEnvCheck();
            EnemyRangeCheck();
            Movement();
            Jumping();
        }

    }

    public void TakeDamage(int damage, Transform enemyCurrent, int whoIs){
        if(isActive){
        switch(whoIs){
            case 1:
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(true, enemyCurrent);
                enemyCurrent.GetComponent<Maranzus>().setActiveEnemy(true);
                currentHealth -= damage;
                anim.SetTrigger("gothit");
                healthBar.SetHealth(currentHealth);
                break;
            case 2:
                currentHealth -= damage;
                anim.SetTrigger("gothit");
                healthBar.SetHealth(currentHealth);
                break;
        }

        }
    }

    public Inventory GetInventory(){
        return inventory;
    }

    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Cutscene")){
            isActive=false;
            StartCoroutine(other.GetComponent<CutSceneScript>().cutsceneStart((paolino) => { if(paolino==true){isActive=true;}}));
        }

        if (other.gameObject.CompareTag("pickup")){
            pickupsound.enabled=true;
            pickupsound.PlayOneShot(picksoundclip, 0.7F);

        } 

    }

    private void AudioEnvCheck(){
        if(controller.isGrounded==false){
            running.enabled=false;
            walking.enabled=false;
        }  else {
            momentum=0;
        }
    }

    private void EnemyRangeCheck(){
        if(verticalMovement!=0){
            if(lastEnemy!=null){
            if(lastEnemy.GetComponent<Maranzus>().OutOfReach()){
                lastEnemy=null;
                lastEnemy.GetComponent<Maranzus>().setActiveEnemy(false);
                noWeaponCycle=0;
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(false, null);
            }
        }
        }
    }
    
    private void InputGet(){
        horizontalMovement = Input.GetAxis("Horizontal");
        if(fastJump==true){
            verticalMovement=1f;
        } else if(slowJump==true){
            verticalMovement=1.2f;
        } else if(anim.GetBool("crouching")==false){
            verticalMovement = Input.GetAxis("Vertical");
        } else verticalMovement = 0;

        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        rightHorizontal = Input.GetAxis("RightStickHorizontal");
        rightVertical = Input.GetAxis("RightStickVertical");

        if(usingController){
            jump = Input.GetKeyUp(KeyCode.JoystickButton0);
            shift = Input.GetKey(KeyCode.JoystickButton1);
            punchingKey = Input.GetKeyUp(KeyCode.JoystickButton2);
            swordKey = Input.GetKeyUp(KeyCode.JoystickButton3);
            crouchKey = Input.GetKey(KeyCode.JoystickButton10);
        } else {
            shift = Input.GetKey(KeyCode.LeftShift);
            jump = Input.GetKeyUp(KeyCode.Space); 
            punchingKey = Input.GetKeyUp(KeyCode.F);
            crouchKey = Input.GetKey(KeyCode.H);
            swordKey = Input.GetKeyUp(KeyCode.G);
        }

    }

    private void CrouchCheck(){
        if(Input.anyKeyDown || Input.GetAxis("Horizontal")>0 || Input.GetAxis("Vertical")>0){
            if (crouchKey){
                if(anim.GetBool("crouching")){
                    anim.SetBool("crouching", false);
                } else anim.SetBool("crouching", true);
            } else {
                anim.SetBool("crouching", false);
            }
        }
    }

    private void Movement(){
        //definizione del vettore di movimento;
        move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        if((shift) && (verticalMovement!=0f || horizontalMovement!=0f) && controller.isGrounded || momentum!=0f){
            
            if(horizontalMovement+verticalMovement==2 || horizontalMovement+verticalMovement==-2){
                transform.Rotate(Vector3.up * horizontalMovement*0.6f);
            } else transform.Rotate(Vector3.up * horizontalMovement*1f);
            

            if(smooth<=1){
                smooth+=0.005f;
            } else {
                if(controller.isGrounded){
                running.enabled=true;
                walking.enabled=false;
            }
            }
            move*=movementRunSpeed*smooth;
            anim.SetFloat("walking", verticalMovement, 1f, Time.deltaTime * 10f );
            anim.SetFloat("strafing", horizontalMovement, 1f, Time.deltaTime * 10f );



        } else {
            if(smooth>=0.5){
                smooth-=0.010f;
            } 
            
            running.enabled=false;
             if((horizontalMovement!=0 || verticalMovement!=0) && controller.isGrounded){
                walking.enabled=true;
            } else walking.enabled=false;
            transform.Rotate(Vector3.up * horizontalMovement*0.1f);
            move*=movementSpeed*smooth;
            anim.SetFloat("walking", verticalMovement* smooth);
            anim.SetFloat("strafing", horizontalMovement*0.5f,  1f, Time.deltaTime * 10f );
        }

        if(horizontalMovement!=0 || verticalMovement!=0){
            transform.Rotate(Vector3.up * rightHorizontal*verticalMovement);
            transform.Rotate(Vector3.up * mouseX*verticalMovement);
            controller.Move(move * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);
        rotation = Mathf.Clamp(rotation, -30f, 15f);
    }

    private void Jumping(){
        if(controller.isGrounded){
            fastJump=false;
            slowJump=false;
            anim.SetFloat("jumping", 0);
            if(jump /*&& verticalMovement==1*/  && verticalMovement>0 && (!anim.GetCurrentAnimatorStateInfo(0).IsName("landing") && !anim.GetCurrentAnimatorStateInfo(0).IsName("fall")) && shift==true){
                shift=true;
                slowJump=false;
                fastJump=true;
                momentum=1f;
                
                running.enabled=false;
                walking.enabled=false;
                float bobbo = UnityEngine.Random.Range(0, 2);
                if(bobbo==0){
                    salto.clip = altsalto;
                } else salto.clip = oldsalto;
                salto.PlayOneShot(salto.clip, 1f);
                velocity.y=jumpForce;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);
                anim.SetTrigger("jumpTrigger");



            } else if(jump /*&& verticalMovement!=-1 && !(horizontalMovement!=0 && verticalMovement==0) */  && verticalMovement>=0 && (!anim.GetCurrentAnimatorStateInfo(0).IsName("landing") && !anim.GetCurrentAnimatorStateInfo(0).IsName("fall")) ){
                shift=true;
                fastJump=false;
                slowJump=true;
                momentum = 0f;
                running.enabled=false;
                walking.enabled=false;
                float bobbo = UnityEngine.Random.Range(0, 2);
                if(bobbo==0){
                    salto.clip = altsalto;
                } else salto.clip = oldsalto;
                salto.PlayOneShot(salto.clip, 1f);
                velocity.y=jumpForce*0.8f;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);
                anim.SetTrigger("jumpTrigger");
            } 

        } 
        else  {
            anim.SetFloat("jumping", 1, 1f, Time.deltaTime * 10f);
            velocity.y += gravity*Time.deltaTime;
        }
    }

    private void AttackType(){
        if(((punchingKey) && pugnoAir.isPlaying==false) && controller.isGrounded && (anim.GetCurrentAnimatorStateInfo(0).IsName("kick")==false)){
            switch(noWeaponCycle){
                case 0:
                StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0f));
                StartCoroutine(Attack(leftHand, 20, 0.1f));
                anim.SetBool("altPunching", true);
                anim.SetTrigger("punching");
                noWeaponCycle++;
                break;
                
                case 1:
                StartCoroutine(OnTimeSound(pugnoAir, airright, 1f, 0f));
                StartCoroutine(Attack(rightHand, 20, 0.1f));
                anim.SetBool("altPunching", false);
                anim.SetTrigger("punching");
                noWeaponCycle++;
                break;

                case 2:
                StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0.5f));
                int rand = UnityEngine.Random.Range(0, 2);
                if(rand==1){
                    anim.SetBool("altPunching", false);
                    StartCoroutine(Attack(legR, 40, 0.5f));
                } else {
                    anim.SetBool("altPunching", true);
                    StartCoroutine(Attack(legL, 40, 0.5f));
                }
                anim.SetTrigger("kicking");
                noWeaponCycle=0;
                break;
            
            }
            
        }

        if(((swordKey) && pugnoAir.isPlaying==false) && controller.isGrounded && (anim.GetCurrentAnimatorStateInfo(0).IsName("swording")==false)){
            sword.SetActive(true);
            StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0.5f));
            StartCoroutine(Attack(sword.transform, 60, 1f));
            anim.SetTrigger("swording");
        }

    }

    private void AdjustCamera(){
        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, followTransform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 oldRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        if(eulerRotation!=oldRotation && verticalMovement!=0 || horizontalMovement!=0){
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }


    IEnumerator Attack(Transform attackPoint, int dmg, float time){
        verticalMovement=0;
        yield return new WaitForSeconds(time);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        Collider[] hitCittadino = Physics.OverlapSphere(attackPoint.position, attackRange, cittadinoLayers);
        foreach(Collider enemy in hitEnemies){
            float singleStep = 1 * Time.deltaTime;

            lastEnemy=enemy;
            
            float health = enemy.GetComponent<Maranzus>().TakeDamage(dmg);
            
            if(health>0){
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(true, enemy.transform);
                enemy.GetComponent<Maranzus>().setActiveEnemy(true);
            } else if(health<=0){
                lastEnemy=null;
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(false, null);
                enemy.GetComponent<Maranzus>().setActiveEnemy(false);
            }
            
            pugno.PlayOneShot(pugno.clip, 1f);

            bangFX.SetActive(true);
            bangFX.transform.position=attackPoint.position;
            yield return new WaitForSeconds(4f);
            bangFX.SetActive(false);

        }

        foreach(Collider cittadino in hitCittadino){
            cittadino.GetComponent<Cittadino>().StopIt();
        }

        sword.SetActive(false);
    }

    IEnumerator OnTimeSound(AudioSource src, AudioClip clp, float volume, float delay){
        yield return new WaitForSeconds(delay);
        src.PlayOneShot(clp, volume);
    }
    
}