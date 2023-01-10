using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Component utili
    private CharacterController controller;
    //public List<TextMeshProUGUI> textScore;
    
    //Parametri di movimento
    public float movementSpeed = 1;
    public float jumpForce = 1;

    private Vector3 velocity;
    private float gravity = -9.81f;
    public float mouseSens = 100f;
    public Transform cameraTransform;
    private float rotation = 0f;


    
    //Parametri partita
    private int score = 0;
    
    //Flag di controllo
    private bool canJump = false;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Input
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump"); //adesso non ci serve
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;


        //definizione del vettore di movimento
        Vector3 move = transform.right * horizontalMovement + transform.forward * verticalMovement + transform.up * jump * jumpForce;
        /*Vector3 move = new Vector3(horizontalMovement * movementSpeed, 
                                        jump * jumpForce, 
                                        verticalMovement * movementSpeed);
        */

        move*=movementSpeed;

         controller.Move(move * Time.deltaTime);

        if (controller.isGrounded) {
            velocity.y = 0;
        } 
        else {
            velocity.y = gravity * Time.deltaTime;
        }



        //controller.Move(move);
        //velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity);
        controller.Move(move * Time.deltaTime);

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        
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
        UnityEngine.Debug.Log("Ho toccato qualcosa");
    }
    
   /* private void OnControllerColliderHit(ControllerColliderHit collision){
        //controllo per non far saltare più
        if (collision.gameObject.CompareTag("Floor")){
            canJump = false;
            UnityEngine.Debug.Log("Non ho toccato il floor");
        }
           
    }*/
}