using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEquip : MonoBehaviour
{

    public static int potionIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public int cureValue;
    public Sprite sprite;
    public HUDInventoryPotion hudPotion;
    public Transform parentTrasform;

    private void OnTriggerEnter(Collider other)
    {
        index = potionIndex;
        potionIndex++;
        this.gameObject.SetActive(false);
        this.transform.parent = parentTrasform;
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        Inventory inv = player.GetInventory();
        inv.AddPotion(this);
        hudPotion.SetInventory(inv);
    }
}
