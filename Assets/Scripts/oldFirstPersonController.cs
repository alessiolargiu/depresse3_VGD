using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class oldFirstPersonController : MonoBehaviour {
    

    

    public bool isActive = true;
    //Component utili
    private CharacterController controller;
    //public List<TextMeshProUGUI> textScore;
    
    //Parametri di movimento
    public float movementSpeed = 1f;
    public float movementRunSpeed = 1f;
    public float jumpForce = 1;

    private Vector3 velocity;
    public float gravity = -9.81f;
    public float mouseSens = 100f;
    public Transform cameraTransform;
    private float rotation = 0f;
    public Animator anim;
    public Animation pugnoAnim;

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
    public HUDInventoryShoe hudInvShoe;

    private Vector3 newPos;
    private Vector3 velocitydiocane = Vector3.zero;
    
    //Parametri partita
    private int score = 0;
    
    //Flag di controllo
    private bool canJump = false;

    //AudioSource audioData;
    
    int counter = 0;


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

    private bool iJumped;

    private float smooth;


    public Transform followTransform;

    private float momentum;

    public Transform playerModel;



    private float timeSinceJump;

    private bool doDamage;

    public Transform rightHand;
    public Transform leftHand;

    public GameObject sword;


    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    private Collider prevCol;

    private Transform currentEnemy;

    private bool altPunching = false;

    private bool outOfReach = true;
    private Collider lastEnemy;





    public Transform legL;
    public Transform legR;

    private int noWeaponCycle = 0;


    public GameObject bangFX;




    // Start is called before the first frame update
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
    }


    private void Awake()
    {
        inventory = new Inventory();
        hudInvWeapon.SetInventory(inventory);
        hudInvShield.SetInventory(inventory);
        hudInvPotion.SetInventory(inventory);
        hudInvHelmet.SetInventory(inventory);
        hudInvChest.SetInventory(inventory);
        hudInvShoe.SetInventory(inventory);
        verticalMovement=1;
    }


    private void Update()
    {

        //Debug.Log(playerModel.GetComponent<FistCollisonCheck>().getDamage());

        Debug.Log("POSIZIONE CUBO PLAYER: X " + followTransform.rotation.x + " Y " + followTransform.rotation.y + " Z " + followTransform.rotation.z);
         Debug.Log("POSIZIONE CUBO PLAYER: X " + (transform.rotation.x - followTransform.rotation.x) + " Y " + (transform.rotation.y - followTransform.rotation.y) + " Z " + (transform.rotation.z - followTransform.rotation.z));
        if(isActive){
        //momentum=0;
        //PROVA BARRA VITA
        if(Input.anyKeyDown || Input.GetAxis("Horizontal")>0 || Input.GetAxis("Vertical")>0){
            if (Input.GetKey(KeyCode.H) || Input.GetKey(KeyCode.JoystickButton10)){
                if(anim.GetBool("crouching")){
                    anim.SetBool("crouching", false);
                } else anim.SetBool("crouching", true);
            } else {
                anim.SetBool("crouching", false);
            }
        }

        
        if(Input.GetKey(KeyCode.Space)){
            //audioData.Play();
            //AudioSource.PlayClipAtPoint(audioData,transform.position);
            //UnityEngine.Debug.Log("started");
        }


        // Input

        
        Debug.Log("Il valore di fastjump è " + fastJump);        
        
        horizontalMovement = Input.GetAxis("Horizontal");
        if(fastJump==true){
            Debug.Log("Sono in fastjump");
            verticalMovement=1f;
        } else if(slowJump==true){
            Debug.Log("Sono in slowjump");
            verticalMovement=1.2f;
        } else if(anim.GetBool("crouching")==false){
            verticalMovement = Input.GetAxis("Vertical");
        } else verticalMovement = 0;



        //transform.Rotate(Vector3.up * followTransform);


        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, followTransform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 oldRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        if(eulerRotation!=oldRotation && verticalMovement!=0 || horizontalMovement!=0){
            Debug.Log("uuuuuh");
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
        
        

        //float controllerVisualH = Input.GetAxis("HorizontalJ");
        //float controllerVisualV = Input.GetAxis("VerticalJ");
        float jump = Input.GetAxis("Jump"); //adesso non ci serve
        bool shift = Input.GetKey(KeyCode.LeftShift);
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        float rightHorizontal = Input.GetAxis("RightStickHorizontal");
        float rightVertical = Input.GetAxis("RightStickVertical");


        if(((Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.JoystickButton2)) && pugnoAir.isPlaying==false) && controller.isGrounded && (anim.GetCurrentAnimatorStateInfo(0).IsName("kick")==false)){

            Debug.Log("il valore dell variabile nowepcycle è " + noWeaponCycle);
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

        if((Input.GetKeyUp(KeyCode.G) && (pugnoAir.isPlaying==false)) || Input.GetKeyUp(KeyCode.JoystickButton3) && controller.isGrounded && (anim.GetCurrentAnimatorStateInfo(0).IsName("sword")==false)){
            sword.SetActive(true);
            //pugnoAir.PlayOneShot(airleft, 1f);
            StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0.5f));
            StartCoroutine(Attack(sword.transform, 60, 1f));
            //anim.SetBool("altPunching", true);
            anim.SetTrigger("swording");

        } 

        

        //definizione del vettore di movimento;
        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;

        if(controller.isGrounded==false){
            running.enabled=false;
            walking.enabled=false;
            //shift=false;
        }  else {
            momentum=0;
        }

        if((shift || Input.GetKey(KeyCode.JoystickButton1)) && (verticalMovement!=0f || horizontalMovement!=0f) && controller.isGrounded || momentum!=0f){
            
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
            Debug.Log("cacchina freschina " + smooth);
            anim.SetFloat("walking", verticalMovement, 1f, Time.deltaTime * 10f );
            anim.SetFloat("strafing", horizontalMovement, 1f, Time.deltaTime * 10f );



        } else {
            if(smooth>=0.5){
                smooth-=0.010f;
            } 
            
            Debug.Log("cacchina caldina " + smooth);
            
            //fastJump = false;
            running.enabled=false;
             if((horizontalMovement!=0 || verticalMovement!=0) && controller.isGrounded){
                walking.enabled=true;
            } else walking.enabled=false;
            transform.Rotate(Vector3.up * horizontalMovement*0.1f);
            move*=movementSpeed*smooth;
            anim.SetFloat("walking", verticalMovement* smooth);
            anim.SetFloat("strafing", horizontalMovement*0.5f,  1f, Time.deltaTime * 10f );
        }
        

        if(verticalMovement!=0){
            transform.Rotate(Vector3.up * rightHorizontal*verticalMovement);
            transform.Rotate(Vector3.up * mouseX*verticalMovement);
            if(lastEnemy!=null){
            if(lastEnemy.GetComponent<Maranzus>().OutOfReach()){
                lastEnemy=null;
                lastEnemy.GetComponent<Maranzus>().setActiveEnemy(false);
                noWeaponCycle=0;
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(false, null);
            }
        }
        }
        

        Debug.Log("cacchina media " + smooth);





        if(controller.isGrounded){

            fastJump=false;
            slowJump=false;
            anim.SetFloat("jumping", 0);
            //UnityEngine.Debug.Log("isgrounded true");
            if((jump>0 || Input.GetKey(KeyCode.JoystickButton0)) && verticalMovement==1 && (!anim.GetCurrentAnimatorStateInfo(0).IsName("landing") && !anim.GetCurrentAnimatorStateInfo(0).IsName("fall")) && (shift || Input.GetKey(KeyCode.JoystickButton1)==true)){
                canJump=false;
                shift=true;
                slowJump=false;
                fastJump=true;
                iJumped=true;
                momentum=1f;
                
                running.enabled=false;
                walking.enabled=false;
                float bobbo = UnityEngine.Random.Range(0, 2);
                Debug.Log("BOBBO è " + bobbo);
                if(bobbo==0){
                    salto.clip = altsalto;
                } else salto.clip = oldsalto;
                salto.PlayOneShot(salto.clip, 1f);
                //UnityEngine.Debug.Log("Sto saltando");
                velocity.y=jumpForce;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);
                anim.SetTrigger("jumpTrigger");
            } else if((jump>0 || Input.GetKey(KeyCode.JoystickButton0)) && verticalMovement!=-1 && !(horizontalMovement!=0 && verticalMovement==0)  && (!anim.GetCurrentAnimatorStateInfo(0).IsName("landing") && !anim.GetCurrentAnimatorStateInfo(0).IsName("fall")) ){
                canJump=false;
                shift=true;
                fastJump=false;
                slowJump=true;
                iJumped=true;
                momentum = 0f;
                running.enabled=false;
                walking.enabled=false;
                float bobbo = UnityEngine.Random.Range(0, 2);
                Debug.Log("BOBBO è " + bobbo);
                if(bobbo==0){
                    salto.clip = altsalto;
                } else salto.clip = oldsalto;
                salto.PlayOneShot(salto.clip, 1f);
                //UnityEngine.Debug.Log("Sto saltando");
                velocity.y=jumpForce*0.8f;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);
                anim.SetTrigger("jumpTrigger");
            } 

            if(jump>0 || Input.GetKey(KeyCode.JoystickButton0)){
                
            }


        } 
        else  {
            anim.SetFloat("jumping", 1, 1f, Time.deltaTime * 10f);
            velocity.y += gravity*Time.deltaTime;
            if(momentum>0){
                /*
                move *=  movementRunSpeed * momentum;
                momentum -= 0.1f;
                verticalMovement=1f*momentum;
                controller.Move(move *  Time.deltaTime);
                */
            }
            
        }
       
        if(horizontalMovement!=0 || verticalMovement!=0){
                    controller.Move(move * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);

        //DISABILITO PER ORA rotation -= mouseY;
        //rotation -= controllerVisualV;
        //rotation = Mathf.Clamp(rotation, -90f, 90f);
        rotation = Mathf.Clamp(rotation, -30f, 15f);


        
        cameraTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);


        }
        
        /*if(lastEnemy!=null){
            if(lastEnemy.GetComponent<Maranzus>().OutOfReach()){
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(false, null);
            } else if(lastEnemy.GetComponent<Maranzus>().OutOfReach()==false) {
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(true, lastEnemy.transform);
            }
        }*/
        

        Debug.Log("il valore di outofreach è "+ outOfReach);

    }

    public void TakeDamage(int damage, Transform enemyCurrent)
    {
        if(isActive){
        followTransform.GetComponent<PlayerTarget>().SetAttackMode(true, enemyCurrent);
        enemyCurrent.GetComponent<Maranzus>().setActiveEnemy(true);
        currentHealth -= damage;
        anim.SetTrigger("gothit");
        healthBar.SetHealth(currentHealth);
        }
    }

    public Inventory GetInventory(){
        return inventory;
    }


    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Cutscene")){
            isActive=false;
            //CutSceneScript cutscn = other.gameObject;
            StartCoroutine(other.GetComponent<CutSceneScript>().cutsceneStart((paolino) => { if(paolino==true){Debug.Log("cacca"); isActive=true;} }));

            //CutSceneScript.cutsceneStart();
        }

        if (other.gameObject.CompareTag("pickup")){
            Debug.Log("dioc");
            pickupsound.enabled=true;
            pickupsound.PlayOneShot(picksoundclip, 0.7F);

        } 

    }

    private void onTriggerExit(Collider other){
        if (other.gameObject.CompareTag("pickup")){
            Debug.Log("boia");
            /*pickupsound.enabled=false;
            pickupsound.PlayOneShot(sound, 0.7F);*/
        } 
    }


    private void OnControllerColliderHit(ControllerColliderHit collision){   

    }

    private void OnCollisionStay(Collision collision){  

    }


    IEnumerator Attack(Transform attackPoint, int dmg, float time){
        verticalMovement=0;
        Debug.Log("sto in attack");
        yield return new WaitForSeconds(time);
        Debug.Log("sto in attack dopo 5 sec");
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider enemy in hitEnemies){
            float singleStep = 1 * Time.deltaTime;

            lastEnemy=enemy;

            Debug.Log("Dovrebbe succedere qualcosa");

            
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

        sword.SetActive(false);
    }

    IEnumerator OnTimeSound(AudioSource src, AudioClip clp, float volume, float delay){
        yield return new WaitForSeconds(delay);
        src.PlayOneShot(clp, volume);
    }
    
}