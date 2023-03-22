using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEquip : MonoBehaviour
{

    public FirstPersonController player;
    private Armor chest;
    public string nomeEquip;
    public float armorValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryChest hudChest;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        chest = new Armor(nomeEquip, sprite, stamina, armorValue, Armor.ArmorType.Chest);
        Inventory inv = player.GetInventory();
        inv.AddChest(chest);
        hudChest.SetInventory(inv);
    }

    public Armor GetChest()
    {
        return chest;
    }

}
