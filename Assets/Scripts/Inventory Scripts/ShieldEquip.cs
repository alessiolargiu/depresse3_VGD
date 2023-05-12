using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEquip : MonoBehaviour
{

    public static int shieldIndex = 1;
    public FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float shieldValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryShield hudShield;
    public ShieldEquip shieldInPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (shieldInPlayer.index != 0)
            {
                this.gameObject.SetActive(false);

            }
            else
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
            player.GetAvailableShields().Add(shieldInPlayer.index);
            hudShield.SetInventory(player.GetInventory(), player.GetAvailableShields());
        }
    }

}
