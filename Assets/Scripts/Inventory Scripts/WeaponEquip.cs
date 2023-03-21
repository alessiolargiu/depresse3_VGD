using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{

    public FirstPersonController player;
    private Weapon weapon;
    public string nomeEquip;
    public int damage;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryWeapon hudWeapon;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        weapon = new Weapon(damage, stamina, nomeEquip, sprite);
        Inventory inv = player.GetInventory();
        inv.AddWeapon(weapon);
        hudWeapon.SetInventory(inv);
    }

}
