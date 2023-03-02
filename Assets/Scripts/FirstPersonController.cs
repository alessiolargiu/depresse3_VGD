


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class FirstPersonController : MonoBehaviour {

    //Component utili
    private CharacterController controller;
    //public List<TextMeshProUGUI> textScore;
    
    //Parametri di movimento
    public float movementSpeed = 1f;
    public float movementRunSpeed = 2f;
    public float jumpForce = 1;

    private Vector3 velocity;
    public float gravity = -9.81f;
    public float mouseSens = 100f;
    public Transform cameraTransform;
    private float rotation = 0f;
    public Animator anim;

    
    //Parametri partita
    private int score = 0;
    
    //Flag di controllo
    private bool canJump = false;

    //AudioSource audioData;
    
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        //audioData = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {



        if(Input.GetKey(KeyCode.Space)){
            //audioData.Play();
            //AudioSource.PlayClipAtPoint(audioData,transform.position);
            //UnityEngine.Debug.Log("started");
        }

        // Input
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump"); //adesso non ci serve
        bool shift = Input.GetKey(KeyCode.LeftShift);
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;


        

        //definizione del vettore di movimento;
        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        
        if(shift){
            move*=movementRunSpeed;
            anim.SetFloat("walking", verticalMovement);
        } else {
            move*=movementSpeed;
            anim.SetFloat("walking", verticalMovement*0.5f);
        }

        if(controller.isGrounded){
            //UnityEngine.Debug.Log("isgrounded true");
            if(jump>0){
                //UnityEngine.Debug.Log("Sto saltando");
                velocity.y=jumpForce;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);
            }
        } 
        else  {
            velocity.y += gravity*Time.deltaTime;
        }
        if(horizontalMovement!=0 || verticalMovement!=0){
                    controller.Move(move * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        
    }
    
    private void OnTriggerEnter(Collider other)
    {

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