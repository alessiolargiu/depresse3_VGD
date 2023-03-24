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

    private float horizontalMovement;
    private float verticalMovement;

    private bool fastJump;

    private bool iJumped;

    private float smooth;



    private float timeSinceJump;
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
    }


    private void Update()
    {
        if(isActive){
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

        
        
        
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        

        //float controllerVisualH = Input.GetAxis("HorizontalJ");
        //float controllerVisualV = Input.GetAxis("VerticalJ");
        float jump = Input.GetAxis("Jump"); //adesso non ci serve
        bool shift = Input.GetKey(KeyCode.LeftShift);
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;



        if(Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.JoystickButton2) && controller.isGrounded && pugno.isPlaying!=true){
            anim.SetTrigger("punching");
            if(pugno.isPlaying!=true){pugno.PlayOneShot(pugno.clip, 1f);}
            
        } 
        

        //definizione del vettore di movimento;
        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;

        if(controller.isGrounded==false){
            running.enabled=false;
            walking.enabled=false;
            //shift=false;
        } 

        if((shift || Input.GetKey(KeyCode.JoystickButton1)) && (verticalMovement>0.5f || (horizontalMovement!=0f && verticalMovement>=0.5f))){
        
            
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
            anim.SetFloat("walking", smooth);
            anim.SetFloat("strafing", horizontalMovement + Time.deltaTime);

        } else {
            if(smooth>=0.5){
                smooth-=0.010f;
            } 
            Debug.Log("cacchina caldina " + smooth);
            
            fastJump = false;
            running.enabled=false;
             if((horizontalMovement!=0 || verticalMovement!=0) && controller.isGrounded){
                walking.enabled=true;
            } else walking.enabled=false;
            transform.Rotate(Vector3.up * horizontalMovement*0.1f);
            move*=movementSpeed*smooth;
            anim.SetFloat("walking", verticalMovement* smooth);
            anim.SetFloat("strafing", horizontalMovement*0.5f);
        }



        if(controller.isGrounded){
            iJumped=false;
            anim.SetFloat("jumping", 0);
            salto.enabled=false;
            //UnityEngine.Debug.Log("isgrounded true");
            if((jump>0 || Input.GetKey(KeyCode.JoystickButton0)) && verticalMovement>0){
                canJump=false;
                shift=true;

                iJumped=true;

                
                running.enabled=false;
                walking.enabled=false;
                float bobbo = UnityEngine.Random.Range(0, 2);
                Debug.Log("BOBBO è " + bobbo);
                if(bobbo==0){
                    salto.clip = altsalto;
                } else salto.clip = oldsalto;
                salto.enabled=true;
                //UnityEngine.Debug.Log("Sto saltando");
                velocity.y=jumpForce;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);

                
            } 
        } 
        else  {
            anim.SetFloat("jumping", 1);
            velocity.y += gravity*Time.deltaTime;
        }
        if(horizontalMovement!=0 || verticalMovement!=0){
                    controller.Move(move * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);


        
        rotation -= mouseY;
        //rotation -= controllerVisualV;
        //rotation = Mathf.Clamp(rotation, -90f, 90f);
        rotation = Mathf.Clamp(rotation, -30f, 15f);

        cameraTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        //transform.Rotate(Vector3.up * controllerVisualH);

        

        }
        
    }

    private void TakeDamage(int damage)
    {
        if(isActive){
        currentHealth -= damage;
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
    
   /* private void OnControllerColliderHit(ControllerColliderHit collision){
        //controllo per non far saltare più
        if (collision.gameObject.CompareTag("Floor")){
            canJump = false;
            UnityEngine.Debug.Log("Non ho toccato il floor");
        }
           
    }*/





}