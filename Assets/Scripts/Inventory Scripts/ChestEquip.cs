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
    public Sprite sprite;
    public HUDInventoryChest hudChest;
    public ChestEquip chestInPlayer;

    public ChestEquip() { }

    public ChestEquip(int index, string nomeEquip, float armorValue)
    {
        this.index = index;
        this.nomeEquip = nomeEquip;
        this.armorValue = armorValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        chestInPlayer.index = chestIndex;
        chestIndex++;
        this.gameObject.SetActive(false);
        Inventory inv = player.GetInventory();
        inv.AddChest(chestInPlayer);
        hudChest.SetInventory(inv);
    }

}
