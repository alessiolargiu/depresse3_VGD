using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesEquip : MonoBehaviour
{

    public FirstPersonController player;
    private Armor shoes;
    public string nomeEquip;
    public float armorValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryShoe hudShoe;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        shoes = new Armor(nomeEquip, sprite, stamina, armorValue, Armor.ArmorType.Shoe);
        Inventory inv = player.GetInventory();
        inv.AddShoe(shoes);
        hudShoe.SetInventory(inv);
    }

}
