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
    public HelmetEquip helmetInPlayer;


    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (helmetInPlayer.index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            player.GetAvailableHelmets().Add(helmetInPlayer.index);
            hudHelmet.SetInventory(player.GetInventory(), player.GetAvailableHelmets());
        }
    }

}
