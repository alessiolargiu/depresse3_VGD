using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryPotion : MonoBehaviour
{
    
    private List<Potion> potions;
    public Transform itemSlotContainer;
    public Transform itemSlot;
    public Sprite itemSprite;

    public void SetInventory(List<Potion> potions)
    {
        this.potions = potions;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 90, y = 0;
        foreach (Potion potion in potions)
        {
            RectTransform itemSlotRectTransform =  Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);
            itemSprite = potion.GetSprite();
            x += 152;
        }
    }

}
