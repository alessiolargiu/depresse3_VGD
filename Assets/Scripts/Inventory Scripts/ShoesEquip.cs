using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesEquip : MonoBehaviour
{

    public static int shoeIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryShoe hudShoe;
    public Transform parentTrasform;

    private void OnTriggerEnter(Collider other)
    {
        index = shoeIndex;
        shoeIndex++;
        this.gameObject.SetActive(false);
        this.transform.parent = parentTrasform;
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        Inventory inv = player.GetInventory();
        inv.AddShoe(this);
        hudShoe.SetInventory(inv);
    }

}
