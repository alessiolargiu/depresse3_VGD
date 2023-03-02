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

    private bool canJump = false;
    public Animator anim;
    private float speedAnim;




    public float hor=0f;
    public float ver=0f;
    public float rotX=0f;
    public float rotY=0f;


    private Ray ray;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update(){

        ray = new Ray(transform.position + new Vector3(0f, 1.5f, 0f) , transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10);

        

        // Input
        float horizontalMovement = hor;
        float verticalMovement = ver;
        //float jump = Input.GetAxis("Jump"); //adesso non ci serve
        bool shift = false;



        //gravita
        Vector3 move;
        velocity.y += gravity*Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //movimento mouse 
        float mouseX = rotX;
        float mouseY = rotY;

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);


        transform.Rotate(Vector3.up * mouseX);

        if(shift){
            anim.SetFloat("vertical", verticalMovement);
        } else anim.SetFloat("vertical", verticalMovement*0.5f);
        anim.SetFloat("position", horizontalMovement);

        //Debug.Log(shift);
        

        if(UnityEngine.Random.value < 0.0005){
            transform.Rotate(0f, 90f, 0f);
        }


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            var hitPoint = hit.point;
            if (hit.collider != null){
                if(hit.collider.gameObject.tag == "Floor"){
                    var distance = Vector3.Distance(hitPoint, transform.position);
                    if(distance<=2.5){
                        if(randomBoolean()){
                            transform.Rotate(0f, -90f, 0f);
                        } else transform.Rotate(0f, 90f, 0f);
                    } else {
                        //ver=0.5f;
                        //rotX = 0.0f;
                        }
                    //UnityEngine.Debug.Log("tocco qualcosa a distanza " + distance);
                }   else {
                    //ver=0.5f;
                    }
                }
            }
    }

    void FixedUpdate(){
    }
    
    private void OnTriggerEnter(Collider other){
    }

    private void OnControllerColliderHit(ControllerColliderHit collision){   
       /* if (collision.gameObject.tag == "Wall"){
            UnityEngine.Debug.Log("tocco qualcosa");
        } UnityEngine.Debug.Log("non tocco niente");
    */}


    private bool randomBoolean (){
        if (UnityEngine.Random.value >= 0.5){
        return true;
    }
        return false;
}


    
}