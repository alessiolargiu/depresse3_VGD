using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{

    public static int weaponIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public int damage;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryWeapon hudWeapon;
    public WeaponEquip weaponInPlayer;
    public float innerRange;
    public float reloadTime;

    public bool colliding;

    private void OnTriggerEnter(Collider other)
    {
        weaponInPlayer.index = weaponIndex;
        weaponIndex++;
        this.gameObject.SetActive(false);
        Inventory inv = player.GetInventory();
        inv.AddWeapon(weaponInPlayer);
        hudWeapon.SetInventory(inv);
    }

}
