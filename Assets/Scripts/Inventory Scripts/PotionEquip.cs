using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEquip : MonoBehaviour
{

    public static int potionIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public int cureValue;
    public int potionNumber;
    public bool isEquiped = false;
    public Sprite sprite;
    public HUDInventoryPotion hudPotion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            this.gameObject.SetActive(false);
            player.GetAvailablePotions().Add(this.index);
            hudPotion.SetInventory(player.GetInventory(), player.GetAvailablePotions());
        }
    }
}
