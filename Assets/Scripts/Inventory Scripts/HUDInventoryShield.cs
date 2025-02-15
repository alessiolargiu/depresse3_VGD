using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryShield : MonoBehaviour
{

    private Inventory inventory;
    private List<int> availableShields;
    public GameObject invObject1;
    public Image objectImage1;
    public TMP_Text objectText1;
    public GameObject invObject2;
    public Image objectImage2;
    public TMP_Text objectText2;
    public GameObject invObject3;
    public Image objectImage3;
    public TMP_Text objectText3;

    public void SetInventory(Inventory inventory, List<int> availableShields)
    {
        this.inventory = inventory;
        this.availableShields = availableShields;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        foreach (ShieldEquip shield in inventory.GetShields())
        {
            if (!GameObject.Find("GameManager").GetComponent<GameManager>().fullEquip)
            {
                foreach (int i in availableShields)
                {
                    //se un'elmo � disponbile allora lo mostro in HUD
                    if (shield.index == i)
                    {
                        switch (shield.index)
                        {
                            case 1:
                                invObject1.SetActive(true);
                                objectImage1.sprite = shield.sprite;
                                objectText1.text = shield.nomeEquip;
                                break;
                            case 2:
                                invObject2.SetActive(true);
                                objectImage2.sprite = shield.sprite;
                                objectText2.text = shield.nomeEquip;
                                break;
                            case 3:
                                invObject3.SetActive(true);
                                objectImage3.sprite = shield.sprite;
                                objectText3.text = shield.nomeEquip;
                                break;
                        }
                    }
                }
            }
            else
            {
                switch (shield.index)
                {
                    case 1:
                        invObject1.SetActive(true);
                        objectImage1.sprite = shield.sprite;
                        objectText1.text = shield.nomeEquip;
                        break;
                    case 2:
                        invObject2.SetActive(true);
                        objectImage2.sprite = shield.sprite;
                        objectText2.text = shield.nomeEquip;
                        break;
                    case 3:
                        invObject3.SetActive(true);
                        objectImage3.sprite = shield.sprite;
                        objectText3.text = shield.nomeEquip;
                        break;
                }
            }
        }
    }

}
