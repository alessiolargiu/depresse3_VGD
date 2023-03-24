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
    public Transform parentTrasform;

    private void OnTriggerEnter(Collider other)
    {
        index = helmetIndex;
        helmetIndex++;
        this.gameObject.SetActive(false);
        this.transform.parent = parentTrasform;
        this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        Inventory inv = player.GetInventory();
        inv.AddHelmet(this);
        hudHelmet.SetInventory(inv);
    }

}
