using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEquip : MonoBehaviour
{

    public FirstPersonController player;
    private Shield shield;
    public string nomeEquip;
    public float shieldValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryShield hudShield;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        shield = new Shield(nomeEquip, sprite, shieldValue, stamina);
        Inventory inv = player.GetInventory();
        inv.AddShield(shield);
        hudShield.SetInventory(inv);
    }

}
