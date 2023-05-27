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
    public float innerRange;
    public float reloadTime;

    public bool colliding;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        if (hudWeapon == null)
        {
            hudWeapon = GameObject.Find("Inv Weapon").GetComponent<HUDInventoryWeapon>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (hudWeapon == null)
            {
                hudWeapon = GameObject.Find("Inv Weapon").GetComponent<HUDInventoryWeapon>();
            }
            if (index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            GameObject.Find("Compass").GetComponent<Compass>().RemoveQuestMarker(GetComponent<QuestMarker>());
            player.GetAvailableWeapons().Add(index);
            hudWeapon.SetInventory(player.GetInventory(), player.GetAvailableWeapons());
        }
    }

}
