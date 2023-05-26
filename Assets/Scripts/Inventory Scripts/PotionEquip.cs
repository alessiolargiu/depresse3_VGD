using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEquip : MonoBehaviour
{

    public static int potionIndex = 1;
    private FirstPersonController player;
    public int index;
    public string nomeEquip;
    public int cureValue;
    public int potionNumber;
    public bool isEquiped = false;
    public Sprite sprite;
    public HUDInventoryPotion hudPotion;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        if (hudPotion == null)
        {
            hudPotion = GameObject.Find("Inv Potion").GetComponent<HUDInventoryPotion>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if(hudPotion == null)
            {
                hudPotion = GameObject.Find("Inv Potion").GetComponent<HUDInventoryPotion>();
            }
            this.gameObject.SetActive(false);
            if (!player.GetAvailablePotions().Contains(this.index))
            {
                player.GetAvailablePotions().Add(this.index);
            }
            player.GetInventory().AddPotion(this);
            Debug.Log("indice pozione: " + index);
            //prova commit
            hudPotion.SetInventory(player.GetInventory(), player.GetAvailablePotions());
        }
    }
}
