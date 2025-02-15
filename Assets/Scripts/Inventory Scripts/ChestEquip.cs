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
            hudChest = Resources.FindObjectsOfTypeAll<HUDInventoryChest>()[0];
        }
    }

    private void Awake()
    {
        if(player == null)
        {
            player = FindObjectOfType<FirstPersonController>();
        }
        if (player.availableChests.Contains(index) && index != 0 && GetComponent<QuestMarker>().enabled)
        {
            GameObject.Find("Compass").GetComponent<Compass>().RemoveQuestMarker(GetComponent<QuestMarker>());
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("player"))
        {
            if (hudChest == null)
            {
                hudChest = Resources.FindObjectsOfTypeAll<HUDInventoryChest>()[0];
            }
            if (index != 0)
            {
                this.gameObject.SetActive(false);
                GameObject.Find("Compass").GetComponent<Compass>().RemoveQuestMarker(GetComponent<QuestMarker>());
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
