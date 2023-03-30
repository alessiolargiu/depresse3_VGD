using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetEquip : MonoBehaviour
{

    public static int helmetIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryHelmet hudHelmet;
    public HelmetEquip helmetInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        helmetInPlayer.index = helmetIndex;
        helmetIndex++;
        this.gameObject.SetActive(false);
        Inventory inv = player.GetInventory();
        inv.AddHelmet(helmetInPlayer);
        hudHelmet.SetInventory(inv);
    }

}
