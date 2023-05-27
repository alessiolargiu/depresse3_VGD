using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetEquip : MonoBehaviour
{

    private FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float armorValue;
    public Sprite sprite;
    public HUDInventoryHelmet hudHelmet;

    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
        if (hudHelmet == null)
        {
            hudHelmet = GameObject.Find("Inv Helmet").GetComponent<HUDInventoryHelmet>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (hudHelmet == null)
            {
                hudHelmet = GameObject.Find("Inv Helmet").GetComponent<HUDInventoryHelmet>();
            }
            if (index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            GameObject.Find("Compass").GetComponent<Compass>().RemoveQuestMarker(GetComponent<QuestMarker>());
            player.GetAvailableHelmets().Add(index);
            hudHelmet.SetInventory(player.GetInventory(), player.GetAvailableHelmets());
        }
    }

}
