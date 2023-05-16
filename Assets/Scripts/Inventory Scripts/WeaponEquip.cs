using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{

    public static int weaponIndex = 1;
    private FirstPersonController player;
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

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (weaponInPlayer.index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            player.GetAvailableWeapons().Add(weaponInPlayer.index);
            hudWeapon.SetInventory(player.GetInventory(), player.GetAvailableWeapons());
        }
    }

}
