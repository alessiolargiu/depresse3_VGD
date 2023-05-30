using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineMissione : MonoBehaviour
{   
    public GameObject container;
    public QuestMarker questMarker;
    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<Compass>().RemoveQuestMarker(questMarker);
        Destroy(questMarker);
        DestroyObject(container);
    }


}
