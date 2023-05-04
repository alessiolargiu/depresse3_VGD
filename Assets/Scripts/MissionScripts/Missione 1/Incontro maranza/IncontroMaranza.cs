using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncontroMaranza : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject containerMissione;
    public GameObject [] maranzus;
    public GameObject cutscenefinale;

    bool stop;

    void Start()
    {
        stop = false;
        Debug.Log("Hai iniziato la missione");
        containerMissione.SetActive(true);
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
        if(scontrino==maranzus.Length){
            stop=true;
        }

        if(stop){
            Debug.Log("Hai finito la missione");
            cutscenefinale.SetActive(true);
            StartCoroutine(cutscenefinale.GetComponent<CutSceneScript>().cutsceneStart((paolino) => {if(paolino){DestroyObject(gameObject);}}));
        }
    }
}
