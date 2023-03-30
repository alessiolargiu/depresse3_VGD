using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEquip : MonoBehaviour
{

    public static int shieldIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float shieldValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryShield hudShield;
    public ShieldEquip shieldInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        shieldInPlayer.index = shieldIndex;
        shieldIndex++;
        this.gameObject.SetActive(false);
        Inventory inv = player.GetInventory();
        inv.AddShield(shieldInPlayer);
        hudShield.SetInventory(inv);
    }

}
