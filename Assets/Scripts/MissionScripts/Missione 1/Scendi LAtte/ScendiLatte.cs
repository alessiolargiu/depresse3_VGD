using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScendiLatte : MonoBehaviour
{
    public GameObject containerMissione;
    public GameObject prossimaMissione;
    public QuestMarker questMarker;
    bool stop;

    void Awake()
    {
        FindObjectOfType<Compass>().RemoveQuestMarker(questMarker);
        Destroy(questMarker);
        prossimaMissione.SetActive(true);
        containerMissione.SetActive(false);
    }

}
