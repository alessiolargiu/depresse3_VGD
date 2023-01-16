using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path_follower_testing : MonoBehaviour {

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

    public float distanzaMinima = 4f;


    public Transform target;
    
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
        velocity.y += gravity*Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //movimento mouse 
        float mouseX = rotX;
        float mouseY = rotY;

        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);


        transform.Rotate(Vector3.up * mouseX);



        

        // Determine which direction to rotate towards
        Vector3 targetDirection = new Vector3(target.position.x - transform.position.x, 0f, target.position.z - transform.position.z);
        
        UnityEngine.Debug.Log("target direction" + targetDirection);

        // The step size is equal to speed times frame time.
        float singleStep = movementSpeed*10 * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        UnityEngine.Debug.Log("new direction" + targetDirection);


        // Draw a ray pointing at our target in
        Ray targetRay = new Ray(transform.position + new Vector3(0f, 1.2f, 0f) , transform.forward);
        Debug.DrawRay(targetRay.origin, targetRay.direction * 10, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        float distance;
        anim.SetFloat("vertical", verticalMovement);
        anim.SetFloat("position", horizontalMovement);
        Vector3 moveToJump;
        RaycastHit hit;
        if (Physics.Raycast(targetRay, out hit)){
            var hitPoint = hit.point;
            distance = Vector3.Distance(hitPoint, transform.position);
            UnityEngine.Debug.Log("Sono in questa condizione");
            if(distance<distanzaMinima){
            if (hit.collider!= null && hit.collider.gameObject.tag!=target.gameObject.tag){
                if(hit.collider.bounds.size.y<=3.6f){
                            velocity.y=10;
                            controller.Move(velocity * Time.deltaTime);
                    }else ver = 0f;  
                } 
            } else ver = 1f; 


            if (hit.collider!= null && hit.collider.gameObject.tag==target.gameObject.tag){
                distance = Vector3.Distance(hitPoint, transform.position);
                if(distance<distanzaMinima){ver = 0;}  else if(distance<distanzaMinima+1){ver = 0.5f;}  else ver = 1f;
                UnityEngine.Debug.Log("distance " + distance);
            }

            UnityEngine.Debug.Log("sizey" + hit.collider.bounds.size.y);
            
            }
        }





        /*
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
        */
    

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