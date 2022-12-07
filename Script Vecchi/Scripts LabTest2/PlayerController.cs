using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Dichiariamo una variabile di tipo Rigidbody(SerializeField serve per renderla visibile da unity)
    [SerializeField]
    private Rigidbody rb;
    private bool canJump = true;
    public List<TextMeshProUGUI> wallText;
    private int punteggio = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Se abbiamo dichiarato private la variabile Rigidbody, 
        // allora all'interno della Start dobbiamo inizializzare la nostra
        // variabile Rigidbody tramite la GetComponent<>()
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Quando spostiamo il nostro Game Object
    // tramite Rigidbody, tutto si svolge nella FixedUpdate()
    private void FixedUpdate(){

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        float jumpMovement = Input.GetAxis("Jump");

        Vector3 force = new Vector3(0.5f * horizontalMovement, canJump ? 2*jumpMovement : 0f, 0.5f * verticalMovement);
        rb.AddForce(force, ForceMode.Impulse);

    }

    void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

    // Per interagire con Trigger Collider utilizziamo la OnTriggerEnter()
    // Il Collider in input "other" � il collider con cui il
    // nostro player entra in contatto
    void OnTriggerEnter(Collider other){

        if (other.CompareTag("Collectable")){
            // La Destroy() qui sotto rimuove dalla scena l'oggetto
            //Destroy(other.gameObject);
            // Se vogliamo solo disattivarlo allora utilizziamo la SetActive()
            // e cambiamo lo status 
            // La SetActvie() pu� essere utile se vogliamo far riapparire
            // l'oggetto pi� avanti (magari tramite timer)
            other.gameObject.SetActive(false);
            punteggio++;
            for (int i = 0; i < 4; i++) {
                Debug.Log("Palle");
                wallText[i].SetText("Punteggio: " + punteggio);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }

}