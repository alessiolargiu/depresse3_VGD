using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(AudioSource))]
public class FirstPersonController : MonoBehaviour {
    

    

    public bool isActive = true;
    //Component utili
    private CharacterController controller;
    
    //Parametri di movimento
    public float movementSpeed = 1f;
    public float movementRunSpeed = 1f;
    public float jumpForce = 1;

    private Vector3 velocity;
    public float gravity = -9.81f;
    public float mouseSens = 100f;
    //public Transform cameraTransform;
    private float rotation = 0f;
    public Animator anim;

    //parametri vita del player
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    private int maxHealth = 100;
    private int currentHealth;
    private float maxStamina = 100;
    private float currentStamina;
    private int staminaJump = 10;
    private int staminaAttack = 20;

    //rifermineto per l'inventario
    private Inventory inventory;
    public HUDInventoryWeapon hudInvWeapon;
    public HUDInventoryShield hudInvShield;
    public HUDInventoryPotion hudInvPotion;
    public HUDInventoryHelmet hudInvHelmet;
    public HUDInventoryChest hudInvChest;
    //riferimenti al numero di pozioni in HUD
    public TMP_Text numPozioniSlot1;
    public TMP_Text numPozioniSlot2;
    public TMP_Text numPozioniSlot3;


    //Parametri partita
    private int score = 0;
    
    //Flag di controllo

    public AudioSource running;
    public AudioSource salto;
    public AudioClip altsalto;
    public AudioClip oldsalto;
    public AudioSource walking;
    public AudioSource waaa;
    public AudioSource pickupsound;
    public AudioClip picksoundclip;
    public AudioSource pugno;
    public AudioSource pugnoAir;
    public AudioClip airleft;
    public AudioClip airright;

    private float horizontalMovement;
    private float verticalMovement;

    private bool fastJump;
    private bool slowJump;

    private float smooth;


    public Transform followTransform;

    private float momentum;


    public Transform rightHand;
    public Transform leftHand;

    public GameObject sword;


    public float attackRange = 0.5f;

    public LayerMask enemyLayers;
    public LayerMask enemyGiganteLayers;
    public LayerMask cittadinoLayers;

    private Collider prevCol;

    private Transform currentEnemy;

    private bool altPunching = false;

    private bool outOfReach = true;
    private Collider lastEnemy;

    public Transform legL;
    public Transform legR;

    private int noWeaponCycle = 0;


    public GameObject bangFX;

    private Vector3 move;


    //userflag
    public bool usingController;
    public bool infiniteStamina = false;

    //inputs
    bool jump;
    bool inAtck;
    bool shift;
    float mouseX;
    float mouseY;
    float rightHorizontal;
    float rightVertical;
    bool punchingKey;
    bool swordKey;
    bool potionKey;
    bool crouchKey;

    private float lastH;
    private float lastV;

    public CinemachineVirtualCamera virtualCam;



    public Transform playerModel;

    void Start(){   
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //imposto la vita iniziale
        currentHealth = 80;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        smooth = 0.5f;
        fastJump=false;
        slowJump=false;
        bangFX.SetActive(false);
        sword.SetActive(false);
        verticalMovement=1;
    }

    private void Awake(){
        inventory = new Inventory();
        inventory.player = this;
        //inserisco gli elementi di base dell'inventario

        //imposto l'HUD per l'inventario
        hudInvWeapon.SetInventory(inventory);
        hudInvShield.SetInventory(inventory);
        hudInvPotion.SetInventory(inventory);
        hudInvHelmet.SetInventory(inventory);
        hudInvChest.SetInventory(inventory);
        

        verticalMovement=1;
    }

    private void Update(){
        if(isActive){
            InputGet();
            AdjustCamera();
            CrouchCheck();  
            AttackType();
            UsePotion();
            AudioEnvCheck();
            EnemyRangeCheck();
            Debug.Log("Valore di inAtck " + inAtck);
            if(inAtck){
                if(anim.GetBool("inCombat")==false){
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance=2;
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset = new(0, -1.80f, 0);
                    anim.SetBool("inCombat", inAtck);
                }
                MovementAttacking();
            } else {
                if(anim.GetBool("inCombat")==true){
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance=3;
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset = new(0, -2f, 0);

                    anim.SetBool("inCombat", inAtck);
                }
                anim.SetBool("inCombat", inAtck);
                Movement();
            }
            Jumping();

            if (!infiniteStamina)
            {
                /*if (!shift)
                {
                    if (currentStamina < maxStamina)
                    {
                        currentStamina += 6 * Time.deltaTime;
                    }
                    if (currentStamina > maxStamina)
                    {
                        currentStamina = maxStamina;
                    }

                    staminaBar.SetStamina(currentStamina);
                }*/

                currentStamina=maxStamina;
                
            }

        }

    }

    public int DamageCalculation(int initialDamage)
    { 
        
        int damage = 0;
        //seleziono i componenti dell'armatura correntemente equipaggiati
        HelmetEquip currentHelmet = new HelmetEquip();
        ChestEquip currentChest = new ChestEquip();
        foreach (HelmetEquip helmet in inventory.GetHelmets())
        {
            if (helmet.gameObject.activeSelf)
            {
                currentHelmet = helmet;
            }
        }
        foreach (ChestEquip chest in inventory.GetChests())
        {
        if (chest.gameObject.activeSelf)
            {
                currentChest = chest;
            }
        }

        if (currentHelmet.armorValue >= currentChest.armorValue)
        {
            int intermediateDamage = (int)(initialDamage - initialDamage * currentHelmet.armorValue);
            damage = (int)(intermediateDamage - intermediateDamage * currentChest.armorValue);
        }
        else
        {
            int intermediateDamage = (int)(initialDamage - initialDamage * currentChest.armorValue);
            damage = (int)(intermediateDamage - intermediateDamage * currentHelmet.armorValue);
        }

        return damage;
    }

    public void TakeDamage(int damage, Transform enemyCurrent, int whoIs){ 
        if (isActive){
            switch(whoIs){
                case 1:
                    followTransform.GetComponent<PlayerTarget>().SetAttackMode(true, enemyCurrent);
                    enemyCurrent.GetComponent<Maranzus>().setActiveEnemy(true);
                    currentHealth -= DamageCalculation(damage);
                    anim.SetTrigger("gothit");
                    healthBar.SetHealth(currentHealth);
                    break;
                case 2:
                    currentHealth -= DamageCalculation(damage);
                    anim.SetTrigger("gothit");
                    healthBar.SetHealth(currentHealth);
                    break;
                case 3:
                    currentHealth -= DamageCalculation(damage);
                    anim.SetTrigger("gothit");
                    healthBar.SetHealth(currentHealth);
                    break;
            }

        }
    }

    public Inventory GetInventory(){
        return inventory;
    }

    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Cutscene")){
            isActive=false;
            StartCoroutine(other.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {}));
        }

        if (other.gameObject.CompareTag("pickup")){
            pickupsound.enabled=true;
            pickupsound.PlayOneShot(picksoundclip, 0.7F);

        } 

    }

    private void AudioEnvCheck(){
        if(controller.isGrounded==false){
            running.enabled=false;
            walking.enabled=false;
        }  else {
            momentum=0;
        }
    }

    private void EnemyRangeCheck(){
        if(verticalMovement!=0){
            if(lastEnemy!=null){
            if(lastEnemy.GetComponent<Maranzus>().OutOfReach()){
                lastEnemy=null;
                //lastEnemy.GetComponent<Maranzus>().setActiveEnemy(false);
                noWeaponCycle=0;
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(false, null);
            }
        }
        }
    }
    
    private void InputGet(){
        horizontalMovement = Input.GetAxis("Horizontal");
        if(anim.GetBool("crouching")==false){
            verticalMovement = Input.GetAxis("Vertical");
        } else verticalMovement = 0;

        mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        rightHorizontal = Input.GetAxis("RightStickHorizontal");
        rightVertical = Input.GetAxis("RightStickVertical");

        if(usingController){
            jump = Input.GetKeyDown(KeyCode.JoystickButton0);
            inAtck = Input.GetKey(KeyCode.JoystickButton6);
            shift = Input.GetKey(KeyCode.JoystickButton1);
            punchingKey = Input.GetKeyUp(KeyCode.JoystickButton2);
            swordKey = Input.GetKeyUp(KeyCode.JoystickButton3);
            crouchKey = Input.GetKey(KeyCode.JoystickButton10);
        } else {
            shift = Input.GetKey(KeyCode.LeftShift);
            jump = Input.GetKeyDown(KeyCode.Space); 
            punchingKey = Input.GetMouseButton(0);
            inAtck = Input.GetMouseButton(1);
            //inAtck = Input.GetMouseButtonDown(2);
            //punchingKey = Input.GetKeyUp(KeyCode.F);
            crouchKey = Input.GetKey(KeyCode.C);
            swordKey = Input.GetKeyUp(KeyCode.G);
            potionKey = Input.GetKeyUp(KeyCode.Q);
        }

    }

    //Funzione che permette di curarsi usando una pozione
    private void UsePotion()
    {
        if(potionKey && controller.isGrounded)
        {
            if(inventory.GetPotions().Count > 0)
            {
                foreach(PotionEquip potion in inventory.GetPotions())
                {
                    if(potion.isEquiped && potion.potionNumber > 0 && currentHealth < 100)
                    {
                        currentHealth += potion.cureValue;
                        if(currentHealth > 100) { 
                            currentHealth = 100; 
                        }
                        healthBar.SetHealth(currentHealth);
                        potion.potionNumber--;
                        switch (potion.index)
                        {
                            case 1:
                                numPozioniSlot1.text = potion.potionNumber.ToString();
                                break;
                            case 2:
                                numPozioniSlot2.text = potion.potionNumber.ToString();
                                break;
                            case 3:
                                numPozioniSlot3.text = potion.potionNumber.ToString();
                                break;
                        }
                    }              
                }
            }
        }
    }

    private void CrouchCheck(){
        if(Input.anyKeyDown || Input.GetAxis("Horizontal")>0 || Input.GetAxis("Vertical")>0){
            if (crouchKey){
                if(anim.GetBool("crouching")){
                    anim.SetBool("crouching", false);
                } else anim.SetBool("crouching", true);
            } else {
                anim.SetBool("crouching", false);
            }
        }
    }

    private void Movement(){

        Debug.Log("sto in sta cosa");
        float asinHor = 0;

        //QUESTA SEZIONE DI CODICE E' SCRITTA CON IL CULO, MA NON SI CAMBIA ASSOLUTAMENTE
        //CI HO MESSO 3 GIORNI A FARLA FUNZIONARE E ORA CHE FUNZIONA NON SI TOCCA PER DUE MOTIVI:
        // - HO PAURA DI ROMPERE QUALCOSA
        // - VA LASCIATA COSI' A TESTAMENTO DI QUANTO MI SONO TRITURATO I COGLIONI
        // alessio 30/04/23
        if ((horizontalMovement != 0.0f || verticalMovement != 0.0f)) {
            asinHor =   followTransform.eulerAngles.y + Mathf.Atan2(horizontalMovement, verticalMovement) * Mathf.Rad2Deg;
            lastH=horizontalMovement;
            lastV=verticalMovement;
        }
        /*
        if(followTransform.GetComponent<PlayerTarget>().GetAttackMode()){
            Transform prova = followTransform.GetComponent<PlayerTarget>().GetTarget();
            asinHor =  Mathf.Atan2(horizontalMovement, verticalMovement) * Mathf.Rad2Deg;
        }*/ 


        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, asinHor ,  transform.eulerAngles.z);
        float newEuler = Mathf.Round(followTransform.eulerAngles.y * 100f) / 100f;
        float newOld = Mathf.Round(transform.eulerAngles.y * 100f) / 100f;
        float model = Mathf.Round(playerModel.eulerAngles.y * 100f) / 100f;
        if(newEuler==newOld && (verticalMovement!=0 || horizontalMovement!=0)){
            playerModel.rotation = Quaternion.Euler(eulerRotation);
        }
        Debug.Log("MOVEMENTS Euler " + newEuler + " old " + newOld + " equals? " + (newEuler==newOld));


        //followTransform.rotation = Quaternion.Euler(eulerRotation);
        

        if(horizontalMovement!=0 || verticalMovement!=0){
            transform.Rotate(Vector3.up * rightHorizontal);
            transform.Rotate(Vector3.up * mouseX);
        }


        if((shift) && currentStamina > 0 && (verticalMovement!=0f || horizontalMovement!=0f) && controller.isGrounded || momentum!=0f){

            move = transform.right * horizontalMovement + transform.forward * verticalMovement;
            move *= movementRunSpeed;
            if(verticalMovement>0){
                anim.SetFloat("walking", verticalMovement,  1f, Time.deltaTime * 10f);
            } else anim.SetFloat("walking", verticalMovement*-1, 1f, Time.deltaTime * 10f );
            if(horizontalMovement>0){
                anim.SetFloat("strafing", horizontalMovement, 1f, Time.deltaTime * 10f );
            } else anim.SetFloat("strafing", horizontalMovement*-1,  1f, Time.deltaTime * 10f );
        
        } else {
            move = transform.right * horizontalMovement + transform.forward * verticalMovement;
            move *= movementSpeed;
            if(verticalMovement>0){
                anim.SetFloat("walking", verticalMovement * 0.5f,  1f, Time.deltaTime * 10f);
            } else anim.SetFloat("walking", verticalMovement*-1 *0.5f, 1f, Time.deltaTime * 10f );
            if(horizontalMovement>0){
                anim.SetFloat("strafing", horizontalMovement * 0.5f, 1f, Time.deltaTime * 10f );
            } else anim.SetFloat("strafing", horizontalMovement*-1 * 0.5f,  1f, Time.deltaTime * 10f );
        }


        
        
        controller.Move(move * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    private void MovementAttacking(){
        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, followTransform.eulerAngles.y ,  transform.eulerAngles.z);
        playerModel.rotation = Quaternion.Euler(eulerRotation);

        move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        move *= movementSpeed * 0.5f;

        anim.SetFloat("combatWalk", verticalMovement * 0.5f,  1f, Time.deltaTime * 10f);
        anim.SetFloat("combatStrafe", horizontalMovement * 0.5f,  1f, Time.deltaTime * 10f);
        
        transform.Rotate(Vector3.up * horizontalMovement*0.2f);
        transform.Rotate(Vector3.up * mouseX*0.2f);

        controller.Move(move * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jumping(){
        if(controller.isGrounded){
            anim.SetFloat("jumping", 0);
            if(jump /*&& verticalMovement==1*/ && currentStamina >= staminaJump  && shift==true){
                shift=true;
                slowJump=false;
                fastJump=true;
                momentum=1f;
                
                running.enabled=false;
                walking.enabled=false;
                float bobbo = UnityEngine.Random.Range(0, 2);
                if(bobbo==0){
                    salto.clip = altsalto;
                } else salto.clip = oldsalto;
                salto.PlayOneShot(salto.clip, 1f);
                velocity.y=jumpForce;
                controller.Move(velocity * Time.deltaTime);
                controller.Move(move * Time.deltaTime);
                anim.SetTrigger("jumpTrigger");
                if (!infiniteStamina)
                {
                    currentStamina -= staminaJump;
                    staminaBar.SetStamina(currentStamina);
                }


            } else if(jump /*&& verticalMovement!=-1 && !(horizontalMovement!=0 && verticalMovement==0) */ && currentStamina >= staminaJump ){
                shift=true;
                fastJump=false;
                slowJump=true;
                momentum = 0f;
                running.enabled=false;
                walking.enabled=false;
                float bobbo = UnityEngine.Random.Range(0, 2);
                if(bobbo==0){
                    salto.clip = altsalto;
                } else salto.clip = oldsalto;
                salto.PlayOneShot(salto.clip, 1f);
                velocity.y=jumpForce*0.8f;
                controller.Move(velocity * Time.deltaTime);
                //controller.Move(move * Time.deltaTime);
                anim.SetTrigger("jumpTrigger");
                if (!infiniteStamina)
                {
                    currentStamina -= staminaJump;
                    staminaBar.SetStamina(currentStamina);
                }

            } 

        } 
        else  {
            anim.SetFloat("jumping", 1, 1f, Time.deltaTime * 10f);
            velocity.y += gravity*Time.deltaTime;
        }
    }

    private void AttackType(){
        if(((punchingKey) && currentStamina >= staminaAttack && pugnoAir.isPlaying==false) && controller.isGrounded && (anim.GetCurrentAnimatorStateInfo(0).IsName("kick")==false)){
            switch(noWeaponCycle){
                case 0:
                    StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0f));
                    StartCoroutine(Attack(leftHand, 20, 0.1f));
                    anim.SetBool("altPunching", true);
                    anim.SetTrigger("punching");
                    if (!infiniteStamina){
                        currentStamina -= staminaAttack;
                        staminaBar.SetStamina(currentStamina);
                    }
                    noWeaponCycle++;
                    break;
                
                case 1:
                    StartCoroutine(OnTimeSound(pugnoAir, airright, 1f, 0f));
                    StartCoroutine(Attack(rightHand, 20, 0.1f));
                    anim.SetBool("altPunching", false);
                    anim.SetTrigger("punching");
                    if (!infiniteStamina)
                    {
                        currentStamina -= staminaAttack;
                        staminaBar.SetStamina(currentStamina);
                    }
                    noWeaponCycle++;
                    break;

                case 2:
                    StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0.5f));
                    int rand = UnityEngine.Random.Range(0, 2);
                    if(rand==1){
                        anim.SetBool("altPunching", false);
                        StartCoroutine(Attack(legR, 40, 0.5f));
                    } else {
                        anim.SetBool("altPunching", true);
                        StartCoroutine(Attack(legL, 40, 0.5f));
                    }
                    anim.SetTrigger("kicking");
                    if (!infiniteStamina)
                    {
                        currentStamina -= staminaAttack;
                        staminaBar.SetStamina(currentStamina);
                    }
                    noWeaponCycle =0;
                    break;
            
            }
            
        }

        if(((swordKey) && pugnoAir.isPlaying==false) && controller.isGrounded && (anim.GetCurrentAnimatorStateInfo(0).IsName("swording")==false)){
            sword.SetActive(true);
            StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0.5f));
            StartCoroutine(Attack(sword.transform, 60, 1f));
            anim.SetTrigger("swording");
        }

    }

    private void AdjustCamera(){

        //NESSUNO SI AZZARDI A TOCCARE NESSUNA RIGA DI QUESTO METODO
        //VI PRENDO A COLPI
        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, followTransform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 oldRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);        
        float newEuler = Mathf.Round(followTransform.eulerAngles.y * 100f) / 100f;
        float newOld = Mathf.Round(transform.eulerAngles.y * 100f) / 100f;

        Debug.Log("ADJUSTCAMERA Euler " + newEuler + " old " + newOld + " equals? " + (newEuler==newOld));

        if(newEuler!=newOld && (verticalMovement!=0 || horizontalMovement!=0 || punchingKey)){

            Debug.Log("sto qua");
            float test = followTransform.eulerAngles.y;
            float asinHor = Mathf.Atan2(horizontalMovement, verticalMovement) * Mathf.Rad2Deg;
            Vector3 moveRot = new Vector3(transform.eulerAngles.x, test,  transform.eulerAngles.z);

            transform.rotation = Quaternion.Euler(eulerRotation);
            playerModel.rotation = Quaternion.Euler(moveRot);

        }

        if(followTransform.GetComponent<PlayerTarget>().GetAttackMode()==true){
            //transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }


    IEnumerator Attack(Transform attackPoint, int dmg, float time){
        verticalMovement=0;
        yield return new WaitForSeconds(time);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        Collider[] hitGigante = Physics.OverlapSphere(attackPoint.position, attackRange*3, enemyGiganteLayers);
        Collider[] hitCittadino = Physics.OverlapSphere(attackPoint.position, attackRange, cittadinoLayers);


        foreach(Collider enemy in hitEnemies){
            float singleStep = 1 * Time.deltaTime;

            lastEnemy=enemy;
            
            float health = enemy.GetComponent<Maranzus>().TakeDamage(dmg);
            
            if(health>0){
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(true, enemy.transform);
                enemy.GetComponent<Maranzus>().setActiveEnemy(true);
            } else if(health<=0){
                lastEnemy=null;
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(false, null);
                enemy.GetComponent<Maranzus>().setActiveEnemy(false);
            }
            
            pugno.PlayOneShot(pugno.clip, 1f);

            bangFX.SetActive(true);
            bangFX.transform.position=attackPoint.position;
            yield return new WaitForSeconds(4f);
            bangFX.SetActive(false);

        }

        foreach(Collider gigante in hitGigante){
            float singleStep = 1 * Time.deltaTime;
            float health = gigante.GetComponent<MaranzusGigante>().TakeDamage(dmg);

            pugno.PlayOneShot(pugno.clip, 1f);

            bangFX.SetActive(true);
            yield return new WaitForSeconds(4f);
            bangFX.SetActive(false);

        }

        foreach(Collider cittadino in hitCittadino){
            cittadino.GetComponent<Cittadino>().StopIt();
        }

        sword.SetActive(false);
    }

    IEnumerator OnTimeSound(AudioSource src, AudioClip clp, float volume, float delay){
        yield return new WaitForSeconds(delay);
        src.PlayOneShot(clp, volume);
    }

    public void SetActiveInternal(bool what){
        isActive=what;
    }

    public bool GetActiveInternal(){
        return isActive;
    }
    
}