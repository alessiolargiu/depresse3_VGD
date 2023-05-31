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
            hudWeapon = Resources.FindObjectsOfTypeAll<HUDInventoryWeapon>()[0];
        }
    }

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<FirstPersonController>();
        }
        if (player.availableWeapons.Contains(index) && GetComponent<QuestMarker>().enabled)
        {
            GameObject.Find("Compass").GetComponent<Compass>().RemoveQuestMarker(GetComponent<QuestMarker>());
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (hudWeapon == null)
            {
                hudWeapon = Resources.FindObjectsOfTypeAll<HUDInventoryWeapon>()[0];
            }
            if (index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            FindObjectOfType<Compass>().RemoveQuestMarker(GetComponent<QuestMarker>());
            GetComponent<QuestMarker>().enabled = false;
            player.GetAvailableWeapons().Add(index);
            hudWeapon.SetInventory(player.GetInventory(), player.GetAvailableWeapons());
        }
    }

}
