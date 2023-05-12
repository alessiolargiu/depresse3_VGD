using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventoryChest : MonoBehaviour
{

    private Inventory inventory;
    private List<int> availableChests;
    public GameObject invObject1;
    public Image objectImage1;
    public TMP_Text objectText1;
    public GameObject invObject2;
    public Image objectImage2;
    public TMP_Text objectText2;
    public GameObject invObject3;
    public Image objectImage3;
    public TMP_Text objectText3;

    public void SetInventory(Inventory inventory, List<int> availableChests)
    {
        this.inventory = inventory;
        this.availableChests = availableChests;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {

        foreach (ChestEquip chest in inventory.GetChests())
        {
            if (!GameObject.Find("GameManager").GetComponent<GameManager>().fullEquip)
            {
                foreach (int i in availableChests)
                {
                    //se un'elmo è disponbile allora lo mostro in HUD
                    if (chest.index == i)
                    {
                        switch (chest.index)
                        {
                            case 1:
                                invObject1.SetActive(true);
                                objectImage1.sprite = chest.sprite;
                                objectText1.text = chest.nomeEquip;
                                break;
                            case 2:
                                invObject2.SetActive(true);
                                objectImage2.sprite = chest.sprite;
                                objectText2.text = chest.nomeEquip;
                                break;
                            case 3:
                                invObject3.SetActive(true);
                                objectImage3.sprite = chest.sprite;
                                objectText3.text = chest.nomeEquip;
                                break;
                        }
                    }
                }
            }
            else
            {
                switch (chest.index)
                {
                    case 1:
                        invObject1.SetActive(true);
                        objectImage1.sprite = chest.sprite;
                        objectText1.text = chest.nomeEquip;
                        break;
                    case 2:
                        invObject2.SetActive(true);
                        objectImage2.sprite = chest.sprite;
                        objectText2.text = chest.nomeEquip;
                        break;
                    case 3:
                        invObject3.SetActive(true);
                        objectImage3.sprite = chest.sprite;
                        objectText3.text = chest.nomeEquip;
                        break;
                }
            }
        }
    }

}
