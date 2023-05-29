using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBarGigante : MonoBehaviour
{

    public TMP_Text vitaGiganteText;

    private void Update()
    {
        if(FindObjectOfType<MaranzusGigante>() != null)
        {
            float vitaGigante = FindObjectOfType<MaranzusGigante>().health;
            float maxVitaGigante = FindObjectOfType<MaranzusGigante>().maxHealth;
            if (vitaGigante > 0)
            {
                vitaGiganteText.text = vitaGigante + "/" + maxVitaGigante;
            }
        }
        
    }
}
