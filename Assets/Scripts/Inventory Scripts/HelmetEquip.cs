using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetEquip : MonoBehaviour
{

    public FirstPersonController player;
    private Armor helmet;
    public string nomeEquip;
    public float armorValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryHelmet hudHelmet;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        helmet = new Armor(nomeEquip, sprite, stamina, armorValue, Armor.ArmorType.Helmet);
        Inventory inv = player.GetInventory();
        inv.AddHelmet(helmet);
        hudHelmet.SetInventory(inv);
    }

}
