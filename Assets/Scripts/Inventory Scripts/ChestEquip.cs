using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEquip : MonoBehaviour
{

    private FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public Sprite sprite;
    public HUDInventoryChest hudChest;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        if (hudChest == null)
        {
            hudChest = GameObject.Find("Inv Chest").GetComponent<HUDInventoryChest>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (hudChest == null)
            {
                hudChest = GameObject.Find("Inv Chest").GetComponent<HUDInventoryChest>();
            }
            if (index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            player.GetAvailableChests().Add(index);
            hudChest.SetInventory(player.GetInventory(), player.GetAvailableChests());
        }
    }

}
