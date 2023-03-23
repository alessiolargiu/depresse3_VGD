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

    public Vector3 offset;
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
    public AudioSource walking;
    public AudioSource waaa;



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
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        //float controllerVisualH = Input.GetAxis("HorizontalJ");
        //float controllerVisualV = Input.GetAxis("VerticalJ");
        float jump = Input.GetAxis("Jump"); //adesso non ci serve
        bool shift = Input.GetKey(KeyCode.LeftShift);
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        //definizione del vettore di movimento;
        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;

       if(controller.isGrounded==false){
        running.enabled=false;
        walking.enabled=false;
       } 
        
        if((shift || Input.GetKey(KeyCode.JoystickButton1)) && verticalMovement>0.5f){

            if(controller.isGrounded){
            running.enabled=true;
            walking.enabled=false;
            }
            if(horizontalMovement+verticalMovement==2 || horizontalMovement+verticalMovement==-2){
                transform.Rotate(Vector3.up * horizontalMovement*0.6f);
            } else transform.Rotate(Vector3.up * horizontalMovement*1f);
            
            move*=movementRunSpeed;
            anim.SetFloat("walking", verticalMovement);
            anim.SetFloat("strafing", horizontalMovement + Time.deltaTime);
        } else {
            running.enabled=false;
             if((horizontalMovement!=0 || verticalMovement!=0) && controller.isGrounded){
                walking.enabled=true;
            } else walking.enabled=false;
        
            transform.Rotate(Vector3.up * horizontalMovement*0.1f);
            move*=movementSpeed;
            anim.SetFloat("walking", verticalMovement*0.5f);
            anim.SetFloat("strafing", horizontalMovement*0.5f);
        }



        if(controller.isGrounded){
            anim.SetFloat("jumping", 0);
            salto.enabled=false;
            //UnityEngine.Debug.Log("isgrounded true");
            if((jump>0 || Input.GetKey(KeyCode.JoystickButton0)) && verticalMovement>0){
                running.enabled=false;
                walking.enabled=false;
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
    }

    private void OnControllerColliderHit(ControllerColliderHit collision){   
        //UnityEngine.Debug.Log("Ho toccato qualcosa");
       /* if (collision.gameObject.CompareTag("Floor")){
            canJump=true;
            UnityEngine.Debug.Log("ho toccato il floor");
        }*/
    }
    
   /* private void OnControllerColliderHit(ControllerColliderHit collision){
        //controllo per non far saltare più
        if (collision.gameObject.CompareTag("Floor")){
            canJump = false;
            UnityEngine.Debug.Log("Non ho toccato il floor");
        }
           
    }*/





}