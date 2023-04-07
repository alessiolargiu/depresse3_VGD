using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowPathController : MonoBehaviour {

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

    public float distanzaMinima = 2f;

    public int ciao;
    public AudioSource self;
    public AudioClip pugnoSound;
    public AudioClip dying;
    public AudioClip hitsound;
    public AudioClip runsound;

    private int maxHealth = 100;
    private int oldHealth;
    public int currentHealth = 100;

    private bool loopCollisione;
    private Collision col;



    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {   
        loopCollisione=false;
        controller = GetComponent<CharacterController>();
        oldHealth=currentHealth;
    }

    private void Update(){

        if(currentHealth==0){
            if(self.isPlaying==false && self.clip!=dying && anim.GetCurrentAnimatorStateInfo(0).IsName("death")==false){
                self.PlayOneShot(dying);
                anim.SetTrigger("dying");
            }
            
        } else{
        // Input
        float horizontalMovement = hor;
        float verticalMovement = ver;
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
        //Controllo degli ostacoli nel cammino dell'NPC
        RaycastHit hit;
        if (Physics.Raycast(targetRay, out hit)){
            var hitPoint = hit.point;
            distance = Vector3.Distance(hitPoint, transform.position);
            

            //Faccio tutti i dovuti controlli quando l'NPC è abbastanza vicino a qualcosa
            if(distance<distanzaMinima){

            //Ostacolo alto, salto
            if (hit.collider!= null && hit.collider.gameObject.tag!=target.gameObject.tag){

                //Mi genero un ray che parte dall'npc
                ray = new Ray(transform.position + new Vector3(0f, 2f, 0f) , transform.forward);
                Debug.DrawRay(ray.origin, ray.direction * 10);

                RaycastHit newRayHit;
                if(Physics.Raycast(ray, out newRayHit)){
                    var newRatHitPoint = newRayHit.point;
                    var newDist = Vector3.Distance(newRatHitPoint, transform.position); 
                    if(newDist<distanzaMinima){
                    }
                } else {
                        velocity.y=10;
                        controller.Move(velocity * Time.deltaTime);
                }


                /*if(hit.collider.bounds.size.y<=3.6f){
                            velocity.y=10;
                            controller.Move(velocity * Time.deltaTime);
                    }else ver = 0f;  */
                } 
            } //else ver = 1f; 

            //L'ostacolo è il target
            if (hit.collider!= null && hit.collider.gameObject.tag==target.gameObject.tag){
                distance = Vector3.Distance(hitPoint, transform.position);
                if(distance<distanzaMinima){

                    if(currentHealth<oldHealth && currentHealth!=0){
                        ver = -1f;
                    } else ver = 0f;
                    
                    if(self.isPlaying==false && anim.GetCurrentAnimatorStateInfo(0).IsName("punch")==false){
                        anim.SetTrigger("punching");
                    }
                    
                    } 
                    else{
                        if(distance>=distanzaMinima+2f){
                            ver=1f;
                            oldHealth=currentHealth;
                        }
                        
                    }

                
                //UnityEngine.Debug.Log("distance " + distance);
            } 


            anim.SetFloat("vertical", verticalMovement);
            anim.SetFloat("position", horizontalMovement);
        
            //UnityEngine.Debug.Log("sizey" + hit.collider.bounds.size.y);
            
            }

            if(loopCollisione){
            Debug.Log("so di star toccando il player");
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("punch") && self.isPlaying==false){
                Debug.Log("so di togliergli vita");
                self.PlayOneShot(pugnoSound, 1f);
                //col.other.GetComponent<FirstPersonController>().TakeDamage(10);
            }
            }
        }
        }


    

    void FixedUpdate(){
    }
    
    private void OnTriggerEnter(Collider other){
    }
    

    private void OnCollisionEnter(Collision collision){  
        
        if(collision.gameObject.CompareTag("player")){
            loopCollisione=true;
            col=collision;
        }
        

    }

    private void OnCollisionExit(Collision collision){
        if(collision.gameObject.CompareTag("player")){
            loopCollisione=false;
        }
    }


    public void DeathDetect(string msg){
        if(msg=="finito"){
            DestroyObject(gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        if(currentHealth>0){
        anim.SetTrigger("gothit");
        currentHealth -= damage;
        self.Stop();
        self.PlayOneShot(hitsound);
        
        }

        if(currentHealth<0){
            anim.SetTrigger("dying");
            currentHealth=0;
        }
    }


    private bool randomBoolean (){
        if (UnityEngine.Random.value >= 0.5){
        return true;
    }
        return false;
    }


    
}