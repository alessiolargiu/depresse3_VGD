using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEquip : MonoBehaviour
{

    public static int shieldIndex = 1;
    private FirstPersonController player;
    public int index;
    public string nomeEquip;
    public float shieldValue;
    public int stamina;
    public Sprite sprite;
    public HUDInventoryShield hudShield;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<FirstPersonController>();
        }
        player = FindObjectOfType<FirstPersonController>();
        if (hudShield == null)
        {
            hudShield = Resources.FindObjectsOfTypeAll<HUDInventoryShield>()[0];
        }
    }

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<FirstPersonController>();
        }
        if (player.availableShields.Contains(index) && GetComponent<QuestMarker>().enabled)
        {
            GameObject.Find("Compass").GetComponent<Compass>().RemoveQuestMarker(GetComponent<QuestMarker>());
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (hudShield == null)
            {
                hudShield = Resources.FindObjectsOfTypeAll<HUDInventoryShield>()[0];
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
            player.GetAvailableShields().Add(index);
            hudShield.SetInventory(player.GetInventory(), player.GetAvailableShields());
        }
    }

}
