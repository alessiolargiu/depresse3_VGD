using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelmetEquip : MonoBehaviour
{

    public static int helmetIndex = 0;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public Sprite sprite;
    public HUDInventoryHelmet hudHelmet;
    public HelmetEquip helmetInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        helmetInPlayer.index = helmetIndex;

        if (helmetIndex != 0)
        {
            this.gameObject.SetActive(false);
            
        }
        else
        {
            this.GetComponent<BoxCollider>().isTrigger = false;
        }
        Inventory inv = player.GetInventory();
        inv.AddHelmet(helmetInPlayer);
        hudHelmet.SetInventory(inv);
        helmetIndex++;
    }

}
