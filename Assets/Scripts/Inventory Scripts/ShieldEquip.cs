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
    public Transform parentTrasform;

    private void OnTriggerEnter(Collider other)
    {
        index = shieldIndex;
        shieldIndex++;
        this.gameObject.SetActive(false);
        this.transform.parent = parentTrasform;
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        Inventory inv = player.GetInventory();
        inv.AddShield(this);
        hudShield.SetInventory(inv);
    }

}
