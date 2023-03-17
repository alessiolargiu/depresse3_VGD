using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryWeapon : MonoBehaviour
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
        foreach (Weapon weapon in inventory.GetWeapons())
        {
            switch (weapon.GetIndex()) {
                case 0:
                    invObject1.SetActive(true);
                    objectImage1.sprite = weapon.GetSprite();
                    objectText1.text = weapon.GetName();
                    break;
                case 1:
                    invObject2.SetActive(true);
                    objectImage2.sprite = weapon.GetSprite();
                    objectText2.text = weapon.GetName();
                    break;
                case 2:
                    invObject3.SetActive(true);
                    objectImage3.sprite = weapon.GetSprite();
                    objectText3.text = weapon.GetName();
                    break;
            }
        }
    }

}
