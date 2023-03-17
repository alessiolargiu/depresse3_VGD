using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryShield : MonoBehaviour
{
    
    private List<Shield> shields;
    public Transform itemSlotContainer;
    public Transform itemSlot;
    public Sprite itemSprite;

    public void SetInventory(List<Shield> shields)
    {
        this.shields = shields;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        int x = 90, y = 0;
        foreach (Shield shield in shields)
        {
            RectTransform itemSlotRectTransform =  Instantiate(itemSlot, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x, y);
            itemSprite = shield.GetSprite();
            x += 152;
        }
    }

}
