using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScendiLatte : MonoBehaviour
{
    public GameObject containerMissione;
    public GameObject prossimaMissione;
    bool stop;

    void Awake()
    {
        prossimaMissione.SetActive(true);
        containerMissione.SetActive(false);
    }

}
