using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEquip : MonoBehaviour
{

    public static int chestIndex = 0;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public Sprite sprite;
    public HUDInventoryChest hudChest;
    public ChestEquip chestInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        chestInPlayer.index = chestIndex;

        if (chestIndex != 0)
        {
            this.gameObject.SetActive(false);

        }
        else
        {
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
        Inventory inv = player.GetInventory();
        inv.AddChest(chestInPlayer);
        hudChest.SetInventory(inv);
        chestIndex++;

        Debug.Log("chestindex: " + chestIndex);
    }

}
