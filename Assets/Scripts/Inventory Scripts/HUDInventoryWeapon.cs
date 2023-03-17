using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryWeapon : MonoBehaviour
{
    
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlot;


    private void Awake()
    {
        itemSlotContainer = transform.Find("Inv Container");
        itemSlot = itemSlotContainer.Find("Inv Object");

    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        int x = 90, y = 0;
        foreach (Weapon weapon in inventory.GetWeapons())
        {
            Debug.Log(weapon.GetName());
            RectTransform itemSlotRectTransform =  Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);
            itemSlotRectTransform.Find("Equip Image").GetComponent<Image>().sprite = weapon.GetSprite();
            itemSlot.GetComponent<Image>().sprite = weapon.GetSprite();
            itemSlot.GetComponentInChildren<TMP_Text>().text = "PROVAA";
            x += 152;
        }
    }

}
