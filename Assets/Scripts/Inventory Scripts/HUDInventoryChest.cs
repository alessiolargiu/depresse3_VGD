using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryChest : MonoBehaviour
{
    
    private List<Armor> chests;
    public Transform itemSlotContainer;
    public Transform itemSlot;
    public Sprite itemSprite;

    public void SetInventory(List<Armor> chests)
    {
        this.chests = chests;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        int x = 90, y = 0;
        foreach (Armor chest in chests)
        {
            RectTransform itemSlotRectTransform =  Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);
            itemSprite = chest.GetSprite();
            x += 152;
        }
    }

}
