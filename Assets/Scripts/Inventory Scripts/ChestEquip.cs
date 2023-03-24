using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEquip : MonoBehaviour
{

    public static int chestIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryChest hudChest;
    public Transform parentTrasform;

    private void OnTriggerEnter(Collider other)
    {
        index = chestIndex;
        chestIndex++;
        this.gameObject.SetActive(false);
        this.transform.parent = parentTrasform;
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        Inventory inv = player.GetInventory();
        inv.AddChest(this);
        hudChest.SetInventory(inv);
    }

}
