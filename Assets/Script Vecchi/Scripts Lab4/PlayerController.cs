using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Component utili
    private CharacterController ch;
    public List<TextMeshProUGUI> textScore;
    
    //Parametri di movimento
    public float movementSpeed;
    public float jumpForce;
    public float rotationSpeed = 720f;
    
    //Parametri partita
    private int score = 0;
    
    //Flag di controllo
    private bool canJump = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ch = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Input
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump"); //adesso non ci serve

        //definizione del vettore di movimento
        Vector3 movement = new Vector3(horizontalMovement * movementSpeed, 
                                        0f, 
                                        verticalMovement * movementSpeed);

        //definizione del magnitudo e applicazione del movimento tramite SimpleMove
        float magnitude = Mathf.Clamp01(movement.magnitude) * movementSpeed;
        ch.SimpleMove(movement * magnitude);

        //rotazione del giocatore verso dove sta guardando
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation,
                                        toRotation, 
                                        rotationSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collect"))
        {
            other.gameObject.SetActive(false);
            
            //gestione del punteggio (nel ciclo aggiorniamo tutte le scritte)
            score++;
            foreach (var text in textScore)
            {
                text.SetText("Punteggio: " + score);
            }
        }
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
