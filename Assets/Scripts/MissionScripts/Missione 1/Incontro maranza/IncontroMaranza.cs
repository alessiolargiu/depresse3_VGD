using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncontroMaranza : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject containerMissione;
    public GameObject [] maranzus;
    public GameObject cutscenefinale;
    public QuestMarker questMarker;

    private TMP_Text contatore;
    private TMP_Text missionText;

    bool stop;

    void Start()
    {
        stop = false;
        //missionText.gameObject.SetActive(true); 
        //missionText.text = "Hai iniziato la prima missione. Uccidi i maranzus.";
        Debug.Log("Hai iniziato la missione");
        containerMissione.SetActive(true);
        contatore = GameObject.Find("Contatore").GetComponent<TMP_Text>();
        contatore.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {   
        int scontrino = 0;
        Debug.Log("la lunghezza di maranzus è " + maranzus.Length);
        for(int i=0; i<maranzus.Length; i++){
            if(maranzus[i]==null){
                scontrino++;
            }
        }
        contatore.text = "Hai ucciso\n" + scontrino + " maranza su " + maranzus.Length;
        if(scontrino==maranzus.Length){
            stop=true;
        }

        if(stop){
            Debug.Log("Hai finito la missione");
            contatore.text = " ";
            //missionText.gameObject.SetActive(false);
            DestroyObject(containerMissione);
            cutscenefinale.SetActive(true);
            FindObjectOfType<Compass>().RemoveQuestMarker(questMarker);
            Destroy(questMarker);
            StartCoroutine(cutscenefinale.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
        }
    }
}
