using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEquip : MonoBehaviour
{

    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public Sprite sprite;
    public HUDInventoryChest hudChest;
    public ChestEquip chestInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (chestInPlayer.index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            player.GetAvailableChests().Add(chestInPlayer.index);
            hudChest.SetInventory(player.GetInventory(), player.GetAvailableChests());
        }
    }

}
