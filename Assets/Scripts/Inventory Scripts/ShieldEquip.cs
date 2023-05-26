using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEquip : MonoBehaviour
{

    public static int shieldIndex = 1;
    private FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float shieldValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryShield hudShield;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        if (hudShield == null)
        {
            hudShield = GameObject.Find("Inv Shield").GetComponent<HUDInventoryShield>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (hudShield == null)
            {
                hudShield = GameObject.Find("Inv Shield").GetComponent<HUDInventoryShield>();
            }
            if (index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            player.GetAvailableShields().Add(index);
            hudShield.SetInventory(player.GetInventory(), player.GetAvailableShields());
        }
    }

}
