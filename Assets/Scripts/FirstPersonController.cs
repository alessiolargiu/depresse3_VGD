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

    public List<WeaponEquip> weapons;
    public List<ShieldEquip> shields;
    public List<PotionEquip> potions;
    public List<HelmetEquip> helmets;
    public List<ChestEquip> chests;

    private List<int> availableWeapons = new List<int>();
    private List<int> availableShields = new List<int>();
    private List<int> availableChests = new List<int>();
    private List<int> availableHelmets = new List<int>();
    private List<int> availablePotions = new List<int>();

    //riferimenti al numero di pozioni in HUD
    public TMP_Text numPozioniSlot1;
    public TMP_Text numPozioniSlot2;
    public TMP_Text numPozioniSlot3;

    private GameManager gameManager;

    //Parametri partita
    private int score = 0;
    
    //Flag di controllo

    public AudioSource running;
    public AudioSource salto;
    public AudioClip altsalto;
    public AudioClip oldsalto;
    public AudioSource walking;
    public AudioClip [] hitSounds;
    public AudioSource pickupsound;
    public AudioClip picksoundclip;
    public AudioSource pugno;
    public AudioSource hitSource;
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


    //public float attackRange;

    public LayerMask enemyLayers;
    public LayerMask enemyGiganteLayers;
    public LayerMask cittadinoLayers;
    public LayerMask acqua;

    private Collider prevCol;

    private Transform currentEnemy;

    private bool altPunching = false;

    private bool outOfReach = true;
    private Collider lastEnemy;

    public Transform legL;
    public Transform legR;

    private int noWeaponCycle;


    public GameObject bangFX;

    private Vector3 move;

    private bool moveToEnemyYes;


    //userflag
    public bool usingController;

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
    bool rotateKey;

    private float lastH;
    private float lastV;

    public CinemachineVirtualCamera virtualCam;



    public Transform playerModel;

    bool isWalk;

    private static bool attackFinished;


    private Transform curposdebug;

    private Transform oldPosShield;

    private bool shieldAvailable;

    private bool dontMove;


    void Start(){   
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //imposto la vita iniziale
        currentHealth = 100;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetStamina(currentStamina);
        smooth = 0.5f;
        fastJump=false;
        slowJump=false;
        attackFinished=true;
        bangFX.SetActive(false);
        verticalMovement=1;
    }

    private void Awake(){
        dontMove=false;
        inventory = new Inventory();
        inventory.player = this;

        foreach(HelmetEquip h in helmets)
        {
            inventory.AddHelmet(h);
        }
        foreach (ChestEquip c in chests)
        {
            inventory.AddChest(c);
        }
        foreach (WeaponEquip w in weapons)
        {
            inventory.AddWeapon(w);
        }
        foreach (ShieldEquip s in shields)
        {
            inventory.AddShield(s);
        }
        foreach (PotionEquip p in potions)
        {
            inventory.AddPotion(p);
            Debug.Log("Numero di pozioni: " + p.potionNumber);
        }

        //imposto l'HUD per l'inventario
        hudInvWeapon.SetInventory(inventory, availableWeapons);
        hudInvShield.SetInventory(inventory, availableShields);
        hudInvPotion.SetInventory(inventory, availablePotions);
        hudInvHelmet.SetInventory(inventory, availableHelmets);
        hudInvChest.SetInventory(inventory, availableChests);

        gameManager = FindObjectOfType<GameManager>();

        running.enabled=false;
        walking.enabled=false;
        shieldAvailable=true;
        verticalMovement=1;
    }

    private void Update(){
        if(isActive){
            InputGet();
            AdjustCamera();
            CrouchCheck();  
            UsePotion();
            AudioEnvCheck();
            EnemyRangeCheck();
            Debug.Log("Valore di inAtck " + inAtck);

            if(inAtck){
                if(anim.GetBool("inCombat")==false){
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance=2;
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset = new(0, -1.80f, 0);
                    virtualCam.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = new(0.2f, 0.5f, 0);
                    virtualCam.m_Lens.FieldOfView = 60;
                    anim.SetBool("inCombat", inAtck);
                }
                MovementAttacking();
            } else {
                if(anim.GetBool("inCombat")==true){
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance=3;
                    virtualCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset = new(0, -2f, 0);
                    virtualCam.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = new(0f, 0.5f, 0);

                    anim.SetBool("inCombat", inAtck);
                }
                anim.SetBool("inCombat", inAtck);
                Movement();
            }
            
            Jumping();
            

            
            if(!shift)
            {
                if (currentStamina < maxStamina)
                {
                    currentStamina +=  4 * Time.deltaTime;
                }
                if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }

                staminaBar.SetStamina(currentStamina);
            }

                
            Debug.Log("atck" + attackFinished);
            AttackType();
            MoveToEnemy();

        }
        Transform prova = GetNearestEnemy();
        Debug.Log("nearest enemy " + prova.name);

        
        
            
        
    }

    public int DamageCalculation(float initialDamage)
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

    public void TakeDamage(float damage, Transform enemyCurrent, int whoIs){ 

        ShieldEquip currentShield = new ShieldEquip();
        float valoreProtezione = 1;
        
        foreach (ShieldEquip shield in inventory.GetShields()){
                if (shield.gameObject.activeSelf)
                {
                    currentShield = shield;
                }
            }

        if(inAtck && currentShield.shieldValue!=0 && shieldAvailable){

            valoreProtezione=currentShield.shieldValue;
        } else if(currentShield.shieldValue==0 && inAtck && shieldAvailable){
            valoreProtezione=0.9f;
        } 

        AnimatorClipInfo[] animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
        String current_animation = animatorinfo[0].clip.name;

        if(current_animation!="incazzato nero"){
            valoreProtezione=1;
        }

        

        Debug.Log("Sto difendendo "  + current_animation);
        Debug.Log("Sto difendendo, il valore è " + valoreProtezione);
        Debug.Log("Sto difendendo, il valore del danno è " + damage*valoreProtezione);

        
        int ranInt = UnityEngine.Random.Range(0, hitSounds.Length);
        AudioClip ranClip = hitSounds[ranInt];
        if(hitSource.isPlaying==false){
            hitSource.PlayOneShot(ranClip, 1);
        }
        
        if (isActive && !gameManager.vitaInfinita){
            switch(whoIs){
                case 1:
                    followTransform.GetComponent<PlayerTarget>().SetAttackMode(true, enemyCurrent);
                    enemyCurrent.GetComponent<Maranzus>().setActiveEnemy(true);
                    currentHealth -= DamageCalculation(damage*valoreProtezione);
                    anim.SetTrigger("gothit");
                    healthBar.SetHealth(currentHealth);
                    break;
                case 2:
                    currentHealth -= DamageCalculation(damage*valoreProtezione);
                    anim.SetTrigger("gothit");
                    healthBar.SetHealth(currentHealth);
                    break;
                case 3:
                    currentHealth -= DamageCalculation(damage*valoreProtezione);
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

        if(other.gameObject.layer == acqua){
            Debug.Log("sto toccando acqua");
            TakeDamage(100, transform, 0);
        }

    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.layer == acqua){
            Debug.Log("sto toccando acqua");
            TakeDamage(100, transform, 0);
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
                followTransform.GetComponent<PlayerTarget>().SetAttackMode(false, null);
            }
        }
        }
    }
    
    private void InputGet(){
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");


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
            crouchKey = Input.GetKeyUp(KeyCode.JoystickButton10);
        } else {
            shift = Input.GetKey(KeyCode.LeftShift);
            jump = Input.GetKeyDown(KeyCode.Space); 
            punchingKey = Input.GetMouseButton(0);
            inAtck = Input.GetMouseButton(1);
            rotateKey = Input.GetMouseButton(2);
            //inAtck = Input.GetMouseButtonDown(2);
            //punchingKey = Input.GetKeyUp(KeyCode.F);
            crouchKey = Input.GetKeyUp(KeyCode.C);
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
                    if(potion.isEquiped && currentHealth < 100)
                    {
                        if(!gameManager.fullEquip)
                        {
                            if(potion.potionNumber <= 0)
                            {
                                return;
                            }
                            potion.potionNumber--;

                        }
                        //DA AGGIUNGERE L'ANIMAZIONE DELLA POZIONE
                        anim.SetTrigger("potion");
                        currentHealth += potion.cureValue;
                        if(currentHealth > 100) { 
                            currentHealth = 100; 
                        }
                        healthBar.SetHealth(currentHealth);
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
        if (crouchKey){
                if(anim.GetBool("crouching")){
                    anim.SetBool("crouching", false);
                } else anim.SetBool("crouching", true);
        }

        if(jump || inAtck || shift || swordKey){
            anim.SetBool("crouching", false);
        }

    }

    private void Movement(){

        float asinHor = 0;



        //QUESTA SEZIONE DI CODICE E' SCRITTA CON IL CULO, MA NON SI CAMBIA ASSOLUTAMENTE
        //CI HO MESSO 3 GIORNI A FARLA FUNZIONARE E ORA CHE FUNZIONA NON SI TOCCA PER DUE MOTIVI:
        // - HO PAURA DI ROMPERE QUALCOSA
        // - VA LASCIATA COSI' A TESTAMENTO DI QUANTO MI SONO TRITURATO I COGLIONI
        // alessio 30/04/23

        if(rotateKey){
            horizontalMovement=0;
            if(verticalMovement<0){
                verticalMovement=0;
            }
        }
        
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
        if(newEuler==newOld && (verticalMovement!=0 || horizontalMovement!=0) && dontMove==false){
            playerModel.rotation = Quaternion.Euler(eulerRotation);
        }
        Debug.Log("MOVEMENTS Euler " + newEuler + " old " + newOld + " equals? " + (newEuler==newOld));


        //followTransform.rotation = Quaternion.Euler(eulerRotation);
        

        if(horizontalMovement!=0 || verticalMovement!=0){
            transform.Rotate(Vector3.up * rightHorizontal);
            transform.Rotate(Vector3.up * mouseX);
        }


        if((shift) && currentStamina > 0 && (verticalMovement!=0f || horizontalMovement!=0f) && controller.isGrounded || momentum!=0f){

            if (!gameManager.staminaInfinita)
            {
                currentStamina -= 2 * Time.deltaTime;
                staminaBar.SetStamina(currentStamina);
            }
            move = transform.right * horizontalMovement + transform.forward * verticalMovement;
            move *= movementRunSpeed;

            if(virtualCam.m_Lens.FieldOfView<70){
                virtualCam.m_Lens.FieldOfView += 10 * Time.deltaTime;
            }
            
            if(verticalMovement>0){
                anim.SetFloat("walking", verticalMovement,  1f, Time.deltaTime * 10f);
            } else anim.SetFloat("walking", verticalMovement*-1, 1f, Time.deltaTime * 10f );

            if(horizontalMovement>0){
                anim.SetFloat("strafing", horizontalMovement, 1f, Time.deltaTime * 10f );
            } else anim.SetFloat("strafing", horizontalMovement*-1,  1f, Time.deltaTime * 10f );
        
        } else {
            if(virtualCam.m_Lens.FieldOfView>60){
                virtualCam.m_Lens.FieldOfView -= 10 * Time.deltaTime;
            }
            move = transform.right * horizontalMovement + transform.forward * verticalMovement;
            move *= movementSpeed;
            if(verticalMovement>0){
                anim.SetFloat("walking", verticalMovement * 0.5f,  1f, Time.deltaTime * 10f);
            } else anim.SetFloat("walking", verticalMovement*-1 *0.5f, 1f, Time.deltaTime * 10f );
            if(horizontalMovement>0){
                anim.SetFloat("strafing", horizontalMovement * 0.5f, 1f, Time.deltaTime * 10f );
            } else anim.SetFloat("strafing", horizontalMovement*-1 * 0.5f,  1f, Time.deltaTime * 10f );
        }

        if(verticalMovement!=0 || horizontalMovement!=0){
            
            if(shift){
                walking.enabled=false;
                running.enabled=true;
            } else {
                walking.enabled=true;
                running.enabled=false;
            }
        } else {
            walking.enabled=false;
            running.enabled=false;
        }
            
        
        if(dontMove==false){
            controller.Move(move * Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);
    }

    private void MovementAttacking(){
        
        
        ShieldEquip currentShield = new ShieldEquip();
        GameObject shieldGO;
        foreach (ShieldEquip shield in inventory.GetShields()){
                if (shield.gameObject.activeSelf){
                    currentShield = shield;
                }
            }
            
            if(currentShield==null){
                shieldGO=null;
            }
        else shieldGO = GameObject.Find("Equip Container Shield/" + currentShield.name );  

        //shieldGO.transform.Rotate(10,10,10);


        if(verticalMovement!=0 || horizontalMovement!=0){
            walking.enabled=true;
            running.enabled=false;
        } else {
            walking.enabled=false;
            running.enabled=false;
        }


        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, followTransform.eulerAngles.y ,  transform.eulerAngles.z);
        playerModel.rotation = Quaternion.Euler(eulerRotation);

        move = transform.right * horizontalMovement + transform.forward * verticalMovement;
        move *= movementSpeed * 0.5f;

        anim.SetFloat("combatWalk", verticalMovement * 0.5f,  1f, Time.deltaTime * 10f);
        anim.SetFloat("combatStrafe", horizontalMovement * 0.5f,  1f, Time.deltaTime * 10f);
        
        transform.Rotate(Vector3.up * horizontalMovement*0.2f);
        transform.Rotate(Vector3.up * mouseX*0.2f);

        if(dontMove==false){
            controller.Move(move * Time.deltaTime);
        }
        
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
                if (!gameManager.staminaInfinita)
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
                if (!gameManager.staminaInfinita)
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


        
        
        WeaponEquip currentWeapon = new WeaponEquip();
        ShieldEquip currentShield = new ShieldEquip();

        GameObject shieldGO;
        
        if(((((punchingKey) && currentStamina >= staminaAttack && pugnoAir.isPlaying==false) && controller.isGrounded && attackFinished) && anim.GetBool("crouching")) && anim.GetCurrentAnimatorStateInfo(0).IsName("strangling")==false){
            Transform enemyClose = GetNearestEnemy();
            float cacca = Vector3.Distance(enemyClose.position, transform.position);
            Debug.Log("sto impazzendo per la distanza " + cacca);
            if(enemyClose.position!=transform.position && cacca<=2f){
                dontMove=true;
                if(enemyClose.GetComponent<Maranzus>().GetAwareness()==false){
                    anim.SetTrigger("strangling");
                    enemyClose.GetComponent<Maranzus>().BeingStrangled();
                }
                
            }
            

        } else if(((punchingKey) && currentStamina >= staminaAttack && pugnoAir.isPlaying==false) && controller.isGrounded && attackFinished  && anim.GetCurrentAnimatorStateInfo(0).IsName("strangling")==false){


            foreach (WeaponEquip weapon in inventory.GetWeapons())
            { 
                if (weapon.gameObject.activeSelf)
                {
                    currentWeapon = weapon;
                }
            }

            foreach (ShieldEquip shield in inventory.GetShields())
            {
                if (shield.gameObject.activeSelf)
                {
                    currentShield = shield;
                }
            }
            
            if(currentShield==null){
                shieldGO=null;
            }
            else shieldGO = GameObject.Find("Equip Container Shield/" + currentShield.name );  


            AnimatorClipInfo[] animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
            string current_animation = animatorinfo[0].clip.name;
            Debug.Log("animazione corrente " + current_animation);

            if(currentWeapon==null) {
                moveToEnemyYes=true;
                switch(noWeaponCycle){
                    case 0:
                    StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0f));
                    StartCoroutine(Attack(leftHand, 5, 0.1f, 0.5f, 0.2f, shieldGO, false));
                    anim.SetBool("altPunching", true);
                    anim.SetTrigger("punching");
                    noWeaponCycle++;
                    break;
                    
                    case 1:
                    StartCoroutine(OnTimeSound(pugnoAir, airright, 1f, 0f));
                    StartCoroutine(Attack(rightHand, 5, 0.1f, 0.5f, 0.2f, shieldGO, false));
                    anim.SetBool("altPunching", false);
                    anim.SetTrigger("punching");
                    noWeaponCycle++;
                    break;

                    case 2:
                    StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0.5f));
                    int rand = UnityEngine.Random.Range(0, 2);
                    if(rand==1){
                        anim.SetBool("altPunching", false);
                        StartCoroutine(Attack(legR, 10, 0.5f, 0.5f, 0.8f, shieldGO, true));
                    } else {
                        anim.SetBool("altPunching", true);
                        StartCoroutine(Attack(legL, 10, 0.5f, 0.5f, 0.8f, shieldGO, true));
                    }
                    anim.SetTrigger("kicking");
                    noWeaponCycle =0;
                    break;

                }
                
                if (!gameManager.staminaInfinita){
                    currentStamina -= staminaAttack;
                    staminaBar.SetStamina(currentStamina);
                }
                
            } else if(currentWeapon!=null){
                Transform sword = GameObject.Find("Equip Container Weapon/" + currentWeapon.name + "/Collider").transform;    
                StartCoroutine(OnTimeSound(pugnoAir, airleft, 1f, 0.5f));

                Debug.Log(currentWeapon.name);

                if(currentWeapon.name=="lancia_in_player"){
                    anim.SetTrigger("spear");
                    StartCoroutine(Attack(sword, currentWeapon.damage, 1f, currentWeapon.innerRange, currentWeapon.reloadTime, shieldGO, true));
                } else if(currentWeapon.name!="lancia_in_player"){
                    moveToEnemyYes=true;

                    switch(noWeaponCycle){
                        case 0:
                        anim.SetBool("altPunching", true);
                        anim.SetTrigger("swording");
                        StartCoroutine(Attack(sword, currentWeapon.damage, 1f, currentWeapon.innerRange, currentWeapon.reloadTime, shieldGO, false));
                        noWeaponCycle++;
                        break;


                        case 1:
                        anim.SetBool("altPunching", false);
                        anim.SetTrigger("swording");
                        StartCoroutine(Attack(sword, currentWeapon.damage, 1f, currentWeapon.innerRange, currentWeapon.reloadTime, shieldGO, false));
                        noWeaponCycle++;
                        break;

                        case 2:
                        anim.SetTrigger("swordpesante");
                        StartCoroutine(Attack(sword, currentWeapon.damage, 1f, currentWeapon.innerRange, currentWeapon.reloadTime, shieldGO, true));
                        noWeaponCycle=0;
                        break;


                    }
                    
                }
                
                
                
                    
                if (!gameManager.staminaInfinita){
                    currentStamina -= currentWeapon.stamina;
                    staminaBar.SetStamina(currentStamina);
                }
                
            }
                /*case 2:
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
                    if (!infiniteHealth)
                    {
                        currentStamina -= staminaAttack;
                        staminaBar.SetStamina(currentStamina);
                    }
                    noWeaponCycle =0;
                    break;*/
            
            }
            
/*
        if(((swordKey) && pugnoAir.isPlaying==false) && controller.isGrounded && (anim.GetCurrentAnimatorStateInfo(0).IsName("swording")==false)){
            
        }*/

    }

    private void MoveToEnemy(){

        if(moveToEnemyYes){
            Transform test = GetNearestEnemy();
            if(test!=transform){
                transform.position = Vector3.Lerp(transform.position, test.position, 0.1f);
            }
            if(Vector3.Distance(transform.position, test.position)<=1.5f){
                moveToEnemyYes=false;
            }
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

        if(newEuler!=newOld && (verticalMovement!=0 || horizontalMovement!=0) && !rotateKey && dontMove==false){

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


    IEnumerator Attack(Transform attackPoint, int dmg, float time, float attackRange, float timeToReload, GameObject sh, bool blockMove){

        if(blockMove){
            dontMove=true;
        }
        shieldAvailable=false;
        if(sh!=null){ 
            meshDisabler(sh.transform);
        }
        



        curposdebug = attackPoint;
        attackFinished=false;
        verticalMovement=0;
        yield return new WaitForSeconds(time);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        Collider[] hitGigante = Physics.OverlapSphere(attackPoint.position, attackRange*3, enemyGiganteLayers);
        Collider[] hitCittadino = Physics.OverlapSphere(attackPoint.position, attackRange, cittadinoLayers);



        foreach(Collider enemy in hitEnemies){
            float singleStep = 1 * Time.deltaTime;

            lastEnemy=enemy;
            
            Debug.Log("Starei toccando un maranza");
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
            yield return new WaitForSeconds(timeToReload);
            bangFX.SetActive(false);
            dontMove=false;
        }

        foreach(Collider gigante in hitGigante){

            
            float singleStep = 1 * Time.deltaTime;
            float health = gigante.GetComponent<MaranzusGigante>().TakeDamage(dmg);

            pugno.PlayOneShot(pugno.clip, 1f);

            bangFX.SetActive(true);
            yield return new WaitForSeconds(timeToReload);
            bangFX.SetActive(false);

        }

        foreach(Collider cittadino in hitCittadino){
            cittadino.GetComponent<Cittadino>().StopIt();
        }
        //yield return new WaitForSeconds(0.5f);
        attackFinished=true;

        if(sh!=null){
            meshEnabler(sh.transform);
            //sh.SetActive(true);
        }
        shieldAvailable=true;

        if(blockMove){
            yield return new WaitForSeconds(time);
            dontMove=false;
        }
        
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

    public GameObject GetGameObject(){
        return gameObject;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        //Gizmos.DrawWireSphere (transform.position + curposdebug.position, attackRange);
    }


    private void meshDisabler(Transform t) {
        if (t.childCount > 0) {
            foreach (Transform child in t) {
                meshDisabler(child);
            }
        }
        if(t.gameObject.GetComponent<Renderer>()!=null){
            t.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void meshEnabler(Transform t) {
        if (t.childCount > 0) {
            foreach (Transform child in t) {
                meshEnabler(child);
            }
        }
        if(t.gameObject.GetComponent<Renderer>()!=null){
            t.gameObject.GetComponent<Renderer>().enabled = true;
        }
        
    }

    private Transform GetNearestEnemy(){



        GameObject [] enemiesGO = GameObject.FindGameObjectsWithTag("enemy");
        Transform [] enemiesT = new Transform[enemiesGO.Length];;

        for(int i = 0; i < enemiesGO.Length; i++){
            enemiesT[i] = enemiesGO[i].transform;
        }


        if(enemiesT.Length!=0){
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            foreach (Transform t in enemiesT)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            float distanza = Vector3.Distance(tMin.position, currentPos);
            if(distanza>4){
                return transform;
            } else return tMin;
        } else return transform;
        
    }

    public void SetDontMove(bool can){
        dontMove = can;
    }

    public List<int> GetAvailableHelmets()
    {
        return availableHelmets;
    }

    public List<int> GetAvailableChests()
    {
        return availableChests;
    }
    public List<int> GetAvailableWeapons()
    {
        return availableWeapons;
    }
    public List<int> GetAvailableShields()
    {
        return availableShields;
    }
    public List<int> GetAvailablePotions()
    {
        return availablePotions;
    }
}