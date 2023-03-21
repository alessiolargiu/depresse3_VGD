using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEquip : MonoBehaviour
{
    public FirstPersonController player;
    private Potion potion;
    public string nomeEquip;
    public int cureValue;
    public Sprite sprite;
    public HUDInventoryPotion hudPotion;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        potion = new Potion(nomeEquip, sprite, cureValue);
        Inventory inv = player.GetInventory();
        inv.AddPotion(potion);
        hudPotion.SetInventory(inv);
    }
}
