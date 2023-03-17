using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryShoe : MonoBehaviour
{
    
    private List<Armor> shoes; 
    public Transform itemSlotContainer;
    public Transform itemSlot;
    public Sprite itemSprite;

    public void SetInventory(List<Armor> shoes)
    {
        this.shoes = shoes;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        int x = 90, y = 0;
        foreach (Armor shoe in shoes)
        {
            RectTransform itemSlotRectTransform =  Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);
            itemSprite = shoe.GetSprite();
            x += 152;
        }
    }

}
