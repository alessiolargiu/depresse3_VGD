using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelmetEquip : MonoBehaviour
{

    public static int helmetIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public Sprite sprite;
    public HUDInventoryHelmet hudHelmet;
    public HelmetEquip helmetInPlayer;

    public HelmetEquip(){}

    public HelmetEquip(int index, string nomeEquip, float armorValue)
    {
        this.index = index;
        this.nomeEquip = nomeEquip;
        this.armorValue = armorValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        helmetInPlayer.index = helmetIndex;
        helmetIndex++;
        
        Inventory inv = player.GetInventory();
        inv.AddHelmet(helmetInPlayer);
        hudHelmet.SetInventory(inv);
        this.gameObject.SetActive(false);
    }

}
