using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMission : MonoBehaviour
{
    // Start is called before the first frame update

    public FirstPersonController player;
    public Sprite icon;
    public QuestMarker target;
    public GameObject toDie;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        /*
      if(toDie.health>0){
            toDie.AddComponent<QuestMarker>() as prova;
            prova.icon = icon;

            Debug.Log("Il target è ancora vivo");
        } else {
            Debug.Log("Il target è morto");
        }*/
    }
}
