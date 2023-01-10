using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //Component utili
    private CharacterController controller;
    //public List<TextMeshProUGUI> textScore;
    
    //Parametri di movimento
    public float movementSpeed;
    public float jumpForce;

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
        Vector3 move = new Vector3(horizontalMovement * movementSpeed, 
                                        jump * jumpForce, 
                                        verticalMovement * movementSpeed);


         if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }


        controller.Move(move);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

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

    private void OnCollisionEnter(Collision collision)
    {
        //controllo per poter risaltare
        if (collision.gameObject.CompareTag("Floor"))
            canJump = true;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        //controllo per non far saltare più
        if (collision.gameObject.CompareTag("Floor"))
            canJump = false;
    }
}