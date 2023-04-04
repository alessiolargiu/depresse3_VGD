using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FirstPersonController : MonoBehaviour {
    

    

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
    public float attackRange = 0.5f;

    public LayerMask enemyLayers;

    private Collider prevCol;

    private Transform currentEnemy;

    private bool altPunching = false;


    // Start is called before the first frame update
    void Start()
    {   
        controller = GetComponent<CharacterController>();
        //audioData = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //imposto la vita iniziale
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        smooth = 0.5f;
        fastJump=false;
        slowJump=false;
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
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
            verticalMovement=1f;
        } else if(slowJump==true){
            verticalMovement=0.5f;
        } else verticalMovement = Input.GetAxis("Vertical");



        //transform.Rotate(Vector3.up * followTransform);


        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, followTransform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 oldRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

        if(eulerRotation!=oldRotation && verticalMovement!=0 || horizontalMovement!=0 || (Input.GetKeyUp(KeyCode.F)|| Input.GetKeyUp(KeyCode.JoystickButton2))){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), 1);
        } 
        
        

        //float controllerVisualH = Input.GetAxis("HorizontalJ");
        //float controllerVisualV = Input.GetAxis("VerticalJ");
        float jump = Input.GetAxis("Jump"); //adesso non ci serve
        bool shift = Input.GetKey(KeyCode.LeftShift);
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        float rightHorizontal = Input.GetAxis("RightStickHorizontal");
        float rightVertical = Input.GetAxis("RightStickVertical");


        if((Input.GetKeyUp(KeyCode.F) && pugnoAir.isPlaying!=true) || Input.GetKeyUp(KeyCode.JoystickButton2) && controller.isGrounded && pugnoAir.isPlaying!=true){
            if(altPunching==false){
                pugnoAir.PlayOneShot(airleft, 1f);
                Attack(leftHand);
            } else {
                Attack(rightHand);
                pugnoAir.PlayOneShot(airright, 1f);
            }
            altPunching = !altPunching;
            anim.SetBool("altPunching", altPunching);
            anim.SetTrigger("punching");
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
        }
        

        Debug.Log("cacchina media " + smooth);





        if(controller.isGrounded){

            fastJump=false;
            slowJump=false;
            anim.SetFloat("jumping", 0);
            //UnityEngine.Debug.Log("isgrounded true");
            if((jump>0 || Input.GetKey(KeyCode.JoystickButton0)) && verticalMovement==1 && (!anim.GetCurrentAnimatorStateInfo(0).IsName("landing") && !anim.GetCurrentAnimatorStateInfo(0).IsName("fall"))){
                canJump=false;
                shift=true;
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
            } 

            if((jump>0 || Input.GetKey(KeyCode.JoystickButton0)) && verticalMovement==1 && (!anim.GetCurrentAnimatorStateInfo(0).IsName("landing") && !anim.GetCurrentAnimatorStateInfo(0).IsName("fall")) && (shift && Input.GetKey(KeyCode.JoystickButton1)==false)){
                canJump=false;
                shift=true;
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
                velocity.y=jumpForce;
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
        //DISABILITO PER ORA transform.Rotate(Vector3.up * mouseX);
        //transform.Rotate(Vector3.up * controllerVisualH);

        /*
        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseX * 100f, Vector3.up);

        followTransform.transform.rotation *= Quaternion.AngleAxis(mouseY * 100f , Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        angles.z=0;

        var angle = followTransform.transform.localEulerAngles.x;

        if(angle> 180 && angle <340){
            angles.x=340;
        } else if(angle<180 && angle > 40){angles.x=40;}

        followTransform.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);

        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0,0);
        */

        }
        
    }

    public void TakeDamage(int damage)
    {
        if(isActive){
        currentHealth -= damage;
        anim.SetTrigger("gothit");
        healthBar.SetHealth(currentHealth);
        }
    }

    public Inventory GetInventory()
    {
        return inventory;
    }


    private void OnTriggerEnter(Collider other)
    {
        
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

        /*if(collision.gameObject.CompareTag("enemy")){
            Debug.Log("so di star toccando il nemico");
            if(playerModel.GetComponent<FistCollisonCheck>().getDamage()){
                Debug.Log("so di togliere vit al nemico");
                collision.other.GetComponent<Maranzus>().TakeDamage(10);
            }
        }*/

    }


    void Attack(Transform attackPoint){
        
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider enemy in hitEnemies){

            currentEnemy = enemy.transform;

            /*var lookPos = currentEnemy.position - transform.position;
            lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(lookPos);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
            

            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, -currentEnemy.eulerAngles.y, transform.eulerAngles.z);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(eulerRotation), 1);
            */

            //transform.LookAt(currentEnemy + new Vector3(0,currentEnemy.eulerAngles.y, 0));



            enemy.GetComponent<Maranzus>().TakeDamage(10);
            pugno.PlayOneShot(pugno.clip, 1f);

        }

    }

    void onDrawGizmosSelected(){

        /*f(attackPoint==null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);*/
    }

    



    
   /* private void OnControllerColliderHit(ControllerColliderHit collision){
        //controllo per non far saltare più
        if (collision.gameObject.CompareTag("Floor")){
            canJump = false;
            UnityEngine.Debug.Log("Non ho toccato il floor");
        }
           
    }*/





}