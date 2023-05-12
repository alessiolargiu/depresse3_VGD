using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryWeapon : MonoBehaviour
{
    
    private Inventory inventory;
    private List<int> availableWeapons;
    public GameObject invObject1;
    public Image objectImage1;
    public TMP_Text objectText1;
    public GameObject invObject2;
    public Image objectImage2;
    public TMP_Text objectText2;
    public GameObject invObject3;
    public Image objectImage3;
    public TMP_Text objectText3;

    public void SetInventory(Inventory inventory, List<int> availableWeapons)
    {
        this.inventory = inventory;
        this.availableWeapons = availableWeapons;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {

        foreach (WeaponEquip weapon in inventory.GetWeapons())
        {
            if (!GameObject.Find("GameManager").GetComponent<GameManager>().fullEquip)
            {
                foreach (int i in availableWeapons)
                {
                    //se un'elmo è disponbile allora lo mostro in HUD
                    if (weapon.index == i)
                    {
                        switch (weapon.index)
                        {
                            case 1:
                                invObject1.SetActive(true);
                                objectImage1.sprite = weapon.sprite;
                                objectText1.text = weapon.nomeEquip;
                                break;
                            case 2:
                                invObject2.SetActive(true);
                                objectImage2.sprite = weapon.sprite;
                                objectText2.text = weapon.nomeEquip;
                                break;
                            case 3:
                                invObject3.SetActive(true);
                                objectImage3.sprite = weapon.sprite;
                                objectText3.text = weapon.nomeEquip;
                                break;
                        }
                    }
                }
            }
            else
            {
                switch (weapon.index)
                {
                    case 1:
                        invObject1.SetActive(true);
                        objectImage1.sprite = weapon.sprite;
                        objectText1.text = weapon.nomeEquip;
                        break;
                    case 2:
                        invObject2.SetActive(true);
                        objectImage2.sprite = weapon.sprite;
                        objectText2.text = weapon.nomeEquip;
                        break;
                    case 3:
                        invObject3.SetActive(true);
                        objectImage3.sprite = weapon.sprite;
                        objectText3.text = weapon.nomeEquip;
                        break;
                }
            }
        }
    }

}
