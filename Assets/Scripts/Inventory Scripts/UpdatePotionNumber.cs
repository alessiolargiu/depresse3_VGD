using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePotionNumber : MonoBehaviour
{

    public FirstPersonController player;
    public TMP_Text numeroPozioni;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        if(numeroPozioni == null)
        {
            numeroPozioni = GameObject.Find("Number").GetComponent<TMP_Text>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        bool unaPozioneEquipaggiata = false;
        if(player.GetInventory().GetPotions().Count > 0)
        {
            foreach (PotionEquip potion in player.GetInventory().GetPotions())
            {
                if (potion.isEquiped)
                {
                    numeroPozioni.text = potion.potionNumber.ToString();
                    unaPozioneEquipaggiata = true;
                }
            }
            if (!unaPozioneEquipaggiata)
            {
                numeroPozioni.text = "0";
            }
        }
        
    }
}
