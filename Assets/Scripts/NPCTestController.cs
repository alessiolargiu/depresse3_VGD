using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTestController : MonoBehaviour {

    //Component utili
    private CharacterController controller;
    
    //Parametri di movimento
    public float movementSpeed = 1;
    public float jumpForce = 1;
    private Vector3 velocity;
    public float gravity = -9.81f;
    public float mouseSens = 100f;
    private float rotation = 0f;
    //Parametri partita
    private int score = 0;
    //Flag di controllo
    private bool canJump = false;
    public Animator anim;
    private float speedAnim;




    public float hor=0f;
    public float ver=0f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        // Input
        float horizontalMovement = hor;
        float verticalMovement = ver;
        //float jump = Input.GetAxis("Jump"); //adesso non ci serve
        bool shift = false;

        //roba prima persona
        /*

        //definizione del vettore di movimento;
        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        move*=movementSpeed;
        


        //gestione salto
        if(controller.isGrounded){
            if(jump>0){
                velocity.y=jumpForce;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);
            }
        } 
        else  {
            velocity.y += gravity*Time.deltaTime;
        }

        //aggiorno il movimento
        if(horizontalMovement!=0 || verticalMovement!=0){
                    controller.Move(move * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);


        //gravita
        Vector3 move;
        velocity.y += gravity*Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //movimento mouse 
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        
        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
       */



        //gravita
        Vector3 move;
        velocity.y += gravity*Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //movimento mouse 
        float mouseX = 1;
        float mouseY = 0;

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);


        transform.Rotate(Vector3.up * mouseX);

        if(shift){
            anim.SetFloat("vertical", verticalMovement);
        } else anim.SetFloat("vertical", verticalMovement*0.5f);
        anim.SetFloat("position", horizontalMovement);

        //Debug.Log(shift);
    }

    void FixedUpdate(){

    }
    
    private void OnTriggerEnter(Collider other)
    {
       /* if (other.CompareTag("Collect"))
        {
            other.gameObject.SetActive(false);
            
            //gestione del punteggio (nel ciclo aggiorniamo tutte le scritte)
            score++;
            foreach (var text in textScore)
            {
                text.SetText("Punteggio: " + score);
            }
        }*/
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