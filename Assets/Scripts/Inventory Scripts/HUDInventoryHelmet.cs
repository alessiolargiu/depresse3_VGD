using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryHelmet : MonoBehaviour
{
    
    private List<Armor> helmets;
    public Transform itemSlotContainer;
    public Transform itemSlot;
    public Sprite itemSprite;

    public void SetInventory(List<Armor> helmets)
    {
        this.helmets = helmets;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        int x = 90, y = 0;
        foreach (Armor helmet in helmets)
        {
            RectTransform itemSlotRectTransform =  Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);
            itemSprite = helmet.GetSprite();
            x += 152;
        }
    }

}
