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
    public Transform parentTrasform;

    private void OnTriggerEnter(Collider other)
    {
        index = weaponIndex;
        weaponIndex++;
        this.gameObject.SetActive(false);
        this.transform.parent = parentTrasform;
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        Inventory inv = player.GetInventory();
        inv.AddWeapon(this);
        hudWeapon.SetInventory(inv);
    }

}
