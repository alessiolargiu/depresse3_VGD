using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryShoe : MonoBehaviour
{

    private Inventory inventory;
    public GameObject invObject1;
    public Image objectImage1;
    public TMP_Text objectText1;
    public GameObject invObject2;
    public Image objectImage2;
    public TMP_Text objectText2;
    public GameObject invObject3;
    public Image objectImage3;
    public TMP_Text objectText3;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        int x = 90, y = 0;
        foreach (ShoesEquip shoe in inventory.GetShoes())
        {
            switch (shoe.index)
            {
                case 1:
                    invObject1.SetActive(true);
                    objectImage1.sprite = shoe.sprite;
                    objectText1.text = shoe.nomeEquip;
                    break;
                case 2:
                    invObject2.SetActive(true);
                    objectImage2.sprite = shoe.sprite;
                    objectText2.text = shoe.nomeEquip;
                    break;
                case 3:
                    invObject3.SetActive(true);
                    objectImage3.sprite = shoe.sprite;
                    objectText3.text = shoe.nomeEquip;
                    break;
            }
        }
    }

}
